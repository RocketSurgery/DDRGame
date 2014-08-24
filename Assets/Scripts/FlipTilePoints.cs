using UnityEngine;
using System.Collections;

public class FlipTilePoints : JumpTilePoints
{
	GameObject worldManagerObject;
	WorldManager worldManagerScript;

	protected override void Start()
	{
		base.Start();

		worldManagerObject = GameObject.Find("WorldManager");
		worldManagerScript = worldManagerObject.GetComponent<WorldManager>();
	}

	protected override void PlayerCollision()
	{
		if (JumpTypeFromInput() == jump)
		{
			worldManagerScript.FlipWorlds();
			timeManager.AddTime(timeBonus);
			Destroy(gameObject);
		}
	}
}
