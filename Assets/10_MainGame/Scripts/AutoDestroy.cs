﻿using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, GetComponent<ParticleSystem>().duration + 3f);
	}
}
