using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OfficeGenerator : MonoBehaviour 
{
	[SerializeField] Vector2 boundsMinMax = new Vector2(6, 15);

	public Transform netPath;
	Vector3 furthestCol = Vector3.zero;

	[SerializeField] GameObject wallPrefab;

	List<GameObject> walls = new List<GameObject>();

	void Awake()
	{
		if(!rigidbody)
		{
			gameObject.AddComponent(typeof(Rigidbody));
			rigidbody.useGravity = false;
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

		StartCoroutine(SetupRoom());
	}

	IEnumerator SetupRoom()
	{
		GetComponent<BoxCollider>().size = new Vector3(	Mathf.Ceil(Random.Range(boundsMinMax.x, boundsMinMax.y)), 
		                                               	Mathf.Ceil(Random.Range(boundsMinMax.x, boundsMinMax.y)),
		          							 			Mathf.Ceil(Random.Range(boundsMinMax.x, boundsMinMax.y)));

		Debug.Log("Before");
		Debug.Break();

		// Get all objects in collision box
		// Save the position of the farthest away and a 2x1 box is the entrance
		// Delete all of them
		// Create walls along the edge of the collision

		yield return 0;

		int roomWidth = (int)Mathf.Ceil(collider.bounds.size.x);
		int roomLength = (int)Mathf.Ceil(collider.bounds.size.y);

		if(furthestCol == Vector3.zero && netPath)
		{
			furthestCol = netPath.position;
			transform.position = furthestCol + netPath.forward * collider.bounds.extents.x/2.0f;
		}

		// Horizontal

		Vector3 roomFront = collider.bounds.min;
		roomFront = new Vector3(Mathf.Ceil(roomFront.x), Mathf.Ceil(roomFront.y), transform.position.z);

		Vector3 roomBack = roomFront + transform.up * collider.bounds.size.y;
		roomBack = new Vector3(Mathf.Ceil(roomBack.x), Mathf.Ceil(roomBack.y), transform.position.z);

		for(int i = 1; i < roomWidth; i++)
		{
			walls.Add(SpawnWall(roomFront + transform.right * i));
			walls.Add(SpawnWall(roomBack + transform.right * i));
		}

		// Vertical

		Vector3 roomLeft = collider.bounds.min;
		roomLeft = new Vector3(Mathf.Ceil(roomLeft.x), Mathf.Ceil(roomLeft.y), transform.position.z);

		Vector3 roomRight = roomLeft + transform.right * collider.bounds.size.x;
		roomRight = new Vector3(Mathf.Ceil(roomRight.x), Mathf.Ceil(roomRight.y), transform.position.z);

		for(int i = 0; i < roomLength + 1; i++)
		{
			walls.Add(SpawnWall(roomLeft + transform.up * i));
			walls.Add(SpawnWall(roomRight + transform.up * i));
		}

		foreach(GameObject wall in walls)
		{
			wall.collider.enabled = false;
		}

		yield return 0;

		Destroy(rigidbody);

		foreach(GameObject wall in walls)
		{
			wall.collider.enabled = true;
		}


		// Pick a random 2x1 space along the opposite wall to be the exit
		// Put in ground objects
		// Put in office objects
		// Pick an appropriately size pattern
	}

	GameObject SpawnWall(Vector3 pos)
	{
		GameObject officeWall = WadeUtils.Instantiate(wallPrefab, pos, wallPrefab.transform.rotation);
		officeWall.transform.parent = transform;
		officeWall.name = "OfficeWall";

		return officeWall;
	}

	void OnTriggerEnter(Collider col)
	{
		if(!col.transform.CompareTag("Player"))
		{
			float colDistance = Vector3.Distance(transform.position, col.transform.position);
			if( colDistance > Vector3.Distance(furthestCol, transform.position))
			{
				furthestCol = col.transform.position;
			}

			//foreach wall
				//compare to find closest wall
				//make that a doorway

			Destroy(col.gameObject);
		}
	}
}
