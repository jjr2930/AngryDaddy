using UnityEngine;
using System.Collections;

public class AutoPosition : MonoBehaviour {
    public float _fAutoSetPosTime;
    private UISprite[] _talks;
    private float _yPos;
	// Use this for initialization
	void Start () {
        _talks = GetComponentsInChildren<UISprite>();
        _yPos = 0;
       
	}
	void FixedUpdate()
    {
        _talks = GetComponentsInChildren<UISprite>();
        if (null == _talks)
        {
            return;
        }
        else
        {
            _yPos = 0;
            for (int i = 1; i < _talks.Length; i++)
            {
                _yPos -= _talks[i - 1].GetComponentInChildren<UILabel>().height;

                _talks[i].transform.localPosition = new Vector3(0, _yPos, 0);
            }
            return;
        }
    }
}
