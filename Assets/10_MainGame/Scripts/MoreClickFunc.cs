using UnityEngine;
using System.Collections;

public class MoreClickFunc : MonoBehaviour {
    public string _rank { set;get;}
    public GameObject _morePanel;
	// Use this for initialization
	void Start () {
	
	}
	void OnEnable()
    {
       switch(_rank)
       {
           case "A":
               _morePanel.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "5_more_2";
               break;
           case "B":
               _morePanel.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "5_more_3";
               break;
           case "C":
               _morePanel.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "5_more_4";
               break;
       }
       //set message in here
        //blah blah
    }
	// Update is called once per frame
	void Update () {
	
	}
    public void ShowMorePanel()
    {
        _morePanel.SetActive(true);
       
        
    }
    public void CloseMorePanel()
    {
        _morePanel.SetActive(false);
    }
}
