using UnityEngine;
using System.Collections;

public class Behavior_hit : MonoBehaviour {

	// Use this for initialization
	public void init() {
        GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 1f);
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 2.5f, "oncomplete", "die"));
        
	}

    void die()
    {
        gameObject.SetActive(false);
    }
}
