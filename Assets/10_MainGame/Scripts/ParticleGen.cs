using UnityEngine;
using System.Collections;

public class ParticleGen : MonoBehaviour {

    void OnEnable()
    {
        GameObject obj = Instantiate(Resources.Load("ParticleStar1"), Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.localPosition = new Vector3(0.34f, 0.55f, 10f);
        obj.transform.localRotation = Quaternion.Euler(new Vector3(-30f, 90f, 0f));
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
