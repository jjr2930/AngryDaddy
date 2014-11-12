using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System;

public class Maingame : MonoBehaviour {
    public static Maingame instance;
    public void Awake()
    {
        Maingame.instance = this;
    }
    enum Status {Play, TimeOver, Clear, Fail, Judge, End};
    Status _status = Status.Play;


    //Chat/설명/대답
    public GameObject _ChatPanel;
    public GameObject _EnemyChat;
    public GameObject _PlayerChat;
    public GameObject _Desc;
    public GameObject _Awer1;
    public GameObject _Awer2;
    public GameObject _Awer3;
    public GameObject _Awer4;
    public GameObject _ResultWin;
    public GameObject _MedalWin;

    //이펙트
    public GameObject _hit;
    public GameObject _critical;
    public GameObject _combo;
    public GameObject _emergency;
    public GameObject _fingame;


    //HP바/별/타이머
    public GameObject _star;
    public GameObject _HpE;
    public GameObject _HpEEffect;
    public GameObject _HpP;
    public GameObject _HpPEffect;
    public GameObject _Timer;


    //플레이어NPC
    public GameObject _Player;
    public GameObject _npcpan1;
    public GameObject _npcpan2;
    public GameObject _npcpan3;

    //얼굴
    public GameObject _EFace;
    public GameObject _PFace;

    //스테이지숫자
    public GameObject _num;

    //공격자
    string _attacker = "";

    //제한시간
    float _limitTime = 600f;
    float _time = 0f;
    //HP
    float _npcPower = 1000f;
    float _playerPower = 1000f;
    //CompoCnt;
    int _Combo = 0;
    //Level
    int _Level = 1;

    //Panel 
    float _yPos = 0f;

    string filePath = "";
    Rootobject _obj;

    //Log
    public Log _log;
    List<playTime> _ptime = new List<playTime>();
    List<answerSheet> _awsheet = new List<answerSheet>();
    float _startTime = 0;

    /// <summary>
    /// 정열이가 만듬
    /// </summary>
    //private UISprite[] _talks;
    private List<GameObject> _talks;
    public GameObject _dadyTalk;
    GameObject _beforeObj = null;
    private int testInt =0;
    // Use this for initialization
	void Start () {
        _talks = new List<GameObject>();
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Levels.json");
        _log = JSONSerializer.Deserialize<Log>(PlayerPrefs.GetString("gamelog"));
        if (_log._sex == 1) _Player.transform.FindChild("son").GetComponent<UISprite>().spriteName = "son1";
        DeserialJson(GetJson());
        _time = _limitTime;
       
        _startTime = Time.time;
        StartCoroutine(SetInit(_Level));
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        #region 안볼꺼얌
        /*
        _talks = _ChatPanel.GetComponentsInChildren<UISprite>();

        if (null == _talks)
        {
            Debug.Log("아직 아니야");
        }
        else
        {
            for (int i = 1; i < _talks.Length; i++)
            {
                //앞에것 위치 - 앞에것 크기 - 자신의 크기
                float _beforePos = _talks[i - 1].GetComponentInChildren<UISprite>().transform.localPosition.y;
                float _beforeSize = _talks[i - 1].GetComponentInChildren<UILabel>().height;
                float _nowSize = _talks[i].GetComponentInChildren<UILabel>().height;

                //Debug.Log("local y : " + _talks[i - 1].GetComponentInChildren<UISprite>().transform.localPosition.y);
                //Debug.Log("height :" +_talks[i - 1].GetComponentInChildren<UILabel>().height / 2);
                
                _yPos = _beforePos - _beforeSize/2 - _nowSize/2;

                _talks[i].transform.localPosition = new Vector3(0, _yPos - 10, 0);
            }
            
        }
        */
        #endregion
        if (_status == Status.Play) GetRemainTime();
        if (_status == Status.Play) SetUpdateFace();
        if (_status == Status.TimeOver || _status == Status.Clear || _status == Status.Fail || _status == Status.Judge) SetResult();
    }

