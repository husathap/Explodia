using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour {

    // The original position of the object.
    public Vector3 beginPosition;

    // The position the component should move to when exploded.
    public Vector3 endPosition;

	// Use this for initialization
	void Start () {
        beginPosition = this.transform.position;
	}
}
