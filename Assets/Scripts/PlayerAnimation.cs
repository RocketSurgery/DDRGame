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

	[SerializeField] float bodyAnimSpeed;
	[SerializeField] float bodyAnimOffset;

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

//		Vector3 tempPos = body.transform.position;
//		tempPos.x = Mathf.Sin(count) * bodyAnimSpeed;
//		body.transform.position = tempPos;
	}
}
