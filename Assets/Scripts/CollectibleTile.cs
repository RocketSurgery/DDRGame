using UnityEngine;
using System.Collections;

public class CollectibleTile : JumpTilePoints
{
	protected override void PlayerCollision()
	{
		if (JumpTypeFromInput() == jump)
		{
			scoreManager.AddScore();
			itemText.DisplayText("You have stolen some government secrets!");
			Destroy(gameObject);
		}
	}
}
