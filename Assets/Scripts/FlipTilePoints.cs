using UnityEngine;
using System.Collections;

public class FlipTilePoints : JumpTilePoints
{
	GameObject worldManagerObject;
	WorldManager worldManagerScript;

    float lastTimeFlipWorld = 0;

	protected override void Start()
	{
		base.Start();

		worldManagerObject = GameObject.Find("WorldManager");
		worldManagerScript = worldManagerObject.GetComponent<WorldManager>();
	}

	protected override void PlayerCollision()
	{
		if (JumpTypeFromInput() == jump && (Time.time - lastTimeFlipWorld) > 1.0f) // Make it so the world doesn't flip back immediately
		{
			worldManagerScript.FlipWorlds();
            lastTimeFlipWorld = Time.time;
			timeManager.AddTime(timeBonus);
		}
	}
}
