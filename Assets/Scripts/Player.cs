using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
	[SerializeField] float moveSpeed;

	[SerializeField] float hLineDistance = 0.2f;
	[SerializeField] float vLineDistance = 1.0f;

	Vector2 moveVector;

	public float distanceToNearestPosition;
	public Vector3 nearestPosition;
	public Vector3 debugPos;

	List<KeyCode> keyPresses;
	public float inputDelay = 0.05f;

	void Awake()
	{
		moveVector = Vector2.up;
		keyPresses = new List<KeyCode>();
	}

	void FixedUpdate()
	{
		Vector2 line1Start = (Vector2)(transform.position - transform.right * hLineDistance);
		Vector2 line1End = line1Start + (Vector2)transform.up * vLineDistance;

		Vector2 line2Start = (Vector2)(transform.position + transform.right * hLineDistance);
		Vector2 line2End = line2Start + (Vector2)transform.up * vLineDistance;

		Debug.DrawLine(line1Start, line1End);
		Debug.DrawLine(line2Start, line2End);

        if (!(Physics2D.Linecast(line1Start, line1End) || Physics2D.Linecast(line2Start, line2End)))
        {
            transform.position += (Vector3)moveVector * moveSpeed * Time.deltaTime;
        }
	}

	void Update()
	{
		bool up = Input.GetKeyDown(KeyCode.UpArrow);
		bool down = Input.GetKeyDown(KeyCode.DownArrow);
		bool left = Input.GetKeyDown(KeyCode.LeftArrow);
		bool right = Input.GetKeyDown(KeyCode.RightArrow);

		if ((up || down || left || right) && keyPresses.Count == 0)
		{
			Invoke("Move", inputDelay);
		}

		if (up) keyPresses.Add(KeyCode.UpArrow);
		if (down) keyPresses.Add(KeyCode.DownArrow);
		if (left) keyPresses.Add(KeyCode.LeftArrow);
		if (right) keyPresses.Add(KeyCode.RightArrow);
	}

	void Move()
	{
		nearestPosition = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
		distanceToNearestPosition = Vector3.Project(transform.position - nearestPosition, moveVector).magnitude;

		// only turn if a single input was detected
		if (keyPresses.Count == 1)
		{
			if (keyPresses[0] == KeyCode.UpArrow && moveVector != Vector2.up)
			{
				moveVector = Vector2.up;
				transform.transform.rotation = Quaternion.Euler(Vector3.zero);
				//Vector3 moveVector3 = new Vector3(moveVector.x, moveVector.y, 0);
                transform.position = nearestPosition;// +distanceToNearestPosition * moveVector3;
			}
			else if (keyPresses[0] == KeyCode.DownArrow && moveVector != -Vector2.up)
			{
				moveVector = -Vector2.up;
				transform.transform.rotation = Quaternion.Euler(Vector3.forward * 180.0f);
				//Vector3 moveVector3 = new Vector3(moveVector.x, moveVector.y, 0);
                transform.position = nearestPosition;// +distanceToNearestPosition * moveVector3;
			}
			else if (keyPresses[0] == KeyCode.LeftArrow && moveVector != -Vector2.right)
			{
				moveVector = -Vector2.right;
				transform.transform.rotation = Quaternion.Euler(Vector3.forward * 90.0f);
				//Vector3 moveVector3 = new Vector3(moveVector.x, moveVector.y, 0);
                transform.position = nearestPosition;// +distanceToNearestPosition * moveVector3;
			}
			else if (keyPresses[0] == KeyCode.RightArrow && moveVector != Vector2.right)
			{
				moveVector = Vector2.right;
				transform.transform.rotation = Quaternion.Euler(Vector3.forward * -90.0f);
				//Vector3 moveVector3 = new Vector3(moveVector.x, moveVector.y, 0);
                transform.position = nearestPosition;// +distanceToNearestPosition * moveVector3;
			}
		}

		// empty input queue
		keyPresses.Clear();
	}
}
