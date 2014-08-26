using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeScript : MonoBehaviour
{
	Text text;
	string baseText;

	void Start()
	{
		text = GetComponent<Text>();
		baseText = text.text;
	}

	void Update()
	{
		text.text = baseText + TimeManager.singleton.instance.remainingTime.ToString("f2");
	}
}
