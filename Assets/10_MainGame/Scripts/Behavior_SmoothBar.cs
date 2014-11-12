using UnityEngine;
using System.Collections;

public class Behavior_SmoothBar : MonoBehaviour {

    public float _valBefore {set; get;}
    public float _valAfter{set;get;}
    bool _isLock = false;
    bool _isDanger = false;

    float _TimeLast = 0f;
    float _TimeTerm = 0.5f;
    //정열이가 만듬여
    public float _delayTime;
	// Use this for initialization
    bool _isStarStart;
    void Start()
    {
        _isStarStart = false;
        _valBefore = 1f;
        _valAfter = 1f;        
    }

    void FixedUpdate()
    {
        if (_isLock == true) return;
        //_valAfter = GetComponent<UIProgressBar>().value;
        if (_valAfter < _valBefore) StartCoroutine( SetAni());
        if (_isDanger == true) SetBlankBar();
        //if (_isDanger == true && (_TimeLast + _TimeTerm < Time.time)) ShootingStar();
    }

    void ShootingStar()
    {
        
        _TimeLast = Time.time;
        GameObject _obj = NGUITools.AddChild(NGUITools.FindCameraForLayer(8).transform.FindChild("BG").gameObject, Resources.Load("Star") as GameObject);
        _obj.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        int randomX = Random.Range(-Screen.width, Screen.width);
        int randomY = Random.Range(0, Screen.height);

        Debug.Log("random x:" + randomX + "random y:" + randomY);

        _obj.transform.localPosition = new Vector3(randomX,randomY, -1f);
        //GameObject _star = Instantiate(Resources.Load("Star"), new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        //_obj.transform.parent = NGUITools.FindCameraForLayer(8).transform.FindChild("BG").transform;
        //_obj.transform.position = new Vector3(Random.Range(-600f, 600f), 100f, 0f);
        //_obj.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));        
    }
    
    IEnumerator SetBackAni(float delayTime)
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SetAni());        
    }
    
    IEnumerator SetAni()
    {
        _isLock = true;
        
        //흔들기
        if(!this.name.Contains("Effect"))
            iTween.ShakePosition(this.gameObject,new Vector3(0.1f,0.1f,0f), 0.2f);
        GetComponent<UIProgressBar>().value = _valBefore;
        while (_valAfter < GetComponent<UIProgressBar>().value)
        {
            GetComponent<UIProgressBar>().value -= 0.001f;
            yield return new WaitForSeconds(0.01f);
        }
        GetComponent<UIProgressBar>().value = _valAfter;
        _valBefore = _valAfter;
        if(!this.name.Contains("Effect"))//effect바가 움직이게 해주자.
        {
            transform.FindChild("Red_Effect").GetComponent<Behavior_SmoothBar>()._valAfter = _valBefore;
        }
        if (transform.name == "Blue" && _valBefore <= 0.3f) _isDanger = true;
        _isLock = false;
    }

    void SetBlankBar()
    {
        if (GetComponent<TweenAlpha>().enabled == false)
        { 
            GetComponent<TweenAlpha>().enabled = true;
            if (!_isStarStart)
            {
                _isStarStart = true;
                StartCoroutine("ShowStar");
            }
        }
    }
    
    //j
    IEnumerator ShowStar()
    {
        while(true)
        {
            ShootingStar();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
