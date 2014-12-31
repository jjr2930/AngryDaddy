using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Behavior_Intro : MonoBehaviour {
    public GameObject _start;
    //public GameObject _bold;
    public GameObject[] _cuts;
    Vector3[] _v = new Vector3[] { new Vector3(-500f, 0f, 0f), new Vector3(0f, 500f, 0f), new Vector3(0f, -420f, 0f), new Vector3(550f, -550f, 0f) };
    int[] _MovingScene = new int[] { 0, 2, 5, 8};
    Log _log;

    // Use this for initialization
	IEnumerator Start () {
        string _s = PlayerPrefs.GetString("gamelog");
        _log = JSONSerializer.Deserialize<Log>(_s);
        SetChangeSprite(_log._sex);
        SetDeActive();
        yield return StartCoroutine( SetActive());
        //Application.LoadLevelAsync("10_MainGame");
	}

    void SetChangeSprite(int _g)
    {
        if (_g == 0) return;
        _cuts[0].GetComponent<UISprite>().spriteName = "intro1-son";
        _cuts[2].GetComponent<UISprite>().spriteName = "intro3-son";
        _cuts[3].GetComponent<UISprite>().spriteName = "intro4-son";
        _cuts[6].GetComponent<UISprite>().spriteName = "intro7-son";
    }

    void SetDeActive()
    {
        foreach(GameObject _o in _cuts)
        {
            _o.SetActive(false);
        }
    }

    IEnumerator SetActive()
    {
        int _idx = 0;
        int _Vecidx = 0;
        foreach (GameObject _o in _cuts)
        {
            if (System.Array.IndexOf(_MovingScene, _idx) > -1)
            {
                _o.SetActive(true);
                iTween.MoveFrom(_o.transform.parent.gameObject, _v[_Vecidx], 0.5f);
                _Vecidx++;
            }
            else
            {
                _o.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
            _idx++;
        }
        yield return new WaitForSeconds(1.0f);
        _start.SetActive(true);
        //GameObject.Find("SpriteBold").GetComponent<Behavior_StartBold>().OnOff = true;
        yield return null;
        iTween.MoveFrom(_start, iTween.Hash("x", _start.transform.localPosition.x + 350f, "islocal", true, "time", 1f,
                                                "easetype", iTween.EaseType.easeOutBounce,
                                                "delay", 0.5f));

        
    }

	// Update is called once per frame
	void Update () {
	
	}
}
