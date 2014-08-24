using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    static uint score = 0;

	public static uint Score
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
