using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetPathmaker : MonoBehaviour 
{
	bool firstTile = true;
	public bool endInRoom = true;
	public int spawnNum = 3;

	[SerializeField] int minPathLength = 4;
	public List<GameObject> spawnedObjs = new List<GameObject>();

	int moveNum = 0;

	[SerializeField] GameObject pathPrefab;
	[SerializeField] float spawnMod; // increment of the spawn chance
	float spawnChance = 0.0f;
	NetPathmaker lastNetPathmaker;

	[SerializeField] GameObject officeGenPrefab;

	void Awake()
	{
	}

	void Update () 
	{
		SpawnPath();
		transform.position += transform.forward;

		if(Random.Range(0.0f, 1.0f) < spawnChance && !firstTile && moveNum > 3)
		{
			spawnChance = 0.0f;
			firstTile = true;
			spawnNum -= 1;

			if(spawnNum > 1)
			{
				SpawnPathmaker();
			}
		}
		else
		{
			spawnChance += Time.deltaTime * spawnMod/5.0f;
			firstTile = false;
		}

		moveNum++;

		if(spawnNum == 0)
		{
			if(endInRoom)
			{
				SpawnOffice();
			}
			
			DestroyMaker();
		}
	}

	void SpawnPath()
	{
		GameObject pathObj = WadeUtils.Instantiate(pathPrefab);
		spawnedObjs.Add(pathObj);
		pathObj.transform.position = transform.position - transform.up * pathPrefab.transform.localScale.z/2.0f; 
	}

	GameObject SpawnPath(Vector3 spawnPos)
	{
		GameObject pathObj = WadeUtils.Instantiate(pathPrefab);
		pathObj.transform.position = spawnPos - transform.up * pathPrefab.transform.localScale.z/2.0f; 
		return pathObj;
	}

	void SpawnPathmaker()
	{
		GameObject pathMakerObj = WadeUtils.Instantiate(gameObject);

		NetPathmaker netPathmaker = pathMakerObj.GetComponent<NetPathmaker>();
		lastNetPathmaker = netPathmaker;
		netPathmaker.spawnedObjs.Clear();
		netPathmaker.spawnNum = spawnNum;
		netPathmaker.endInRoom = true;

		if(Random.Range(-1.0f, 1.0f) > 0.0f)
		{
			pathMakerObj.transform.position += Vector3.right;
			pathMakerObj.transform.rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);

			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position + transform.forward + transform.right));
			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position + transform.forward - transform.right));
			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position - transform.forward + transform.right));
			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position - transform.forward - transform.right));
			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position - transform.right));
		}
		else
		{
			pathMakerObj.transform.position -= Vector3.right;
			pathMakerObj.transform.rotation *= Quaternion.Euler(0.0f, -90.0f, 0.0f);

			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position + transform.forward + transform.right));
			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position + transform.forward - transform.right));
			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position - transform.forward + transform.right));
			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position - transform.forward - transform.right));
			netPathmaker.spawnedObjs.Add(SpawnPath(transform.position + transform.right));
		}

//		Debug.LogError("FUCK YOU");
//		Debug.Break();
	}

	void SpawnOffice()
	{
		GameObject officeGenObj = WadeUtils.Instantiate(officeGenPrefab);
		officeGenObj.transform.position = transform.position;
		officeGenObj.GetComponent<OfficeGenerator>().netPath = transform;
	}

	void OnTriggerEnter(Collider col)
	{
		if(!spawnedObjs.Contains(col.gameObject))
		{
			if(lastNetPathmaker && !lastNetPathmaker.spawnedObjs.Contains(col.gameObject))
			{
				DestroyMaker();
			}
		}
	}

	void DestroyMaker()
	{
		if(spawnedObjs.Count < minPathLength)
		{
			GameObject[] spawned = spawnedObjs.ToArray();
			foreach(GameObject go in spawned)
			{
				Destroy(go);
			}
		}
		Destroy(gameObject);
	}
}
