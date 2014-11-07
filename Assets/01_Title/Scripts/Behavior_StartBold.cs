using UnityEngine;
using System.Collections;

public class Behavior_StartBold : MonoBehaviour {
    public bool OnOff { set;get;}
	// Use this for initialization
	void Start () {
	    OnOff = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void StartAnim()
    {
        StartCoroutine(AnimOnOff());
    }
    IEnumerator AnimOnOff()
    {        
        for (; ; )
        {
            if (OnOff)
            {
                GetComponent<UISprite>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                yield return new WaitForSeconds(0.2f);
                GetComponent<UISprite>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                GetComponent<UISprite>().color = new Color(1.0f,1.0f,1.0f, 0.0f);
                yield return null;
            }
        }
    }
}
