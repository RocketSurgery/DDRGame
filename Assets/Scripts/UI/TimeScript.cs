using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeScript : MonoBehaviour
{
	TimeManager timeManager;
	Text text;
	string baseText;

	void Start()
	{
		text = GetComponent<Text>();
		baseText = text.text;
		timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
	}

	void Update()
	{
		text.text = baseText + timeManager.Time;
	}
}
