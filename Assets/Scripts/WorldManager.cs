using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour
{
	public GameObject world1;
	public GameObject world2;
	public GameObject playerModel1;
	public GameObject playerModel2;

	public bool officeMode = false;

	NetPathmakerManager netPathmakerManager;
    
    GameObject[] flipTiles;
    GameObject[] governmentItems;
    GameObject[] internetItems;


	[HideInInspector] public GameObject currentOffice;

    void Start()
    {
		currentOffice = null;

		netPathmakerManager = GameObject.Find("NetPathmakerManager").GetComponent<NetPathmakerManager>();
		governmentItems = GameObject.FindGameObjectsWithTag("GovernmentStuff");
        foreach (GameObject go in governmentItems)
        {
            go.SetActive(false);
        }
    }

	public void FlipWorlds()
	{
		flipTiles = GameObject.FindGameObjectsWithTag("FlipTile");
		governmentItems = GameObject.FindGameObjectsWithTag("GovernmentStuff");
		internetItems = GameObject.FindGameObjectsWithTag("InternetStuff");

		//world1.SetActive(!world1.activeSelf);
		//world2.SetActive(!world2.activeSelf);

        foreach (GameObject go in internetItems)
        {
            if (go != null)
            {
                go.SetActive(!go.activeSelf);
            }
        }

        foreach (GameObject go in governmentItems)
        {
            if (go != null)
            {
                go.SetActive(!go.activeSelf);
            }
        }

		if(!officeMode)
		{
			foreach(OfficeGenerator officeGenerator in netPathmakerManager.officeHolder.GetComponentsInChildren<OfficeGenerator>())
			{
				if(officeGenerator.gameObject != currentOffice)
				{
					Destroy(officeGenerator.gameObject);
				}
			}

			netPathmakerManager.DestroyInternet();
		}

		playerModel1.SetActive(!playerModel1.activeSelf);
		playerModel2.SetActive(!playerModel2.activeSelf);

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
