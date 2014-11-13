using UnityEngine;
using System.Collections;

public class Behaviour_Medal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTooltip(bool show)
    {
        if (show == true)
        {
            string text = "[000000]";
            switch (this.gameObject.name)
            {
                case "Medal1":
                    text += "최대 콤보가 2이상일 경우 얻는다.";
                    break;
                case "Medal2":
                    text += "최대 콤보가 3이상일 경우 얻는다.";
                    break;
                case "Medal3":
                    text += "최대 콤보가 5이상일 경우 얻는다.";
                    break;
                case "Medal4":
                    text += "랭크가 'A'일 경우 얻는다.";
                    break;
                case "Medal5":
                    text += "정답률이 50% 이상일 경우 얻는다.";
                    break;
                case "Medal6":
                    text += "정답률이 60% 이상일 경우 얻는다.";
                    break;
                case "Medal7":
                    text += "정답률이 70% 이상일 경우 얻는다.";
                    break;
                case "Medal8":
                    text += "게임을 실패 후 다시하게 되면 얻는다.";
                    break;
            }
            UITooltip.ShowText(text);
        }
        else
        {
            UITooltip.
        }
    }
}
