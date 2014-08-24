using UnityEngine;
using System.Collections;

public class ChoreoTilePoints : MonoBehaviour
{
	GameObject scoreManagerObject;
	protected ScoreManager scoreManagerComponent;
	public float threshhold = 0.1f;

	// Use this for initialization
	protected virtual void Start () {
		scoreManagerObject = GameObject.Find("ScoreManager");
		scoreManagerComponent = scoreManagerObject.GetComponent<ScoreManager>();
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		PlayerCollision();
	}

	protected virtual void PlayerCollision()
	{
		scoreManagerComponent.AddScore();
		Destroy(gameObject);
	}
}
