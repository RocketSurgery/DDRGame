using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetPathmakerManager : MonoBehaviour 
{
	public static SingletonBehaviour<NetPathmakerManager> singleton = new SingletonBehaviour<NetPathmakerManager>();
	public List<Transform> internetTiles = new List<Transform>();
	public GameObject netPathmakerPrefab;
	public Player player;

	public Transform internetTileHolder;
	public Transform officeHolder;
	public Transform netPathmakerHolder;

	public WorldManager worldManager;

	void Awake()
	{
		singleton.DontDestroyElseKill(this);

		player = GameObject.Find("Player").GetComponent<Player>();
		worldManager = GameObject.Find("WorldManager").GetComponent<WorldManager>();
	}

	public void SpawnNetPathmaker(Vector3 position, Quaternion rotation)
	{
		GameObject netPathmaker = WadeUtils.Instantiate(netPathmakerPrefab, position, rotation);
		netPathmaker.transform.parent = netPathmakerHolder;
	}

	public void DestroyInternet()
	{
		Vector3 pos = internetTileHolder.position;
		Quaternion rot = internetTileHolder.rotation;

		Destroy(internetTileHolder.gameObject);
		internetTiles.Clear();

		GameObject newTileHolder = new GameObject("InternetTileHolder");
		newTileHolder.transform.position = pos;
		newTileHolder.transform.rotation = rot;

		internetTileHolder = newTileHolder.transform;
	}
}
