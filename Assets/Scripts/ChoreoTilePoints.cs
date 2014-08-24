using UnityEngine;
using System.Collections;

public class ChoreoTilePoints : MonoBehaviour
{
	public float threshhold = 0.2f;
	public float timeBonus = 0.5f;

	protected ScoreManager scoreManager;
	protected TimeManager timeManager;

	protected virtual void Start ()
	{
		scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if ( (collider.gameObject.transform.position - gameObject.transform.position).magnitude < threshhold )
		{
			PlayerCollision();
		}
	}

	protected virtual void PlayerCollision()
	{
		scoreManager.AddScore();
		timeManager.AddTime(timeBonus);
		Destroy(gameObject);
	}
}
