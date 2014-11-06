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
        Application.LoadLevelAsync("00_Title");
    }

}
