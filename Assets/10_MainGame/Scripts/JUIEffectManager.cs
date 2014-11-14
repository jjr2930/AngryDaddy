using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JUIEffectManager : MonoBehaviour {

    /// <summary>
    /// 반짝이게 하고싶은 UI를 선택하면 알아서 빤짝여준다.
    /// </summary>
    /// <param name="obj"></param>
    public static void MakeTwicle(GameObject obj)
    {
        JTwincleUI twincle = obj.AddComponent<JTwincleUI>();
        twincle.StartTwicle();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
