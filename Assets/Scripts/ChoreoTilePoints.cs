using UnityEngine;
using System.Collections;

public class ChoreoTilePoints : MonoBehaviour
{
	public static float timeBonus = 0.25f;

	protected ScoreManager scoreManager;
	protected TimeManager timeManager;

	protected virtual void Start ()
	{
		scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		PlayerCollision();
	}

	protected virtual void PlayerCollision()
	{
		timeManager.AddTime(timeBonus);
		Destroy(gameObject);
	}
}
