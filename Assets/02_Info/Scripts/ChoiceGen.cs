using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChoiceGen : MonoBehaviour {

    int ClickCnt = 0;

    void OnClick()
    {
        
        if (ClickCnt > 0) return;

        SoundManager.PlaySFX(SoundManager.Load("ButtonClick"), false);
        Transform _obj = GameObject.Find("UI Root").transform.FindChild("Camera").transform.FindChild("Container").transform.FindChild("BirthLabel").transform.FindChild("InputBox").transform.FindChild("BirthLabel");
        int _birthyear = int.Parse(_obj.GetComponent<UILabel>().text);
        int _gen = (transform.name == "Male") ? 1 : 0;

//        List<answer> _a = new List<answer>();
//        answerSheet _as = new answerSheet { _id = 0, _answers = _a };

        Log _l = new Log { _id = "", _sex = _gen, _year = _birthyear, _answerSheet = new List<answerSheet>(), _maxCombo = 0, _playTimeSec = new List<playTime>(), _retryCnt = 0, _timeOutCnt = 0 };
        string _s = JSONSerializer.Serialize<Log>(_l);
        PlayerPrefs.SetString("gamelog", _s);
        PlayerPrefs.Save();

        Application.LoadLevelAsync("03_Intro");
    }
}
