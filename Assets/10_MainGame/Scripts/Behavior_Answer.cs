using UnityEngine;
using System.Collections;

public class Behavior_Answer : MonoBehaviour {

    public int _ClickCnt{get;set;}

    string _word, _attacker;
    int _damage, _linkid, _oriid;

	// Use this for initialization
	public void init (string _w, int _dmg, int _link, int _ori, string _atk) {
        _word = _w;
        _damage = _dmg;
        _linkid = _link;
        _oriid = _ori;
        _attacker = _atk;
	}
	
    public void OnClick() {
        if (_ClickCnt > 0) return;
        _ClickCnt++;
        SoundManager.PlaySFX(SoundManager.Load("attack"), false);
        StartCoroutine(GameObject.FindObjectOfType<Maingame>().SetAttack(_word, _damage, _linkid, _oriid, _attacker));
	}

    public void OnHover(bool _isOver)
    {
        if (_isOver) SoundManager.PlaySFX(SoundManager.Load("menu slide"), false);
    }

    void OnEnable()
    {
        _ClickCnt = 0;
    }
}
