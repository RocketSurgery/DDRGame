using UnityEngine;
using System.Collections;

public class Hair : MonoBehaviour 
{
	[SerializeField] int length;
	[SerializeField] GameObject hair;
	[SerializeField] Vector3 hairOffset;

	GameObject[] hairPieces;

	void Awake()
	{
		SpawnHair();
	}

	void Update () 
	{
	
	}

	void SpawnHair()
	{
		GameObject lastHair = gameObject;

		for(int i = 0; i < length; i++)
		{
			hairPieces[i] = Instantiate(hair, lastHair.transform.position + hairOffset, hair.transform.rotation) as GameObject;

		}
	}
}
