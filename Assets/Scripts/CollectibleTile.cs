using UnityEngine;
using System.Collections;

public class CollectibleTile : MonoBehaviour
{
	void OnTriggerStay2D()
	{
		ScoreManager.singleton.instance.AddScore();
		ItemText.singleton.instance.DisplayText("You have stolen some government secrets!");
		Destroy(gameObject);
	}
}
