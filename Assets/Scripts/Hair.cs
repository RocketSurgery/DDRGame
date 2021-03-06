using UnityEngine;
using System.Collections;

public class Hair : MonoBehaviour 
{
	[SerializeField] int length = 4;
	[SerializeField] GameObject hair;
	[SerializeField] Vector3 hairOffset;
	[SerializeField] float followSpeed = 1.0f;
	[SerializeField] float indexBias = 0.7f;

	GameObject[] hairPieces;

	void Awake()
	{
		SpawnHair();
	}

	void FixedUpdate () 
	{
		Transform lastHair = transform;

		for(int i = 0; i < length; i++)
		{
			float indexAlpha = ((float)i + 1.0f) * indexBias;
			hairPieces[i].transform.localPosition = Vector3.Lerp(hairPieces[i].transform.localPosition, 
			                                                lastHair.position + transform.rotation * hairOffset * indexAlpha,
			                                                Time.deltaTime * followSpeed);

			hairPieces[i].transform.rotation = Quaternion.Lerp(hairPieces[i].transform.rotation, 
			                                                   transform.rotation,
			                                                   Time.deltaTime * followSpeed);
			lastHair = hairPieces[i].transform;
		}
	}

	void SpawnHair()
	{
		hairPieces = new GameObject[length];
		Transform lastHair = gameObject.transform;

		for(int i = 0; i < length; i++)
		{
			hairPieces[i] = Instantiate(hair, lastHair.position + hairOffset, hair.transform.rotation) as GameObject;
			hairPieces[i].transform.localScale = Vector3.one - Vector3.one * (i)/length;

			lastHair = hairPieces[i].transform;
		}
	}
}
