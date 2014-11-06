using UnityEngine;
using System.Collections;

public class Behavior_Hover : MonoBehaviour {

    float _y = 0f;

    void Start()
    {
        if (transform.parent.name == "Credit") _y = -90;
        if (transform.parent.name == "Exit") _y = -90;
    }

	void OnHover(bool _isOver)
    {
        if (_isOver)
            SoundManager.PlaySFX(SoundManager.Load("menu slide"), false);
            iTween.MoveTo(transform.parent.gameObject, iTween.Hash("position", new Vector3(0f, _y, 0f), "islocal", true, "time", 1));
        if (_isOver == false)
            iTween.MoveTo(transform.parent.gameObject, iTween.Hash("position", new Vector3(85f, _y, 0f), "islocal", true, "time", 1));

    }

    void OnClick()
    {
        SoundManager.PlaySFX(SoundManager.Load("click"), false);
        if (transform.parent.name == "Start") Application.LoadLevel("02_Infomation");
        //if (transform.parent.name == "Credit") Application.LoadLevel("01_Creadit");
        if (transform.parent.name == "Exit") Application.LoadLevel("01_Creadit");
    }
}
