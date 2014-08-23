using UnityEngine;
using System.Collections;

public class ChoreoTilePoints : MonoBehaviour
{
	GameObject scoreManagerObject;
	protected ScoreManager scoreManagerComponent;
	public float threshhold = 0.2f;

	// Use this for initialization
	protected virtual void Start () {
		scoreManagerObject = GameObject.Find("ScoreManager");
		scoreManagerComponent = scoreManagerObject.GetComponent<ScoreManager>();
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
		scoreManagerComponent.AddScore();
		Destroy(gameObject);
	}
}
