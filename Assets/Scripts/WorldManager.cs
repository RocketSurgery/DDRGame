using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour
{
	public GameObject world1;
	public GameObject world2;
	public GameObject playerModel1;
	public GameObject playerModel2;

	public void FlipWorlds()
	{
		world1.SetActive(!world1.activeSelf);
		world2.SetActive(!world2.activeSelf);

		playerModel1.SetActive(!playerModel1.activeSelf);
		playerModel2.SetActive(!playerModel2.activeSelf);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			FlipWorlds();
		}
	}
}
