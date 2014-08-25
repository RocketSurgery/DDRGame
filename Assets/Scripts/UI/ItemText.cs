using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemText : MonoBehaviour
{
	public static SingletonBehaviour<ItemText> singleton = new SingletonBehaviour<ItemText>();

	public float displayTime = 2.0f;
	float remainingTime = 0.0f;

	Text text;

	void Start()
	{
		text = GetComponent<Text>();
	}

	void Update ()
	{
		remainingTime -= Time.deltaTime;
		if (remainingTime < 0)
		{
			Color color = text.color;
			color.a = 0.0f;
			text.color = color;
		}
	}

	public void DisplayText(string displayText)
	{
		text.text = displayText;
		remainingTime = displayTime;

		Color color = text.color;
		color.a = 1.0f;
		text.color = color;
	}
}
