using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GIFSpawner : MonoBehaviour 
{
	[SerializeField] GameObject[] gifPrefabs;
	List<GameObject> gifs = new List<GameObject>();

	[SerializeField] int numToSpawn;
	[SerializeField] float spawnRange;
	[SerializeField] float zPos;

	void Awake()
	{
		SpawnGifs();
	}

	void Update()
	{
		if(Vector2.Distance(Player.singleton.instance.transform.position, transform.position) > spawnRange)
		{
			DeleteGifs();
			transform.position = Player.singleton.instance.transform.position;
			SpawnGifs();
		}
	}

	public void DeleteGifs()
	{
		GameObject[] gifObjs = gifs.ToArray();
		gifs.Clear();

		for(int i = 0; i < gifObjs.Length; i++)
		{
			Destroy(gifObjs[i]);
		}
	}

	public void SpawnGifs()
	{
		for(int i = 0; i < numToSpawn; i++)
		{
			GameObject gif = WadeUtils.Instantiate(gifPrefabs[Random.Range(0, gifPrefabs.Length)],
			                                       Player.singleton.instance.transform.position + (Vector3)Random.insideUnitCircle * spawnRange,
			                                       gifPrefabs[0].transform.rotation);
			gif.transform.SetPositionZ(zPos);
			gif.transform.localScale = Random.insideUnitSphere + Vector3.one * 0.5f;
			gif.transform.parent = transform;
			gifs.Add(gif);

			gif.GetComponent<SpriteAnimation>().SetRandomFrame();
		}
	}
}
