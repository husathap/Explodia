using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour {

#if UNITY_EDITOR
    /* The list of explosion components. This will be automatically populated at the start.
     * This is public to allow for debugging.
     */
    public ExplosionComponent[] compArray;
#else
    private ExplosionComponent[] components;
#endif

    // Similar to compArray, but contains string to allow for quick referencing.
    private Dictionary<string, ExplosionComponent> compDict;

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
        compArray = GetComponentsInChildren<ExplosionComponent>();
        compDict = new Dictionary<string, ExplosionComponent>();

        foreach (ExplosionComponent e in compArray)
        {
            compDict.Add(e.name, e);
        }
	}
	
	// Update is called once per frame
	void Update () {

        // If the components are supposed to be exploding, move them to the intended target.
		if (explosionStatus == "exploding")
        {
            explosionCounter += Time.deltaTime;

            foreach (var c in compArray)
            {
                // Move the object.
                Vector3 actualEndPosition = c.endPosition + c.beginPosition;
                float step = Vector3.Distance(c.beginPosition, actualEndPosition) / explosionDuration * Time.deltaTime;
                c.transform.position = Vector3.MoveTowards(c.transform.position, actualEndPosition, step);

                // Update the transparency of the description.
                GameObject cd = c.GetDescription();
                Color originalColor = cd.GetComponent<Renderer>().material.color;
                cd.GetComponent<Renderer>().material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - Vector3.Distance(c.transform.position, actualEndPosition) / Vector3.Distance(c.beginPosition, c.endPosition));
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

            foreach (var c in compArray)
            {
                // Move the object.
                Vector3 actualEndPosition = c.endPosition + c.beginPosition;
                float step = Vector3.Distance(c.beginPosition, actualEndPosition) / reverseDuration * Time.deltaTime;
                c.transform.position = Vector3.MoveTowards(c.transform.position, c.beginPosition, step);

                // Update the transparency of the description.
                GameObject cd = c.GetDescription();
                Color originalColor = cd.GetComponent<Renderer>().material.color;
                cd.GetComponent<Renderer>().material.color = new Color(originalColor.r, originalColor.g, originalColor.b, Vector3.Distance(c.transform.position, actualEndPosition) / Vector3.Distance(c.beginPosition, c.endPosition));
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
