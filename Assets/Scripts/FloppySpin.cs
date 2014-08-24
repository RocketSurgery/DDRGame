using UnityEngine;
using System.Collections;

public class FloppySpin : MonoBehaviour
{
	void Update ()
	{
		transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), -90.0f * Time.deltaTime, Space.World);
	}
}
