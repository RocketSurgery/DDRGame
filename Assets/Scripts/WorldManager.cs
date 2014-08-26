using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour
{
	public static SingletonBehaviour<WorldManager> singleton = new SingletonBehaviour<WorldManager>();

//	public GameObject world1;
//	public GameObject world2;
	public GameObject playerModel1;
	public GameObject playerModel2;

	public bool officeMode = false;
    
    GameObject[] flipTiles;
    GameObject[] governmentItems;
    GameObject[] internetItems;

	int allCameraLayers = 0;

	[HideInInspector] public OfficeGenerator currentOffice;

	public AudioSource backgroundMusic;

    void Awake()
    {
		backgroundMusic = SoundManager.singleton.instance.Play2DSong("FloppyDrift", 1.0f, true);

		allCameraLayers = Camera.main.cullingMask;
		currentOffice = null;

		governmentItems = GameObject.FindGameObjectsWithTag("GovernmentStuff");
        foreach (GameObject go in governmentItems)
        {
            go.SetActive(false);
        }
    }

	public void FlipWorlds()
	{
		if(!officeMode)
		{
			backgroundMusic.volume = 0.2f;

			foreach(OfficeGenerator officeGenerator in NetPathmakerManager.singleton.instance.officeHolder.GetComponentsInChildren<OfficeGenerator>())
			{
				if(officeGenerator != currentOffice)
				{
					Destroy(officeGenerator.gameObject);
				}
			}

			Camera.main.cullingMask = (1 << LayerMask.NameToLayer("Ignore Raycast") |  1 << LayerMask.NameToLayer("Office Contents"));
			NetPathmakerManager.singleton.instance.DestroyInternet();
		}
		else
		{
			backgroundMusic.volume = 1.0f;

			currentOffice.DespawnPoints();
			Camera.main.cullingMask = allCameraLayers;
		}

		playerModel1.SetActive(!playerModel1.activeSelf);
		playerModel2.SetActive(!playerModel2.activeSelf);

		flipTiles = GameObject.FindGameObjectsWithTag("FlipTile");
        foreach ( GameObject flipTile in flipTiles )
        {
            flipTile.transform.localRotation *= Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }

		officeMode = !officeMode;
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
