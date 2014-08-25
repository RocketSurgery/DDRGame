using UnityEngine;
using System.Collections;

public class ScaleIn : MonoBehaviour 
{
	Vector3 initScale;
	[SerializeField] float scaleTime = 1.0f;
	float currentTime = 0.0f;

	void Awake()
	{
		initScale = transform.localScale;
		transform.localScale = Vector3.zero;

		StartCoroutine(Scale());
	}

	IEnumerator Scale()
	{
		while(currentTime <= scaleTime)
		{
			transform.localScale = Vector3.Lerp(Vector3.zero, initScale, currentTime/scaleTime);

			currentTime += Time.deltaTime;
			yield return 0;
		}
	}
}
