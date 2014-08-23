using UnityEngine;
using System.Collections;

public class OfficeRoom : MonoBehaviour 
{
	[SerializeField] GameObject pickupObj;

	void Awake()
	{
		Setup(GetComponent<Pattern>());
	}

	void Setup(Pattern pattern)
	{
		for(int i = 0; i < pattern.patternSquare.Length; i++)
		{
			if(pattern.patternSquare[i])
			{
				GameObject pickup = Instantiate(pickupObj) as GameObject;

				Vector3 pickupPos = transform.position + new Vector3(-1.0f, 1.0f) * ((float)pattern.size)/2.0f;
                pickupPos += new Vector3(i % pattern.size + 0.5f, -(int)i / pattern.size + 0.5f);

				pickup.transform.position = pickupPos;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
