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
        //this.renderer.material.color = new Color(1.0f,1.0f,1.0f,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if(null != _sprite )
            _sprite.height = _label.height;
	}
}
