using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour {

    // The original position of the object.
    public Vector3 beginPosition;

    // The position the component should move to when exploded.
    public Vector3 endPosition;

    // Get the description of the object.
    public GameObject GetDescription()
    {
        return this.transform.GetChild(0).gameObject;
    }

	// Use this for initialization
	void Start () {
        Color descriptColor = this.GetDescription().GetComponent<Renderer>().material.color;
        this.GetDescription().GetComponent<Renderer>().material.color = new Color(descriptColor.r, descriptColor.g, descriptColor.b, 0);
        beginPosition = this.transform.position;
	}
}
