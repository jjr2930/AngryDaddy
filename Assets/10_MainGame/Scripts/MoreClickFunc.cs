using UnityEngine;
using System.Collections;

public class MoreClickFunc : MonoBehaviour {
    public GameObject _morePanel;
	// Use this for initialization
	void Start () {
	
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
