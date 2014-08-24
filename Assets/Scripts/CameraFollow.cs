using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{	
	[SerializeField] Transform followTarget;
	[SerializeField] float followSpeed = 10.0f;

	void FixedUpdate () 
	{
		Vector2 flatPosition = Vector2.Lerp(transform.position, followTarget.position, Time.deltaTime * followSpeed);
		transform.position = new Vector3(flatPosition.x, flatPosition.y, transform.position.z);
	}
}
