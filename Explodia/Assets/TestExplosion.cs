using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExplosion : MonoBehaviour {

    private ExplosionManager man;

	// Use this for initialization
	void Start () {
        man = this.GetComponent<ExplosionManager>();
        man.Explode();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            man.Reverse();
        }
	}
}
