using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour
{
	public GameObject world1;
	public GameObject world2;
	public GameObject playerModel1;
	public GameObject playerModel2;
    
    GameObject[] flipTiles;
    GameObject[] governmentItems;
    GameObject[] internetItems;

    void Start()
    {
        flipTiles = GameObject.FindGameObjectsWithTag("FlipTile");
        governmentItems = GameObject.FindGameObjectsWithTag("GovernmentStuff");
        internetItems = GameObject.FindGameObjectsWithTag("InternetStuff");

        foreach (GameObject go in governmentItems)
        {
            go.SetActive(false);
        }
    }

	public void FlipWorlds()
	{
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

		playerModel1.SetActive(!playerModel1.activeSelf);
		playerModel2.SetActive(!playerModel2.activeSelf);

        foreach ( GameObject flipTile in flipTiles )
        {
            flipTile.transform.Rotate(0, 0, 180);
            Debug.Log("Rotating");
        }
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
