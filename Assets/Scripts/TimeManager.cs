using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
	public float remainingTime = 20.0f;

	void Update ()
	{
		remainingTime -= Time.deltaTime;
	}

	public void AddTime(float time)
	{
		remainingTime += time;
	}
}
