using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OfficeGenerator : MonoBehaviour 
{
	[SerializeField] Vector2 boundsMinMax = new Vector2(6, 15);

	public Transform netPath;
	Vector3 furthestPos;
	Vector3 furthestDir;

	[SerializeField] float groundHeight = 0.0f;

	[SerializeField] GameObject wallPrefab;
	[SerializeField] GameObject floorPrefab;
	[SerializeField] GameObject doorPrefab;

	Transform floor;
	public Transform ceiling;
	Transform wallHolder;
	Transform doorHolder;

	Vector3 rayDir = Vector3.zero;
	Vector3 rayStart = Vector3.zero;
	Quaternion doorRot;

	public bool insideOffice = false;

	int roomWidth = 0;
	int roomLength = 0;

	[HideInInspector] public float timeAlive = 0.0f;

	List<GameObject> walls = new List<GameObject>();

	void Awake()
	{
		GameObject wallHolderObj = new GameObject("WallHolder");
		wallHolder = wallHolderObj.transform;
		wallHolder.parent = transform;
		wallHolder.position = transform.position;

		GameObject doorHolderObj = new GameObject("DoorHolder");
		doorHolder = doorHolderObj.transform;
		doorHolder.parent = transform;
		doorHolder.position = transform.position;

		furthestPos.z = transform.position.z;

		if(!rigidbody)
		{
			gameObject.AddComponent(typeof(Rigidbody));
		}

		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;

		StartCoroutine(SetupRoom());
	}

	void FixedUpdate()
	{
		if(rigidbody)
		{
			rigidbody.WakeUp();
		}

		timeAlive += Time.deltaTime;
	}

	IEnumerator SetupRoom()
	{
		Vector3 roomSize = new Vector3(	Mathf.Ceil(Random.Range(boundsMinMax.x, boundsMinMax.y)), 
		                            	Mathf.Ceil(Random.Range(boundsMinMax.x, boundsMinMax.y)),
		          						Mathf.Ceil(Random.Range(boundsMinMax.x, boundsMinMax.y)));

		BoxCollider boxCollider = GetComponent<BoxCollider>();
		boxCollider.enabled = false;
		boxCollider.size = roomSize;

		// Get all objects in collision box
		// Save the position of the farthest away and a 2x1 box is the entrance
		// Delete all of them
		// Create walls along the edge of the collision

		yield return 0;

		boxCollider.enabled = true;

		roomWidth = (int)Mathf.Ceil(boxCollider.size.x);
		roomLength = (int)Mathf.Ceil(roomSize.y);

		if(furthestPos == Vector3.zero && netPath)
		{
			furthestPos = transform.position;
			furthestDir = netPath.transform.forward;
			transform.position = furthestPos + netPath.forward * roomSize.x/2.0f;
		}

		// Horizontal

		Vector3 roomFront = collider.bounds.min;
		roomFront = new Vector3(Mathf.Ceil(roomFront.x), Mathf.Ceil(roomFront.y), transform.position.z);

		Vector3 roomBack = roomFront + transform.up * roomSize.y;
		roomBack = new Vector3(Mathf.Ceil(roomBack.x), Mathf.Ceil(roomBack.y), transform.position.z);

		for(int i = 1; i < roomWidth; i++)
		{
			walls.Add(SpawnWall(roomFront + transform.right * i));
			walls.Add(SpawnWall(roomBack + transform.right * i));
		}

		// Vertical

		Vector3 roomLeft = collider.bounds.min;
		roomLeft = new Vector3(Mathf.Ceil(roomLeft.x), Mathf.Ceil(roomLeft.y), transform.position.z);

		Vector3 roomRight = roomLeft + transform.right * roomSize.x;
		roomRight = new Vector3(Mathf.Ceil(roomRight.x), Mathf.Ceil(roomRight.y), transform.position.z);

		for(int i = 0; i < roomLength + 1; i++)
		{
			walls.Add(SpawnWall(roomLeft + transform.up * i));
			walls.Add(SpawnWall(roomRight + transform.up * i));
		}

		Vector3 averageWallPos = Vector3.zero;
		foreach(GameObject wall in walls)
		{
			averageWallPos += wall.transform.position;
		}
		averageWallPos *= 1.0f/walls.Count;

		GameObject clearBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
		clearBox.layer = LayerMask.NameToLayer("Ignore Raycast");
		clearBox.transform.parent = transform;
		clearBox.transform.position = averageWallPos;
		clearBox.transform.localScale = new Vector3(roomSize.x, roomSize.y, 1);

		GameObject floorObj = WadeUtils.Instantiate(floorPrefab);
		floor = floorObj.transform;
		floor.parent = transform;
		averageWallPos.z = groundHeight;
		floor.position = averageWallPos;
		floor.localScale = new Vector3(roomSize.x - 1, roomSize.y - 1, 1);

		GameObject ceilingObj = WadeUtils.Instantiate(floorPrefab);
		ceilingObj.renderer.material.color = Color.black;
		ceiling = ceilingObj.transform;
		ceiling.parent = transform;
		averageWallPos.z = groundHeight - 3.0f;
		ceiling.position = averageWallPos;
		ceiling.localScale = new Vector3(roomSize.x - 1, roomSize.y - 1, 1);

		foreach(GameObject wall in walls)
		{
			wall.collider.enabled = false;
		}

		boxCollider.enabled = false;

		yield return new WaitForSeconds(0.5f);

		foreach(GameObject wall in walls)
		{
			wall.collider.enabled = true;
		}

		Destroy(clearBox);
		Destroy(rigidbody);

		MakeEntrances();

		// Put in office objects
		// Pick an appropriately size pattern
	}

	GameObject SpawnWall(Vector3 pos)
	{
		GameObject officeWall = WadeUtils.Instantiate(wallPrefab, pos, wallPrefab.transform.rotation);
		officeWall.transform.parent = wallHolder;
		officeWall.name = "OfficeWall";

		return officeWall;
	}

	void OnTriggerEnter(Collider col)
	{
		DestroyContents(col);
	}

	void OnTriggerStay(Collider col)
	{
		DestroyContents(col);
	}

	void MakeEntrances()
	{
		if(furthestDir == Vector3.right)
		{
			rayStart.x = transform.position.x;
		}
		else
		{
			rayStart.y = transform.position.y;
		}
		rayStart.z = -0.0f;

		GameObject currentOfficeObj = NetPathmakerManager.singleton.instance.worldManager.currentOffice;

		OfficeGenerator currentOffice = null;
		if(currentOfficeObj)
		{
			currentOffice = currentOfficeObj.GetComponent<OfficeGenerator>();
		}

		RaycastHit hit = WadeUtils.RaycastAndGetInfo(new Ray(rayStart, furthestDir), Mathf.Infinity);
		if(hit.transform)
		{
			// spawn entrance prefab
			// aim it correctly

			GameObject door = WadeUtils.Instantiate(doorPrefab);
			door.transform.parent = doorHolder;
			door.transform.position = hit.transform.position;
			door.transform.rotation = doorRot;
			door.transform.rotation.SetFromToRotation(door.transform.up, door.transform.forward);
			door.transform.localRotation *= Quaternion.Euler(90.0f, 0.0f, 180.0f);

			if(currentOffice && currentOffice.insideOffice)
			{
				door.transform.localRotation *= Quaternion.Euler(0.0f, 0.0f, 180.0f);
			}

			door.GetComponent<FlipTilePoints>().office = gameObject;

			Destroy(hit.transform.gameObject);
		}
		
		hit = WadeUtils.RaycastAndGetInfo(new Ray(rayStart, -furthestDir), Mathf.Infinity);
		if(hit.transform)
		{
			GameObject door = WadeUtils.Instantiate(doorPrefab);
			door.transform.parent = doorHolder;
			door.transform.position = hit.transform.position;
			door.transform.rotation = doorRot * Quaternion.Euler(0.0f, 0.0f, 180.0f);
			door.transform.rotation.SetFromToRotation(door.transform.up, door.transform.forward);
			door.transform.localRotation *= Quaternion.Euler(-90.0f, 0.0f, 180.0f); 

			if(currentOffice && currentOffice.insideOffice)
			{
				door.transform.localRotation *= Quaternion.Euler(0.0f, 0.0f, 180.0f);
			}

			door.GetComponent<FlipTilePoints>().office = gameObject;

			Destroy(hit.transform.gameObject);
		}
	}

	void DestroyContents(Collider col)
	{
		if(!col.transform.CompareTag("Player"))
		{
			float colDistance = Vector3.Distance(transform.position, col.transform.position);
			if(colDistance > Vector3.Distance(transform.position + furthestPos, transform.position))
			{
				furthestPos = col.transform.position;
				furthestDir = col.transform.forward;
				rayStart = furthestPos;
				doorRot = col.transform.rotation;
			}

			//foreach wall
			//compare to find closest wall
			//make that a doorway

			Destroy(col.gameObject);
		}
		else
		{
			Destroy(col.gameObject);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.transform.GetComponent<NetPathmaker>())
		{
			Destroy(col.gameObject);
		}
		else
		{
			OfficeGenerator otherOffice = col.transform.GetComponent<OfficeGenerator>();
			if(otherOffice && otherOffice.timeAlive <= timeAlive)
			{
				Destroy(col.gameObject);
			}
		}
	}

	public void RespawnInternet(Transform entrance)
	{
		Transform[] doors = doorHolder.GetComponentsInChildren<Transform>();

		if(doors.Length > 1)
		{
			foreach(Transform t in doorHolder.GetComponentsInChildren<Transform>())
			{
				if(t != doorHolder && t != entrance)
				{
					Vector3 spawnPos = t.position;
					spawnPos.z = transform.position.z;

					Quaternion makerRot = t.localRotation * Quaternion.Euler(-90.0f, 0.0f, 0.0f);
					NetPathmakerManager.singleton.instance.SpawnNetPathmaker(spawnPos + t.up, makerRot);

					return;
				}
			}
		}
		else
		{

		}
	}

	void OnDrawGizmos()
	{
		if(furthestPos != Vector3.zero)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawCube(furthestPos, Vector3.one);
		}

		for(int i = 0; i < roomWidth * 2 - 2; i++)
		{
			if(walls[i])
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawCube(walls[i].transform.position, Vector3.one);
			}
		}

		Gizmos.color = Color.green;
		Gizmos.DrawCube(rayStart, Vector3.one);

		Gizmos.DrawLine(rayStart, rayStart + rayDir * 3.0f);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(rayStart, rayStart - rayDir * 3.0f);
	}
}
