using UnityEngine;
using System.Collections;

public class CollectibleTile : JumpTilePoints
{
	protected override void PlayerCollision()
	{
		if (JumpTypeFromInput() == jump)
		{
			scoreManager.AddScore();
			Destroy(gameObject);
		}
	}
}
