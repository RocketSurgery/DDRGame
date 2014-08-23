﻿using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

	public GameObject world1;
	public GameObject world2;

	public void FlipWorlds()
	{
		world1.SetActive(!world1.activeSelf);
		world2.SetActive(!world2.activeSelf);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			FlipWorlds();
		}
	}
}