using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour
{
	Text text;
	string baseText;

	void Start()
	{
		text = GetComponent<Text>();
		baseText = text.text;
	}

	void Update ()
	{
		text.text = baseText + ScoreManager.singleton.instance.score;
	}
}
