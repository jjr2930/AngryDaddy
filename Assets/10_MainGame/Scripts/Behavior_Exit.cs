using UnityEngine;
using System.Collections;

public class Behavior_Exit : MonoBehaviour {
    int _ClickCnt = 0;

    // Use this for initialization
    void OnEnable()
    {
        _ClickCnt = 0;
    }

    // Update is called once per frame
    void OnClick()
    {
        if (_ClickCnt > 0) return;
        SoundManager.PlaySFX(SoundManager.Load("ButtonClick"));
        Application.LoadLevelAsync("00_Title");
    }
    void OnHover()
    {
        if (_ClickCnt > 0) return;
        SoundManager.PlaySFX(SoundManager.Load("MouseOver"));
    }

}
