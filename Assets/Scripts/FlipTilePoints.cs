using UnityEngine;
using System.Collections;

public class FlipTilePoints : JumpTilePoints
{
	public OfficeGenerator office;

    float lastTimeFlipWorld = 0;

	protected override void Start()
	{
		base.Start();
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() && JumpTypeFromInput() == jump && (Time.time - lastTimeFlipWorld) > 1.0f) // Make it so the world doesn't flip back immediately
		{
			WorldManager.singleton.instance.currentOffice = office;
			WorldManager.singleton.instance.FlipWorlds();

			if(WorldManager.singleton.instance.officeMode)
			{
				office.ceiling.gameObject.SetActive(false);
				office.insideOffice = true;

				office.settingUp = true;
				office.RespawnInternet(transform);
			}
			else
			{
				office.ceiling.gameObject.SetActive(true);
				office.insideOffice = false;
			}

            lastTimeFlipWorld = Time.time;
			//Destroy(gameObject);
		}
    }

	protected override void PlayerCollision()
	{
		if (JumpTypeFromInput() == jump && (Time.time - lastTimeFlipWorld) > 1.0f) // Make it so the world doesn't flip back immediately
		{
			WorldManager.singleton.instance.FlipWorlds();
            lastTimeFlipWorld = Time.time;
			TimeManager.singleton.instance.AddTime(timeBonus);
		}
	}
}
