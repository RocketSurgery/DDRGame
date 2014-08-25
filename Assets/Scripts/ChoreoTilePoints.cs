using UnityEngine;
using System.Collections;

public class ChoreoTilePoints : MonoBehaviour
{
	public static float timeBonus = 0.25f;

	protected virtual void Start ()
	{
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		PlayerCollision();
	}

	protected virtual void PlayerCollision()
	{
		TimeManager.singleton.instance.AddTime(timeBonus);
		Destroy(gameObject);
	}
}
