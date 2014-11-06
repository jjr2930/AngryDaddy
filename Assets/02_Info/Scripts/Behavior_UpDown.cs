using UnityEngine;
using System.Collections;

public class Behavior_UpDown : MonoBehaviour {

    UILabel _label = null;

    IEnumerator Start()
    {
        while (_label == null)
        {
            _label = transform.parent.FindChild("InputBox").FindChild("BirthLabel").GetComponent<UILabel>();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnClick()
    {
        if (transform.name == "Up")
            _label.text = (int.Parse(_label.text) + 1).ToString();
        if (transform.name == "Down")
            _label.text = (int.Parse(_label.text) - 1).ToString();
    }
}
