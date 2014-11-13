using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour {

    public Camera _uiCamera;
    Ray _ray;
    RaycastHit _hit;
	// Use this for initialization
	void Start () {
        _hit = new RaycastHit();
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Fire1"))
        {
            _ray = _uiCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(_ray,out _hit,10000);
            Debug.Log(_hit.transform.name);
        }
	}
}
