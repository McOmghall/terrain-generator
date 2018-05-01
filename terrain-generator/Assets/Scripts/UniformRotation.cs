using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformRotation : MonoBehaviour {
  public float degreesPerSecondRotation = 90;
  private float degreeRotation = 0;

	// Update is called once per frame
	void Update () {
    degreeRotation += degreesPerSecondRotation * Time.deltaTime;
    transform.localRotation = Quaternion.Euler(0, degreeRotation, 0);
	}
}
