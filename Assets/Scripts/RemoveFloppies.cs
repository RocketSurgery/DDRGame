using UnityEngine;
using System.Collections;

public class RemoveFloppies : MonoBehaviour 
{
	void OnTriggerStay2D(Collider2D col)
	{
		if(col.transform.CompareTag("Floppy"))
		{
			Destroy(col.gameObject);
		}
	}
}