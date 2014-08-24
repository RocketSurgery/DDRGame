using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
	public float remainingTime = 20.0f;
	public bool debug = false;
    public GameObject player;
    public GameObject endScreen;
    public GameObject endTextObject;

	void Update ()
	{
		remainingTime -= UnityEngine.Time.deltaTime;
		if (!debug && remainingTime < 0.0f)
		{
            remainingTime = 0;
            player.GetComponent<Player>().moveSpeed = 0;
            endScreen.SetActive(true);
            Text endTextComponent = endTextObject.GetComponent<Text>();
            endTextComponent.text = "You have ran out of hours on the internet.\nYou leaked " + ScoreManager.Score + " documents";
            Color newColor = endTextComponent.color;
            newColor.a = 1.0f;
            endTextComponent.color = newColor;
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
