using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour 
{
	[SerializeField] Transform[] legs;
	[SerializeField] Transform[] arms;
	[SerializeField] Transform body;
	[SerializeField] Transform head;

	[SerializeField] float animSpeed;
	[SerializeField] float armAnimOffset; // this is to keep arm/legs from being exactly in sync

	[SerializeField] float bodyRotForce = 7.0f;
	[SerializeField] float headMoveForce;

	void Awake()
	{

	}

	void FixedUpdate () 
	{
		float count = Time.frameCount/60.0f * animSpeed;

		Vector3 tempScale = legs[0].transform.localScale;
		tempScale.y = Mathf.Sin(count);
		legs[0].transform.localScale = tempScale;

		tempScale = legs[1].transform.localScale;
		tempScale.y = -Mathf.Sin(count);
		legs[1].transform.localScale = tempScale;

		tempScale = arms[0].transform.localScale;
		tempScale.y = Mathf.Sin(count + armAnimOffset);
		arms[0].transform.localScale = tempScale;
		
		tempScale = arms[1].transform.localScale;
		tempScale.y = -Mathf.Sin(count + armAnimOffset);
		arms[1].transform.localScale = tempScale;

		Vector3 tempRot = body.transform.eulerAngles;
		tempRot.z = transform.parent.eulerAngles.z + 90.0f + Mathf.Sin(count) * bodyRotForce;
		body.transform.rotation = Quaternion.Euler(tempRot);

		Vector3 tempPos = head.transform.localPosition;
		tempPos.x = Mathf.Sin(count) * headMoveForce;
		head.transform.localPosition = tempPos;
	}
}
