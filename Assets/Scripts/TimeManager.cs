using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
	public float remainingTime = 20.0f;
	public bool debug = false;

	void Update ()
	{
		remainingTime -= UnityEngine.Time.deltaTime;
		if (!debug && remainingTime < 0.0f)
		{
			Application.LoadLevel("InternetPoints");
		}
	}

	public void AddTime(float time)
	{
		remainingTime += time;
	}

	public float Time
	{
		get
		{
			return remainingTime;
		}
	}
}
