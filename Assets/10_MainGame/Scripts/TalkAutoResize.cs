using UnityEngine;
using System.Collections;

public class TalkAutoResize : MonoBehaviour {
    private UISprite _sprite;
    private UILabel _label;
	// Use this for initialization
	void Start () {
        _sprite = GetComponent<UISprite>();
        _label  = GetComponentInChildren<UILabel>();
        _sprite.height = _label.height;
	}
	
	// Update is called once per frame
	void Update () {
        _sprite.height = _label.height;
	}
}
