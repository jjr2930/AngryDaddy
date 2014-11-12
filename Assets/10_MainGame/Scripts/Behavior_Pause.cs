using UnityEngine;
using System.Collections;

public enum PauseState {
    Pause,
    Play,
}
public class Behavior_Pause : MonoBehaviour {
    public GameObject _pausePanel;
    private PauseState _state { set;get;}
	// Use this for initialization
	void Start () {
	    _state = PauseState.Pause;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnHover()
    {
        SoundManager.PlaySFX(SoundManager.Load("MouseOver"));
    }
    void OnClick()
    {
        SoundManager.PlaySFX(SoundManager.Load("ButtonClick"));
        if(PauseState.Play == _state)//플래이면 바꿔준다.
        {
            SetPauseButton();
            Time.timeScale = 1.0f;
            _pausePanel.SetActive(false);
        }
        else if(PauseState.Pause == _state)
        {
            SetPlayButton();
            Time.timeScale = 0.0f;
            _pausePanel.SetActive(true);
        }
    }
    /// <summary>
    /// 버튼을 플레이 모양으로 바꾼다. 옵션들도 그에 맞게
    /// </summary>
    public void SetPlayButton()
    {
        GetComponent<UISprite>().spriteName = "Btn_Play_1";
        GetComponent<UIButton>().normalSprite = "Btn_Play_1";
        GetComponent<UIButton>().pressedSprite = "Btn_Play_2";
        _state = PauseState.Play;
    }
    /// <summary>
    /// 버튼을 일시정지 모양으로 바꾼다. 옵션들도 그에 맞게
    /// </summary>
    public void SetPauseButton()
    {
        GetComponent<UISprite>().spriteName = "Btn_Pause_1";
        GetComponent<UIButton>().normalSprite = "Btn_Pause_1";
        GetComponent<UIButton>().pressedSprite = "Btn_Pause_2";
        _state = PauseState.Pause;        
    }
}
