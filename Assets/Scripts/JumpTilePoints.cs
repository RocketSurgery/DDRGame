using UnityEngine;
using System.Collections;

public enum JumpTypes
{
	UP_DOWN,
	UP_RIGHT,
	UP_LEFT,
	LEFT_RIGHT,
	DOWN_LEFT,
	DOWN_RIGHT
}

public class JumpTilePoints : ChoreoTilePoints
{
	public JumpTypes jump;

	void OnTriggerStay2D(Collider2D collider)
	{
		if ( (collider.gameObject.transform.position - gameObject.transform.position).magnitude < threshhold &&
			JumpTypeFromInput() == jump )
		{
			scoreManagerComponent.AddScore();
			Destroy(gameObject);
		}
	}

	protected JumpTypes JumpTypeFromInput()
	{
		return JumpTypes.UP_DOWN;
	}
}
