using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
	public static SingletonBehaviour<TimeManager> singleton = new SingletonBehaviour<TimeManager>();

	public float remainingTime = 20.0f;
	public bool debug = false;
    public GameObject player;
    public GameObject endScreen;
    public GameObject endTextObject;
    public GameObject mainMenuButton;

	void Update ()
	{
		remainingTime -= UnityEngine.Time.deltaTime;
		if (!debug && remainingTime < 0.0f)
		{
            remainingTime = 0;
            player.GetComponent<Player>().moveSpeed = 0;

            GameObject[] playerStuff = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject go in playerStuff)
            {
                go.SetActive(false);
            }

            endScreen.SetActive(true);
            mainMenuButton.SetActive(true);
            Text endTextComponent = endTextObject.GetComponent<Text>();
            endTextComponent.text = "You have ran out of hours on the internet.\nYou leaked " + ScoreManager.singleton.instance.score + " documents";
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
