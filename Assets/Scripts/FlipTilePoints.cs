using UnityEngine;
using System.Collections;

public class FlipTilePoints : JumpTilePoints
{
	GameObject worldManagerObject;
	WorldManager worldManagerScript;

	public override void Start()
	{
		base.Start();

		worldManagerObject = GameObject.Find("WorldManager");
		worldManagerScript = worldManagerObject.GetComponent<WorldManager>();
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if ( (collider.gameObject.transform.position - gameObject.transform.position).magnitude < threshhold &&
			JumpTypeFromInput() == jump )
		{
			scoreManagerComponent.AddScore();
			worldManagerScript.FlipWorlds();
			Destroy(gameObject);
		}
	}
}
