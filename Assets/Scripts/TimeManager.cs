using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
	public static SingletonBehaviour<TimeManager> singleton = new SingletonBehaviour<TimeManager>();

	public float remainingTime = 20.0f;
	public bool debug = false;
    public GameObject endScreen;
    public GameObject endTextObject;
    public GameObject mainMenuButton;

	float fadeTime = 2.5f;

	void Update ()
	{
		remainingTime -= UnityEngine.Time.deltaTime;
		if (!debug && remainingTime < 0.0f)
		{
			remainingTime = 0;

			Player player = Player.singleton.instance;
            player.moveSpeed = 0;

			StartCoroutine(FadeSong());

			Camera.main.cullingMask = 0;
            Renderer[] playerRenderers = player.GetComponentsInChildren<Renderer>();

            foreach (Renderer go in playerRenderers)
            {
				go.enabled = false;
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

	IEnumerator FadeSong()
	{
		AudioSource song = WorldManager.singleton.instance.backgroundMusic;
		float startVolume = song.volume;

		float fadeTimer = 0.0f;
		while(fadeTimer <= fadeTime)
		{
			song.volume = Mathf.Lerp(startVolume, 0.0f, fadeTimer/fadeTime);
			fadeTimer += Time.deltaTime;
			yield return 0;
		}
	}

	public void AddTime(float time)
	{
		remainingTime += time;
	}
}
