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
		transform.position = new Vector3((int)transform.position.x, (int)transform.position.y, transform.position.z);
		Setup(GetComponent<Pattern>());
	}

	void Setup(Pattern pattern)
	{
		for(int i = 0; i < pattern.patternSquare.Length; i++)
		{
			if(pattern.patternSquare[i])
			{
				GameObject pickup = Instantiate(pickupObj) as GameObject;

				Vector3 pickupPos = transform.position;
				pickupPos += new Vector3(i % pattern.width, -i/pattern.width);
				pickupPos.z = transform.position.z;

				pickup.transform.position = pickupPos;
			}
		}
	}

	void OnDrawGizmos()
	{
		if(drawGrid && !Application.isPlaying)
		{
			Pattern pattern = GetComponent<Pattern>();

			for(int i = 0; i < pattern.patternSquare.Length; i++)
			{
				if(pattern.patternSquare[i])
				{
					Vector3 pickupPos = transform.position;
					pickupPos += new Vector3(i % pattern.width, -i/pattern.width);
					pickupPos.z = transform.position.z;

					Gizmos.color = drawColor;
					Gizmos.DrawCube(pickupPos, Vector3.one);
				}
			}
		}
	}
}
