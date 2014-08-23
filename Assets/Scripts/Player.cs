using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	[SerializeField] float moveSpeed;

	Vector3 lastPosition;
	Vector3 moveVector;

	void Awake()
	{

	}

	void Update()
	{
		transform.position += moveVector;

		if(Input.GetKeyDown(KeyCode.W))
		{
			moveVector = Vector2.up;
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			moveVector = -Vector2.up;
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			moveVector = -Vector2.right;
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			moveVector = Vector2.right;
		}
	}

	void LateUpdate()
	{
		lastPosition = transform.position;
	}

	void OnTriggerEnter(Collider col)
	{
		transform.position = lastPosition;
	}
}
