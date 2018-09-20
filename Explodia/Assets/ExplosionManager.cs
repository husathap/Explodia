﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour {

#if UNITY_EDITOR
    /* The list of explosion components. This will be automatically populated at the start.
     * This is public to allow for debugging.
     */
    public ExplosionComponent[] components;
#else
    private ExplosionComponent[] components;
#endif

    // The time it takes for the explosion to complete.
    public float explosionDuration;

    // The time it takes for the explosion to be reversed.
    public float reverseDuration;

    // The current status of explosion.
    private string explosionStatus = "";
    // The counter for explosion.
    private float explosionCounter = 0;
    //The counter for reversing.
    private float reversingCounter = 0;


    // Use this for initialization
    void Start () {
        components = GetComponentsInChildren<ExplosionComponent>();
	}
	
	// Update is called once per frame
	void Update () {

        // If the components are supposed to be exploding, move them to the intended target.
		if (explosionStatus == "exploding")
        {
            explosionCounter += Time.deltaTime;

            foreach (var c in components)
            {
                Vector3 actualEndPosition = c.endPosition + c.beginPosition;
                float step = Vector3.Distance(c.beginPosition, actualEndPosition) / explosionDuration * Time.deltaTime;
                c.transform.position = Vector3.MoveTowards(c.transform.position, actualEndPosition, step);
            }

            if (explosionCounter >= explosionDuration)
            {
                explosionStatus = "exploded";
                explosionCounter = 0;
            }
        }
        // Reversing the explosion.
        else if (explosionStatus == "reversing")
        {
            reversingCounter += Time.deltaTime;

            foreach (var c in components)
            {
                Vector3 actualEndPosition = c.endPosition + c.beginPosition;
                float step = Vector3.Distance(c.beginPosition, actualEndPosition) / reverseDuration * Time.deltaTime;
                c.transform.position = Vector3.MoveTowards(c.transform.position, c.beginPosition, step);
            }

            if (reversingCounter >= reverseDuration)
            {
                explosionStatus = "";
                reversingCounter = 0;
            }
        }
	}

    // Explode the components.
    public void Explode()
    {
        if (explosionStatus == "")
        {
            explosionStatus = "exploding";
        }
    }

    // Reintegrate the exploded components;
    public void Reverse()
    {
        if (explosionStatus == "exploded")
        {
            explosionStatus = "reversing";
        }
    }

    // Check if the explosion is still on going.
    public bool IsExploding()
    {
        return explosionStatus == "exploding";
    }

    // Check if exploded.
    public bool IsExploded()
    {
        return explosionStatus == "exploded";
    }

    // Check if reversing.
    public bool IsReversing()
    {
        return explosionStatus == "reversing";
    }

    // Check if integrated.
    public bool IsIntegrated()
    {
        return explosionStatus == "";
    }
}