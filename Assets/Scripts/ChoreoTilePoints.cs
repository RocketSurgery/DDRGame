using UnityEngine;
using System.Collections;

public class ChoreoTilePoints : MonoBehaviour
{
	public float threshhold = 0.2f;
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
		if ( ( (Vector2) collider.gameObject.transform.position - (Vector2) gameObject.transform.position).magnitude < threshhold )
		{
			PlayerCollision();
		}
	}

	protected virtual void PlayerCollision()
	{
		timeManager.AddTime(timeBonus);
		Destroy(gameObject);
	}
}
