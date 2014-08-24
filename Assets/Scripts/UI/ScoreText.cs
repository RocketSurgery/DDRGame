using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour
{
	ScoreManager scoreManager;
	Text text;
	string baseText;

	void Start()
	{
		text = GetComponent<Text>();
		baseText = text.text;
		scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}

	void Update ()
	{
		text.text = baseText + scoreManager.Score;
	}
}
