using UnityEngine;
using System.Collections;

public class ChoreoTilePoints : MonoBehaviour {

    GameObject scoreManagerObject;
    ScoreManager scoreManagerComponent;
    float threshhold = 0.2f;

	// Use this for initialization
	void Start () {
        scoreManagerObject = GameObject.Find("ScoreManager");
        scoreManagerComponent = scoreManagerObject.GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if ( (collider.gameObject.transform.position - gameObject.transform.position).magnitude < threshhold )
        {
            scoreManagerComponent.AddScore();
            Destroy(gameObject);
        }
    }
}
