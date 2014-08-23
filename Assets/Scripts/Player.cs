using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	[SerializeField] float moveSpeed;

	[SerializeField] float hLineDistance = 1.0f;
	[SerializeField] float vLineDistance = 1.0f;

	Vector2 moveVector;

	void Awake()
	{
		moveVector = Vector2.up;
	}

	void FixedUpdate()
	{
		Vector2 line1Start = (Vector2)(transform.position - transform.right * hLineDistance);
		Vector2 line1End = line1Start + (Vector2)transform.up * vLineDistance;

		Vector2 line2Start = (Vector2)(transform.position + transform.right * hLineDistance);
		Vector2 line2End = line2Start + (Vector2)transform.up * vLineDistance;

		Debug.DrawLine(line1Start, line1End);
		Debug.DrawLine(line2Start, line2End);

		if(!(Physics2D.Linecast(line1Start, line1End) || Physics2D.Linecast(line2Start, line2End)))
		{
			transform.position += (Vector3)moveVector * moveSpeed * Time.deltaTime;
		}
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveVector = Vector2.up;
            transform.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveVector = -Vector2.up;
            transform.transform.rotation = Quaternion.Euler(Vector3.forward * 180.0f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveVector = -Vector2.right;
            transform.transform.rotation = Quaternion.Euler(Vector3.forward * 90.0f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveVector = Vector2.right;
            transform.transform.rotation = Quaternion.Euler(Vector3.forward * -90.0f);
        }
    }
}
