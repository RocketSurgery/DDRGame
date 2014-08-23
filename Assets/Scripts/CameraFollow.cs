using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{	
	[SerializeField] Transform followTarget;
	[SerializeField] float followSpeed;
	Vector3 offset;

	void Awake()
	{
		offset = followTarget.position - transform.position;
	}

	void FixedUpdate () 
	{
		transform.position = Vector3.Lerp(transform.position, followTarget.position - offset, Time.deltaTime * followSpeed);
	}
}
