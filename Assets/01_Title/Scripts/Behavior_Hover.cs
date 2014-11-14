using UnityEngine;
using System.Collections;

public class Behavior_Hover : MonoBehaviour {

    float _y = 0f;
    Vector3 _originVector;
    JTwincleUI _twincle;
    void Start()
    {
        if (transform.parent.name == "Credit") _y = -90;
        if (transform.parent.name == "Exit") _y = -90;
        _originVector = this.transform.parent.localPosition;
    }

	void OnHover(bool _isOver)
    {
        if (_isOver)
        {
            SoundManager.PlaySFX(SoundManager.Load("MouseOver"), false);
            _twincle =GetComponent<JTwincleUI>();
            _twincle.Stop = true;
            iTween.MoveTo(transform.parent.gameObject, iTween.Hash("position", new Vector3(_originVector.x-30, _originVector.y, 0f), "islocal", true, "time", 1));
        }
        if (_isOver == false)
        {
            _twincle.StartTwicle();
            iTween.MoveTo(transform.parent.gameObject, iTween.Hash("position", new Vector3(_originVector.x, _originVector.y, 0f), "islocal", true, "time", 1));
        }

    }

    void OnClick()
    {
        SoundManager.PlaySFX(SoundManager.Load("ButtonClick"));
        if (Application.loadedLevelName == "00_Title")
        { 
            //SoundManager.PlaySFX(SoundManager.Load("click"), false);
            if (transform.parent.name == "Start") Application.LoadLevel("02_Infomation");
            //if (transform.parent.name == "Credit") Application.LoadLevel("01_Creadit");
            if (transform.parent.name == "Exit") Application.LoadLevel("01_Creadit");
        }
        else if(Application.loadedLevelName == "03_Intro")
        {
            //SoundManager.PlaySFX(SoundManager.Load("ButtonOver"), false);
            PlayerPrefs.SetInt("retry",0);
            if (transform.parent.name == "Start") Application.LoadLevel("10_MainGame");
        }
    }
}
