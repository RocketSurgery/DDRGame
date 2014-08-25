using UnityEngine;
using System.Collections;

public class PatternMaker : MonoBehaviour 
{
	[SerializeField] GameObject[] decorationPrefabs;

	[SerializeField] GameObject pointsPrefab;

	OfficeGenerator officeGen;

	public bool justSpawned = true;
	bool spawnedDecoration = false;

	int initSpawnNum;
	public int spawnNum = 3;

	[SerializeField] float turnChance;
	[SerializeField] float decorationChance;

	[SerializeField] float maxTimeAlive = 1.0f;
	float timeAlive = 0.0f;

	float maxDistance = 0.0f;

	void Awake()
	{

	}
	
	public void Setup(OfficeGenerator office)
	{
		initSpawnNum = spawnNum;

		officeGen = office;
		maxDistance = (office.roomWidth + office.roomLength)/2.0f;
	}

	void Update () 
	{
		if(timeAlive > maxTimeAlive || spawnNum == 0)
		{
			Destroy(gameObject);
		}

		float spawnChance = Random.Range(0.0f, 1.0f);
		if(!justSpawned && spawnChance < turnChance)
		{
			if(initSpawnNum - spawnNum > 1 && spawnChance < decorationChance)
			{
				GameObject decorationPrefab = decorationPrefabs[Random.Range(0, decorationPrefabs.Length)];
				GameObject decoration = WadeUtils.Instantiate(decorationPrefab, 
				                                              transform.position, 
				                                              decorationPrefab.transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 4) * 90.0f));
				decoration.transform.parent = officeGen.decorationHolder;

				spawnedDecoration = true;
			}
			else
			{
				float rotAmount = (Random.Range(0.0f, 1.0f) > 0.5f) ? 90.0f : -90.0f;

				GameObject patternMakerObj = WadeUtils.Instantiate(gameObject, 
				                                                   transform.position, 
				                                                   transform.rotation * Quaternion.Euler(0.0f, 0.0f, rotAmount));
				patternMakerObj.transform.parent = transform.parent;

				PatternMaker patternMaker = patternMakerObj.GetComponent<PatternMaker>();
				patternMaker.Setup(officeGen);
				patternMaker.justSpawned = true;
				patternMaker.spawnNum = spawnNum - 1;
				spawnNum--;
			}
		}

		if(!justSpawned && !spawnedDecoration)
		{
			GameObject points = WadeUtils.Instantiate(pointsPrefab, 
			                                          transform.position + Vector3.forward * 0.9f, 
			                                          pointsPrefab.transform.rotation);
			points.transform.parent = officeGen.pointHolder;
		}

		transform.position += transform.up;

		RaycastHit hit = WadeUtils.RaycastAndGetInfo(new Ray(transform.position, transform.up), 1.0f);
		if(hit.transform || !officeGen || !officeGen.clearBox.collider.bounds.Contains(transform.position))
		{
			Destroy(gameObject);
		}

		timeAlive += Time.deltaTime;
		justSpawned = false;
		rigidbody2D.WakeUp();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(!col.transform.CompareTag("FlipTile") && 
		   !col.transform.GetComponent<PatternMaker>() &&
		   !col.transform.CompareTag("Decoration"))
		{
			//Debug.Log("Collision " + col.transform.name);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(!col.transform.CompareTag("FlipTile") && 
		   !col.transform.GetComponent<PatternMaker>() &&
		   !col.transform.CompareTag("Floppy"))
		{
			//Debug.Log("Trigger " + col.transform.name);
			Destroy(gameObject);
		}
	}
}
