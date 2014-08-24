﻿using UnityEngine;
using System.Collections;

public class CollectibleTile : ChoreoTilePoints
{
	protected override void PlayerCollision()
	{
		scoreManager.AddScore();
		Destroy(gameObject);
	}
}
