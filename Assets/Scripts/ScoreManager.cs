﻿using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	public static SingletonBehaviour<ScoreManager> singleton = new SingletonBehaviour<ScoreManager>();

    uint score = 0;

	public uint Score
	{
		get
		{
			return score;
		}
	}

    public uint scorePerTile = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddScore()
    {
        score += scorePerTile;
    }
}