    void SetUpdateFace()
    {
        if (_npcPower > 700f)
            _EFace.GetComponent<UISprite>().spriteName = "redHP_Max";
        else if (_npcPower <= 700f && _npcPower > 300f)
            _EFace.GetComponent<UISprite>().spriteName = "redHP_Mid";
        else if (_npcPower <= 300f)
            _EFace.GetComponent<UISprite>().spriteName = "redHP_Low";

        if (_playerPower > 700f)
            _PFace.GetComponent<UISprite>().spriteName = "blueHP_Max";
        else if (_playerPower <= 700f && _playerPower > 300f)
            _PFace.GetComponent<UISprite>().spriteName = "blueHP_Mid";
        else if (_playerPower <= 300f)
            _PFace.GetComponent<UISprite>().spriteName = "blueHP_Low";
        if (_npcPower <= 0f) _status = Status.Clear;
        if (_playerPower <= 0f) _status = Status.Fail;
    }

    string GetJson()
    {
        string result;
        using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
        {            
            result = streamReader.ReadToEnd();
        }
        return result;
    }

    void DeserialJson(string json)
    {
        _obj = JSONSerializer.Deserialize<Rootobject>(json);
    }

    //문제초기화
    IEnumerator SetInit(int x)
    {
        testInt++;
        if(testInt == 3)
        {
            Debug.Log("stop");
        }
        //플레이모드가 아님 튕김
        if (_status != Status.Play) yield break;
        //x번 문제를 가져옴
        var _o = _obj.Levels.Where(a => a.id == x).ToList();
        //가져온 문제가 없으면 판정으로 빠짐
        if (_o.Count < 1)
        {
            _status = Status.Judge;
            yield break;
        }
        _dadyTalk.SetActive(false);
        //상황 설명이 먼저 나와야함
        _Desc.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 1f);
        _Desc.transform.FindChild("Label").GetComponent<UILabel>().text = _o[0].background;
        //SetFadeInAnswer();
        yield return new WaitForSeconds(2.0f);

        float _progress = (float)x / (float)_obj.Levels.Count();
        _num.GetComponent<UIProgressBar>().value = 1f - _progress;

