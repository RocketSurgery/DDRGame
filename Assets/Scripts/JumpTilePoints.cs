using UnityEngine;
using System.Collections;

public enum JumpTypes
{
	UP_DOWN,
	UP_RIGHT,
	UP_LEFT,
	LEFT_RIGHT,
	DOWN_LEFT,
	DOWN_RIGHT,
	NONE
}

public class JumpTilePoints : ChoreoTilePoints
{
	protected ItemText itemText;

	public JumpTypes jump;
	public SpriteRenderer sprite;
	public static new float timeBonus = 1.0f;

	protected override void Start()
	{
		base.Start();

		itemText = GameObject.Find("ItemText").GetComponent<ItemText>();

		sprite = (SpriteRenderer)renderer;
		foreach (Sprite resource in Resources.LoadAll<Sprite>(""))
		{
			if (resource.name == jump.ToString())
			{
				sprite.sprite = resource;
				break;
			}
		}
	}

	protected override void PlayerCollision()
	{
		if (JumpTypeFromInput() == jump)
		{
			timeManager.AddTime(timeBonus);
			itemText.DisplayText("You have gained more hours on the internet!");
			Destroy(gameObject);
		}
	}

	protected JumpTypes JumpTypeFromInput()
	{
		bool up = Input.GetKey(KeyCode.UpArrow);
		bool down = Input.GetKey(KeyCode.DownArrow);
		bool left = Input.GetKey(KeyCode.LeftArrow);
		bool right = Input.GetKey(KeyCode.RightArrow);

		if (up && down)
		{
			return JumpTypes.UP_DOWN;
		}
		else if (up && left)
		{
			return JumpTypes.UP_LEFT;
		}
		else if (up && right)
		{
			return JumpTypes.UP_RIGHT;
		}
		else if (left && right)
		{
			return JumpTypes.LEFT_RIGHT;
		}
		else if (down && left)
		{
			return JumpTypes.DOWN_LEFT;
		}
		else if (down && right)
		{
			return JumpTypes.DOWN_RIGHT;
		}
		return JumpTypes.NONE;
	}
}
