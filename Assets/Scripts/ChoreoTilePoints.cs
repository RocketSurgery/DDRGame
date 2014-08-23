using UnityEngine;
using System.Collections;

public class ChoreoTilePoints : MonoBehaviour {

    public GameObject scoreManagerObject;
    ScoreManager scoreManagerComponent;

	// Use this for initialization
	void Start () {
        scoreManagerComponent = scoreManagerObject.GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        scoreManagerComponent.AddScore();
    }
}
