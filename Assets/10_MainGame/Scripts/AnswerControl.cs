using UnityEngine;
using System.Collections;

public class AnswerControl : MonoBehaviour {
    public GameObject _preAnswer;

    private UISprite _answerBack;
    private UILabel _answerLabel;

    private UISprite _numBack;
    private UILabel  _numLabel;

	// Use this for initialization
	void Start () {
	    _answerLabel = transform.FindChild("Text").GetComponent<UILabel>();
        _answerBack = _answerLabel.GetComponentInChildren<UISprite>();

        _numBack = transform.FindChild("Num").GetComponent<UISprite>();
        _numLabel = _numBack.transform.FindChild("Label").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
        if(null != _preAnswer)
	        AutoPos();
	}
    public void SetText(string text)
    {
        _answerLabel.text = text;
    }
    public void AutoPos()
    {
        int preHeight = _preAnswer.GetComponent<AnswerControl>().GetHeight();
        this.transform.localPosition = _preAnswer.transform.localPosition - new Vector3(0, preHeight + 50, 0);
    }
    public int GetHeight()
    {
        int height = 0;
        height = _answerLabel.height;

        return height;
    }
    public void JSetActive(bool value)
    {
        Debug.Log("call Active func");
        if(value)
        {
            Color originColor = _answerLabel.color;
            _answerBack.color   = new Color(1,1,1,1);
            _answerLabel.color  = new Color(originColor.r,originColor.g,originColor.b,1);

            originColor = _numLabel.color;
            _numBack.color      = new Color(1,1,1,1);
            _numLabel.color     = new Color(originColor.r,originColor.g,originColor.b,1);
        }
        else if(!value)
        {
            Color originColor = _answerLabel.color;
            _answerBack.color = new Color(1, 1, 1, 0);
            _answerLabel.color = new Color(originColor.r, originColor.g, originColor.b, 0);

            originColor = _numLabel.color;
            _numBack.color = new Color(1, 1, 1, 0);
            _numLabel.color = new Color(originColor.r, originColor.g, originColor.b, 0);
        }
    }
}
