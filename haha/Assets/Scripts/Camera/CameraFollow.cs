using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform Target;
	public float Smoothing = 5f;

	private Vector3 Offset;

	void Start ()
	{
		Offset = transform.position - Target.transform.position;
	}
	
	void FixedUpdate ()
	{
		transform.position = Vector3.Lerp(transform.position, Target.position + Offset, Smoothing*Time.deltaTime);
	}
}
