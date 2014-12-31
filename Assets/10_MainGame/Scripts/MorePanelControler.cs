using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class Description
{
    public string A;
    public string B;
    public string C;
}
public class MorePanelControler : MonoBehaviour {
    private GameObject _moreButton;
    private UISprite _rank;
    private UILabel _label;
    private Description _description;
    void Awake()
    {
        _moreButton = GameObject.Find("MoreBack");
        _rank = this.transform.FindChild("Rank").GetComponent<UISprite>();
        _label = this.transform.FindChild("Text").GetComponent<UILabel>();
        string filePath ="";
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "description.json");
        string result;
        using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
        {
            result = streamReader.ReadToEnd();
        }
        _description = JSONSerializer.Deserialize<Description>(result);
    }
	// Use this for initialization
	void Start () {
	    _moreButton = GameObject.Find("MoreBack");
        _label.text = "[000000]";
        switch(_moreButton.GetComponent<MoreClickFunc>()._rank)
        {
            case "A":
                _rank.GetComponent<UISprite>().spriteName = "5_more_2";
                _label.text += _description.A;
                break;
            case "B":
                _rank.GetComponent<UISprite>().spriteName = "5_more_3";
                _label.text += _description.B;
                break;
            case "C":
                _rank.GetComponent<UISprite>().spriteName = "5_more_4";
                _label.text += _description.C;
                break;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        	
	}
}
