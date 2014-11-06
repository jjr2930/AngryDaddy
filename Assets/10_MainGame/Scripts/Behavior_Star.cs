using UnityEngine;
using System.Collections;

public class Behavior_Star : MonoBehaviour {

    float _time = 0f;

    void Start()
    {
        _time = Random.Range(5f, 7f);
        iTween.MoveTo(gameObject, new Vector3(transform.position.x-3f, transform.position.y-3f, transform.position.z), _time);
        Destroy(gameObject, _time);
    }



}
