using UnityEngine;
using System.Collections;

public class Behavior_Init : MonoBehaviour {

    public GameObject _TitleBG;
    public GameObject _Title;
    public GameObject _Menu1;
    public GameObject _Menu2;
    public GameObject _Menu3;

    //public  GameObject _bold;
	// Use this for initialization
	IEnumerator Start () {
        if (SoundManager.IsMusicMuted() == true) SoundManager.MuteMusic();
        SoundManager.SetVolumeMusic(0.5f);
        SoundManager.SetVolumeSFX(1f);
        iTween.MoveFrom(_Menu1, iTween.Hash("x", _Menu1.transform.localPosition.x + 350f, "islocal", true, "time", 1f, "easetype", iTween.EaseType.easeOutBounce, "delay", 0.5f));
        //iTween.MoveFrom(_Menu2, iTween.Hash("x", 350f, "islocal", true, "time", 1f, "easetype", iTween.EaseType.easeOutBounce, "delay", 1f));
        iTween.MoveFrom(_Menu3, iTween.Hash("x", _Menu3.transform.localPosition.x + 350f, "islocal", true, "time", 1f, "easetype", iTween.EaseType.easeOutBounce, "delay", 1f));
        yield return new WaitForSeconds(0.5f);
        _Menu1.SetActive(true);
        //JUIEffectManager.MakeTwicle(_Menu1.transform.FindChild("Sprite").gameObject);
        //_Menu2.SetActive(true);
        _Menu3.SetActive(true);
        _TitleBG.SetActive(true);
        yield return new WaitForSeconds(2f);
        _Title.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
 
}
