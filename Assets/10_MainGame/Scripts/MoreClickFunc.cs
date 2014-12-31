using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class MoreClickFunc : MonoBehaviour {
    public string _rank { set;get;}
    public GameObject _morePanel;
    public UILabel _label;
    Description _description;
    // Use this for initialization
    void Awake()
    {
        string filePath = "";
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "ShortDescription.json");
        string result;
        using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
        {
            result = streamReader.ReadToEnd();
        }
        _description = JSONSerializer.Deserialize<Description>(result);
    }
	void Start () {
	    _label = transform.FindChild("Text").GetComponent<UILabel>();
	}
    public void SetText(string value)
    {
        _rank = value;       
        switch(_rank)
        {
            case "A":
                _label.text = _description.A;
                break;
            case "B":
                _label.text = _description.B;
                break;
            case "C":
                _label.text = _description.C;
                break;
        }
    }
	void OnEnable()
    {
      
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
