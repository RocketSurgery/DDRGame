using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Pattern))]
public class PatternRoom : MonoBehaviour 
{
	[SerializeField] GameObject pickupObj;
	[SerializeField] Color drawColor = Color.white;
	[SerializeField] bool drawGrid = false;
	
	void Awake()
	{
		transform.position = new Vector3((int)transform.position.x, (int)transform.position.y, 0.0f);
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
				pickupPos += new Vector3(i % pattern.size, -(int)i/pattern.size);

				pickup.transform.position = pickupPos;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnDrawGizmos()
	{
		if(drawGrid)
		{
			Pattern pattern = GetComponent<Pattern>();

			for(int i = 0; i < pattern.patternSquare.Length; i++)
			{
				if(pattern.patternSquare[i])
				{
					Vector3 pickupPos = transform.position + new Vector3(-1.0f, 1.0f) * ((float)pattern.size)/2.0f;
					pickupPos += new Vector3(i % pattern.size, -(int)i/pattern.size);

					Gizmos.color = drawColor;
					Gizmos.DrawCube(pickupPos, Vector3.one);
				}
			}
		}
	}
}
