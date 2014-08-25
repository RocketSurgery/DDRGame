using UnityEngine;
using System.Collections;

public class FloppyRotate : MonoBehaviour 
{
	[SerializeField] float rotateSpeed;

	void Awake()
	{
		StartCoroutine(ChoppyRotate());
		transform.rotation *= Quaternion.Euler(Random.Range(0.0f, 360.0f), 0.0f, 0.0f);
	}

	IEnumerator ChoppyRotate()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.3f);
			transform.rotation *= Quaternion.Euler(rotateSpeed, 0.0f, 0.0f);
		}
	}
}
