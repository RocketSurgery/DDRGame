using UnityEngine;
using System.Collections;

public class ChoreoTilePoints : MonoBehaviour {

    GameObject scoreManagerObject;
    ScoreManager scoreManagerComponent;

	// Use this for initialization
	void Start () {
        scoreManagerObject = GameObject.Find("ScoreManager");
        scoreManagerComponent = scoreManagerObject.GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        scoreManagerComponent.AddScore();
        Destroy(gameObject);
    }
}
