using UnityEngine;
using System.Collections;

public class Behavior_Credit : MonoBehaviour {

    string[] _creditTxt = new string[] {"제작총괄:윤형섭", "기획:Jake Ahn", "디자인:김주희", "개발:민신현", "2014", "Thanx" };

	// Use this for initialization
	IEnumerator Start () {

        for (int i = 0; i < _creditTxt.Length; i++)
        {
            for (int y = 0; y <= _creditTxt[i].Length; y++)
            {
                GetComponent<UILabel>().text = _creditTxt[i].Substring(0, y);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1.5f);
        }

        Application.Quit();
	}
	
}
