using UnityEngine;
using System.Collections;

public class JTwincleUI : MonoBehaviour {
    private UIWidget _uiObj;
    public float _twincleTime = 0.5f;

    public bool Stop { get; set; }
    void Start()
    {
        
    }
    public void StartTwicle()
    {
        Stop = false;
        StartCoroutine(Twicnle());
    }
    IEnumerator Twicnle()
    {
        Color nowColor;
        for (; ; )
        {
            if (Stop)
                yield break;
            else
            {
                nowColor = GetComponent<UIWidget>().color;
                nowColor.a = 0;
                GetComponent<UIWidget>().color = nowColor;
                yield return new WaitForSeconds(_twincleTime);

                nowColor.a = 1;
                GetComponent<UIWidget>().color = nowColor;
                yield return new WaitForSeconds(_twincleTime);
            }
        }
    }
}
