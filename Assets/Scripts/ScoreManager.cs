using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    uint score = 0;

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
        Debug.Log("New Score " + score);
    }
}