        _talks = new List<GameObject>(); //가비지컬렉터가 알이서 메모리 해재해 주겠지....
        _yPos = 0;
        foreach (Chats _c in _o[0].chats)
        {   
            
            _npcpan1.transform.localPosition = new Vector3(45f, 0f, 0f);
            _npcpan2.transform.localPosition = new Vector3(45f, 0f, 0f);
            _npcpan3.transform.localPosition = new Vector3(45f, 0f, 0f);
            _attacker = _c.speaker;
            if (_c.speaker == "Npc1") _npcpan1.transform.localPosition = Vector3.zero;
            if (_c.speaker == "Npc2") _npcpan2.transform.localPosition = Vector3.zero;
            if (_c.speaker == "Npc3") _npcpan3.transform.localPosition = Vector3.zero;

            GameObject _oc = Instantiate((_c.speaker == "Npc1" || _c.speaker == "Npc2" || _c.speaker == "Npc3") ? _EnemyChat : _PlayerChat, Vector3.zero, Quaternion.identity) as GameObject;
            
            _talks.Add(_oc);//리스트에넣고

            _oc.transform.parent = _ChatPanel.transform; 
            _oc.transform.FindChild("Label").GetComponent<UILabel>().text = _c.word;
            _oc.transform.localScale = new Vector3(1f, 1f, 1f);
            _oc.transform.localPosition = new Vector3(0f, _yPos, 0f);
            
            //글자 싸이즈에 따라 여러 문제가 생김 따라서 한번에 만들어놓고 나중에 위치조정
            //그것을 위해 안보이게함
            _oc.GetComponent<UISprite>().color = new Color(1f,1f,1f,0f);
            Color labelNowColor = _oc.GetComponentInChildren<UILabel>().color;
            _oc.GetComponentInChildren<UILabel>().color = new Color(labelNowColor.r,labelNowColor.g,labelNowColor.b,0f);

            //if (_c.speaker == "Npc1" || _c.speaker == "Npc2" || _c.speaker == "Npc3") SoundManager.PlaySFX(SoundManager.Load("message_in"), false);
            //else SoundManager.PlaySFX(SoundManager.Load("message_sent"), false);
            //yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(0.1f);
        _yPos = 0;
        for(int i =0; i<_talks.Count; i++)
        {            
            if(0 == i)
            {
                _yPos -= _talks[i].GetComponent<UISprite>().height/2;
            }
            else
            {
                _yPos -= _talks[i-1].GetComponent<UISprite>().height / 2 + _talks[i].GetComponent<UISprite>().height/2 + 10f;
            }
            _talks[i].transform.localPosition = new Vector3(0f, _yPos, 0f);
            _talks[i].GetComponent<UISprite>().color = new Color(1f,1f,1f,1f);

            Color labelNowColor = _talks[i].GetComponentInChildren<UILabel>().color;
            _talks[i].GetComponentInChildren<UILabel>().color = new Color(labelNowColor.r,labelNowColor.g,labelNowColor.b,1f);
           
            yield return new WaitForSeconds(1.0f);
        }
        

        //_Awer1.SetActive(true);
        _Awer1.GetComponent<AnswerControl>().JSetActive(true);
        _Awer1.GetComponentInChildren<Behavior_Answer>().init(_o[0].answer[0].word, _o[0].answer[0].power, _o[0].answer[0].linkid, x, _o[0].answer[0].attacker);
        _Awer1.GetComponent<AnswerControl>().SetText(_o[0].answer[0].word);
        //_Awer1.transform.FindChild("Label").GetComponent<UILabel>().text = _o[0].answer[0].word;
        yield return new WaitForSeconds(0.5f);

        //_Awer2.SetActive(true);
        _Awer2.GetComponent<AnswerControl>().JSetActive(true);
        _Awer2.GetComponentInChildren<Behavior_Answer>().init(_o[0].answer[1].word, _o[0].answer[1].power, _o[0].answer[1].linkid, x, _o[0].answer[1].attacker);
        _Awer2.GetComponent<AnswerControl>().SetText(_o[0].answer[1].word);

        //_Awer2.GetComponent<Behavior_Answer>().init(_o[0].answer[1].word, _o[0].answer[1].power, _o[0].answer[1].linkid, x, _o[0].answer[1].attacker);
        //_Awer2.transform.FindChild("Label").GetComponent<UILabel>().text = _o[0].answer[1].word;
        yield return new WaitForSeconds(0.5f);

        //_Awer3.SetActive(true);
        _Awer3.GetComponent<AnswerControl>().JSetActive(true);
        _Awer3.GetComponentInChildren<Behavior_Answer>().init(_o[0].answer[2].word, _o[0].answer[2].power, _o[0].answer[2].linkid, x, _o[0].answer[2].attacker);
        _Awer3.GetComponent<AnswerControl>().SetText(_o[0].answer[2].word);

        //_Awer3.GetComponent<Behavior_Answer>().init(_o[0].answer[2].word, _o[0].answer[2].power, _o[0].answer[2].linkid, x, _o[0].answer[2].attacker);
        //_Awer3.transform.FindChild("Label").GetComponent<UILabel>().text = _o[0].answer[2].word;
        yield return new WaitForSeconds(0.5f);

        //_Awer4.SetActive(true);
        _Awer4.GetComponent<AnswerControl>().JSetActive(true);
        _Awer4.GetComponentInChildren<Behavior_Answer>().init(_o[0].answer[3].word, _o[0].answer[3].power, _o[0].answer[3].linkid, x, _o[0].answer[3].attacker);
        _Awer4.GetComponent<AnswerControl>().SetText(_o[0].answer[3].word);

        //_Awer4.GetComponent<Behavior_Answer>().init(_o[0].answer[3].word, _o[0].answer[3].power, _o[0].answer[3].linkid, x, _o[0].answer[3].attacker);
        //_Awer4.transform.FindChild("Label").GetComponent<UILabel>().text = _o[0].answer[3].word;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator SetAttack(string _msg, int _dmg, int _link, int _ori, string _remsg)
    {
        SetFadeOffAnswer();
        
        GameObject _oc = Instantiate(_PlayerChat, Vector3.zero, Quaternion.identity) as GameObject;
        _talks.Add(_oc);
        _oc.transform.parent = _ChatPanel.transform;
        _oc.transform.FindChild("Label").GetComponent<UILabel>().text = _msg;
        _oc.transform.localScale = new Vector3(1f, 1f, 1f);

        //역시 안보이게
        _oc.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 0f);
        Color labelNowColor = _oc.GetComponentInChildren<UILabel>().color;
        _oc.GetComponentInChildren<UILabel>().color = new Color(labelNowColor.r, labelNowColor.g, labelNowColor.b, 0f);
        
        yield return new WaitForSeconds(0.1f);//이시간후에 보이게
        
        //위치와 컬러지정
        _yPos -= _talks[2].GetComponent<UISprite>().height /2 + _oc.GetComponent<UISprite>().height/2 + 10;
        
        //보이게한다.
        _oc.transform.localPosition = new Vector3(0f,_yPos,0f);
        _oc.GetComponent<UISprite>().color = new Color(labelNowColor.r,labelNowColor.g,labelNowColor.b,1f);
        _oc.GetComponentInChildren<UILabel>().color = new Color(labelNowColor.r, labelNowColor.g, labelNowColor.b, 1f);

        //딜레이
        yield return new WaitForSeconds(0.5f);

        SoundManager.PlaySFX(SoundManager.Load("message_in"), false);
        GameObject _on = Instantiate(_EnemyChat, Vector3.zero, Quaternion.identity) as GameObject;
        _talks.Add(_on);
        _on.transform.parent = _ChatPanel.transform;
        _on.transform.FindChild("Label").GetComponent<UILabel>().text = _remsg;
        _on.transform.localScale = new Vector3(1f, 1f, 1f);
        //역시 안보이게
        _on.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 0f);
        labelNowColor = _on.GetComponentInChildren<UILabel>().color;
        _on.GetComponentInChildren<UILabel>().color = new Color(labelNowColor.r, labelNowColor.g, labelNowColor.b, 0f);
        yield return new WaitForSeconds(0.1f);
        //위치와 컬러지정
        _yPos -= _talks[3].GetComponent<UISprite>().height /2 + _on.GetComponent<UISprite>().height/2 +10;   
        _on.transform.localPosition = new Vector3(0f,_yPos, 0f);
        _on.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 1f);
        _on.GetComponentInChildren<UILabel>().color = new Color(labelNowColor.r, labelNowColor.g, labelNowColor.b, 1f);
        

        yield return new WaitForSeconds(1f);
        _dadyTalk.SetActive(true);
        string daddyContext = "[000000]";
        if (_dmg <= 0)//hit
        {
            StartCoroutine(SetFailStage(_dmg, _ori));
            int rnd = UnityEngine.Random.Range(0, 4);
            switch (rnd)
            {
                case 0:
                    daddyContext += "이런...";
                    break;
                case 1:
                    daddyContext += "조금만 더 신중히 \n생각해봐";
                    break;
                case 2:
                    daddyContext += "네 감정을 조금 \n더 정확히 표현해봐";
                    break;
                case 3:
                    daddyContext += "그건 아니지!!";
                    break;
                default:
                    daddyContext += "error";
                    break;
            }
            _dadyTalk.GetComponent<UILabel>().text = daddyContext;
        }
        else
        {
            StartCoroutine(SetSuccessStage(_dmg, _ori));
            int rnd = UnityEngine.Random.Range(0, 4);
            switch (rnd)
            {
                case 0:
                    daddyContext += "좋아 잘했어 역시 \n우리 " + ((_log._sex == 0)? "딸":"아들");
                    break;
                case 1:
                    daddyContext += "한방 먹였는걸?";
                    break;
                case 2:
                    daddyContext += "완벽해";
                    break;
                case 3:
                    daddyContext += "최고야";
                    break;
                default:
                    daddyContext += "error";
                    break;
            }
            _dadyTalk.GetComponent<UILabel>().text = daddyContext;
        }
        
        yield return new WaitForSeconds(3f);
        
        GameObject[] _obj = GameObject.FindGameObjectsWithTag("Chat");
        foreach (GameObject _o in _obj)
        {
            Destroy(_o);
        }
        _yPos = 62f;
        StartCoroutine(SetInit(_link));

        //todo : 호출자 삭제시 프로세스 중단 현상 수정
        //_Desc.SetActive(false);
        _Awer1.GetComponent<AnswerControl>().JSetActive(false);
        _Awer2.GetComponent<AnswerControl>().JSetActive(false);
        _Awer3.GetComponent<AnswerControl>().JSetActive(false);
        _Awer4.GetComponent<AnswerControl>().JSetActive(false);
    }

    void SetFadeInAnswer()
    {
        _Desc.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 1f);
        _Awer1.GetComponent<AnswerControl>().JSetActive(true);
        _Awer2.GetComponent<AnswerControl>().JSetActive(true);
        _Awer3.GetComponent<AnswerControl>().JSetActive(true);
        _Awer4.GetComponent<AnswerControl>().JSetActive(true);
    }
    
    void SetFadeOffAnswer()
    {
        _Desc.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 0f);
        _Awer1.GetComponent<AnswerControl>().JSetActive(false);
        _Awer2.GetComponent<AnswerControl>().JSetActive(false);
        _Awer3.GetComponent<AnswerControl>().JSetActive(false);
        _Awer4.GetComponent<AnswerControl>().JSetActive(false);
    }

    //공격실패
    IEnumerator SetFailStage(int _dmg, int _stage)
    {
        if (_log._maxCombo <= _Combo) _log._maxCombo = _Combo;
        _Combo = 0;
        SoundManager.PlaySFX(SoundManager.Load("attack_fail"), false);
        float _x = _HpE.GetComponent<UIProgressBar>().value;
        _playerPower += (_x > 0.3f) ? _dmg : _dmg * 2;
        _Player.GetComponent<UISprite>().spriteName = "dad2";
        _HpP.GetComponent<Behavior_SmoothBar>()._valAfter = _playerPower / 1000f;
        yield return new WaitForSeconds (0.5f);
        StartCoroutine(SetFailEffect(_stage));
        yield return new WaitForSeconds(1.5f);

        _Player.GetComponent<UISprite>().spriteName = "dad1";
    }

    IEnumerator SetFailEffect(int _s)
    {
        _awsheet.Add(new answerSheet { _id = _log._retryCnt, _qnum = _s, _score = 1 });
        if (_attacker == "Npc1")
            _npcpan1.transform.FindChild("E1").GetComponent<UISprite>().spriteName = "E1_2";
        else if (_attacker == "Npc2")
            _npcpan2.transform.FindChild("E2").GetComponent<UISprite>().spriteName = "E2_2";
        else if (_attacker == "Npc3")
            _npcpan3.transform.FindChild("E3").GetComponent<UISprite>().spriteName = "E3_2";
        yield return new WaitForSeconds(3f);
        _npcpan1.transform.FindChild("E1").GetComponent<UISprite>().spriteName = "E1_1";
        _npcpan2.transform.FindChild("E2").GetComponent<UISprite>().spriteName = "E2_1";
        _npcpan3.transform.FindChild("E3").GetComponent<UISprite>().spriteName = "E3_1";

    }

    //공격성공
    IEnumerator SetSuccessStage(int _dmg, int _stage)
    {
        _Combo++;
        if (_log._maxCombo <= _Combo) _log._maxCombo = _Combo;

        if (_Combo >= 2)
            _dmg = (int)((float)_dmg * 1.5f);
        float _x = _HpP.GetComponent<UIProgressBar>().value;

        float _Damage = (_x > 0.3f) ? _dmg : _dmg * 2f;
        _npcPower -= _Damage;
        StartCoroutine(SetEffect(_Damage, _stage));

        _Player.transform.FindChild("son").GetComponent<UISprite>().spriteName = (_log._sex == 1) ? "son2" : "daughter2";
        _HpE.GetComponent<Behavior_SmoothBar>()._valAfter = _npcPower / 1000f;
        
        yield return new WaitForSeconds(2f);
        _Player.transform.FindChild("son").GetComponent<UISprite>().spriteName = (_log._sex == 1) ? "son1" : "daughter1";
    }

    IEnumerator SetEffect(float _x, int _s)
    {
        GameObject _hitEffect = null;
        if (_x > 100f){
            _hitEffect = _critical;
            if (_Combo >= 2) _hitEffect = _combo;
            _awsheet.Add(new answerSheet { _id = _log._retryCnt, _qnum = _s, _score = 3 });
            SoundManager.PlaySFX(SoundManager.Load("attack_critical"), false);
        }
        else if (_Combo >= 2)
        {
            _hitEffect = _combo;
            _awsheet.Add(new answerSheet { _id = _log._retryCnt, _qnum = _s, _score = 2 });
            SoundManager.PlaySFX(SoundManager.Load("attack_hit"), false);
        }
        else
        {
            _hitEffect = _hit;
            _awsheet.Add(new answerSheet { _id = _log._retryCnt, _qnum = _s, _score = 2 });
            SoundManager.PlaySFX(SoundManager.Load("attack_hit"), false);
        }

        _hitEffect.SetActive(true);
        _hitEffect.GetComponent<Behavior_hit>().init();

        if (_attacker == "Npc1")
        {
            _npcpan1.transform.FindChild("E1").GetComponent<UISprite>().spriteName = "E1_3";
            _npcpan1.transform.FindChild("Hit").gameObject.SetActive(true);
        }
        else if (_attacker == "Npc2")
        {
            _npcpan2.transform.FindChild("E2").GetComponent<UISprite>().spriteName = "E2_3";
            _npcpan2.transform.FindChild("Hit").gameObject.SetActive(true);
        }
        else if (_attacker == "Npc3")
        {
            _npcpan3.transform.FindChild("E3").GetComponent<UISprite>().spriteName = "E3_3";
            _npcpan3.transform.FindChild("Hit").gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(3f);
        _npcpan1.transform.FindChild("E1").GetComponent<UISprite>().spriteName = "E1_1";
        _npcpan2.transform.FindChild("E2").GetComponent<UISprite>().spriteName = "E2_1";
        _npcpan3.transform.FindChild("E3").GetComponent<UISprite>().spriteName = "E3_1";
        _npcpan1.transform.FindChild("Hit").gameObject.SetActive(false);
        _npcpan2.transform.FindChild("Hit").gameObject.SetActive(false);
        _npcpan3.transform.FindChild("Hit").gameObject.SetActive(false);
    }

    void GetRemainTime()
    {
        _time -= Time.deltaTime;
        TimeSpan ts = TimeSpan.FromSeconds(_time);
        if (_time >= 60f)
        {
            _emergency.SetActive(false);
            _Timer.transform.FindChild("Label").GetComponent<UILabel>().text = string.Format("{0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
        }
        else
        {
            _emergency.SetActive(true);
            _Timer.transform.FindChild("Label").GetComponent<UILabel>().text = string.Format("{0:D2}.{1:D3}", ts.Seconds, ts.Milliseconds);
        }
        if (_time <= 0f && _status == Status.Play)
        {
            _Timer.transform.FindChild("Label").GetComponent<UILabel>().text = "00:00";
            _log._timeOutCnt++;
            _status = Status.TimeOver;
        }
        /*
        if (_time <= 580f && _status == Status.Play)
        {
            _log._timeOutCnt++;
            _status = Status.TimeOver;
        }
         */ 
    }

    void SetResult()
    {
        Status _Staus = _status;
        _status = Status.End;

        _ptime.Add(new playTime { _id = _log._retryCnt, _sec = (int)(Time.time - _startTime)});
        _log._playTimeSec = _ptime;
        _log._id = DateTime.Now.ToString("yyyyMMddHHmmss");
        _log._answerSheet = _awsheet;
        string _s = JSONSerializer.Serialize<Log>(_log);


        _fingame.transform.FindChild("Label").GetComponent<UILabel>().text = (GetWin(_Staus)) ? "YOU Win" : "You lose";
        _fingame.SetActive(true);

        GameObject[] _obj = GameObject.FindGameObjectsWithTag("Chat");
        foreach (GameObject _o in _obj)
        {
            Destroy(_o);
        }
        _yPos = 62f;
        //todo : 호출자 삭제시 프로세스 중단 현상 수정
        _Desc.SetActive(false);
        _Awer1.SetActive(false);
        _Awer2.SetActive(false);
        _Awer3.SetActive(false);
        _Awer4.SetActive(false);
        StartCoroutine(SetActResultWin(_Staus));
    }

    IEnumerator SetActResultWin(Status _s)
    {
        yield return new WaitForSeconds(2.5f);
        _fingame.SetActive(false);
        _ResultWin.SetActive(true);
        string _str = "C";
        if (GetWin(_s) == true && _s == Status.Clear) _str = "A";
        if (GetWin(_s) == true && _s == Status.Judge) _str = "B";

        if (_str == "A") _ResultWin.transform.FindChild("MedalWin").FindChild("BtnRetry").gameObject.SetActive(false);
        _ResultWin.transform.FindChild("RankWin").FindChild("RankBG").FindChild("Radius").FindChild("Label").GetComponent<UILabel>().text = _str;
        _MedalWin.transform.FindChild("Medal1").GetComponent<UISprite>().spriteName = (_log._maxCombo >= 2) ? "emblem1" : "emblem1-";
        _MedalWin.transform.FindChild("Medal2").GetComponent<UISprite>().spriteName = (_log._maxCombo >= 3) ? "emblem2" : "emblem2-";
        _MedalWin.transform.FindChild("Medal3").GetComponent<UISprite>().spriteName = (_log._maxCombo >= 5) ? "emblem3" : "emblem3-";
        _MedalWin.transform.FindChild("Medal4").GetComponent<UISprite>().spriteName = (_str == "A") ? "emblem4" : "emblem4-";
        int _correctCnt = _log._answerSheet.Where(_a => (_a._score == 2 || _a._score == 3) && (_a._id == _log._retryCnt)).Count();
        int _totalExamCnt = _log._answerSheet.Where(_a => _a._id == _log._retryCnt).Count();
        float _rate = (float)_correctCnt / (float)_totalExamCnt;
        //Debug.Log(JSONSerializer.Serialize<Log>(_log));
        _MedalWin.transform.FindChild("Medal5").GetComponent<UISprite>().spriteName = (_rate >= 0.5f) ? "emblem5" : "emblem5-";
        _MedalWin.transform.FindChild("Medal6").GetComponent<UISprite>().spriteName = (_rate >= 0.7f) ? "emblem6" : "emblem6-";
        _MedalWin.transform.FindChild("Medal7").GetComponent<UISprite>().spriteName = (_rate >= 0.9f) ? "emblem7" : "emblem7-";
        _MedalWin.transform.FindChild("Medal8").GetComponent<UISprite>().spriteName = (_log._retryCnt > 0) ? "emblem8" : "emblem8-";
        _MedalWin.transform.FindChild("Radius").FindChild("Label").GetComponent<UILabel>().text = string.Format("{0:F0}%", _rate * 100f);

        if (_str == "A")
            SoundManager.PlaySFX(SoundManager.Load("resultA"), false);
        if (_str == "B")
            SoundManager.PlaySFX(SoundManager.Load("resultB"), false);
        if (_str == "C")
            SoundManager.PlaySFX(SoundManager.Load("resultC"), false);
    }

    bool GetWin(Status _Status)
    {
        bool _isWin = false;
        if ((_Status == Status.Clear) ||
        (_Status == Status.Judge && _playerPower > _npcPower)) _isWin = true;

        SoundManager.MuteMusic();

        if (_isWin == true)
            SoundManager.PlaySFX(SoundManager.Load("youwin"), false);
        else
            SoundManager.PlaySFX(SoundManager.Load("youlose"), false);

        return _isWin;
    }

    public void Retry()
    {
        _npcPower = 1000f;
        _playerPower = 1000f;
        _HpE.GetComponent<UIProgressBar>().value = 1f;
        _HpP.GetComponent<UIProgressBar>().value = 1f;
        _log._retryCnt = _log._retryCnt + 1;
        _Level = 1;
        _time = _limitTime;
        _startTime = Time.time;
        _status = Status.Play;
        //_yPos = 0f;
        StartCoroutine(SetInit(_Level));
        if (SoundManager.IsMusicMuted() == true) SoundManager.MuteMusic();
    }

}
