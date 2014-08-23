using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	[SerializeField] float moveSpeed;
	[SerializeField] Transform model;

	Vector2 moveVector;

	void Awake()
	{
		moveVector = Vector2.up;
	}

	void FixedUpdate()
	{
		Vector2 line1Start = (Vector2)(transform.position - model.right * 0.8f);
		Vector2 line1End = line1Start + (Vector2)model.up;

		Vector2 line2Start = (Vector2)(transform.position + model.right * 0.8f);
		Vector2 line2End = line2Start + (Vector2)model.up;

		Debug.DrawLine(line1Start, line1End);
		Debug.DrawLine(line2Start, line2End);

		if(!(Physics2D.Linecast(line1Start, line1End) || Physics2D.Linecast(line2Start, line2End)))
		{
			transform.position += (Vector3)moveVector * moveSpeed * Time.deltaTime;
		}

		if(Input.GetKeyDown(KeyCode.W))
		{
			moveVector = Vector2.up;
			model.transform.rotation = Quaternion.Euler(Vector3.zero);
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			moveVector = -Vector2.up;
			model.transform.rotation = Quaternion.Euler(Vector3.forward * 180.0f);
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			moveVector = -Vector2.right;
			model.transform.rotation = Quaternion.Euler(Vector3.forward * 90.0f);
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			moveVector = Vector2.right;
			model.transform.rotation = Quaternion.Euler(Vector3.forward * -90.0f);
		}
	}
}
