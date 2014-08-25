using UnityEngine;
using System.Collections;

public class FlipTilePoints : JumpTilePoints
{
	public GameObject office;

	GameObject worldManagerObject;
	WorldManager worldManagerScript;

    float lastTimeFlipWorld = 0;

	protected override void Start()
	{
		base.Start();

		worldManagerObject = GameObject.Find("WorldManager");
		worldManagerScript = worldManagerObject.GetComponent<WorldManager>();
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if (JumpTypeFromInput() == jump && (Time.time - lastTimeFlipWorld) > 1.0f) // Make it so the world doesn't flip back immediately
		{
			worldManagerScript.currentOffice = office;
			worldManagerScript.FlipWorlds();

			OfficeGenerator officeGenerator = office.GetComponent<OfficeGenerator>();

			if(worldManagerScript.officeMode)
			{
				officeGenerator.ceiling.gameObject.SetActive(false);
				officeGenerator.insideOffice = true;

				transform.parent.parent.GetComponent<OfficeGenerator>().RespawnInternet(transform);
			}
			else
			{
				officeGenerator.ceiling.gameObject.SetActive(true);
				officeGenerator.insideOffice = false;
			}

            lastTimeFlipWorld = Time.time;
			//Destroy(gameObject);
		}
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
