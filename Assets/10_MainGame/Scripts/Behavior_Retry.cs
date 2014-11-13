using UnityEngine;
using System.Collections;

public class Behavior_Retry : MonoBehaviour {

    int _ClickCnt = 0;

	// Use this for initialization
    void OnEnable()
    {
        _ClickCnt = 0;
	}
	
	// Update is called once per frame
	void OnClick () {
        if (_ClickCnt > 0) return;
        SoundManager.PlaySFX(SoundManager.Load("ButtonClick"));
        NGUITools.FindCameraForLayer(gameObject.layer).GetComponent<Maingame>().Retry();
        NGUITools.FindCameraForLayer(gameObject.layer).GetComponent<Maingame>()._ResultWin.SetActive(false);
	}
    void OnHover()
    {
        if(_ClickCnt >0) return;
        SoundManager.PlaySFX(SoundManager.Load("MouseOver"));
    }
}
