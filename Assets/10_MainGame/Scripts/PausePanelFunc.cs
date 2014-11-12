using UnityEngine;
using System.Collections;

public class PausePanelFunc : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Replay()
    {
        GameObject.Find("Pause").GetComponent<Behavior_Pause>().SetPauseButton();
        Time.timeScale = 1.0f;

        GameObject.Find("PausePanel").SetActive(false);
    }
    public void Exit()
    {
        Application.LoadLevel("00_Title");
    }

}
