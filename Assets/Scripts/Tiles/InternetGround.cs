using UnityEngine;
using System.Collections;

public class InternetGround : MonoBehaviour 
{
	[SerializeField] Transform target;
	[SerializeField] float minDistance;
	[SerializeField] float maxDistance;

	Vector3 defaultPos;

	float defaultHeight;
	[SerializeField] float minHeight;
	float useMinHeight;

	Vector3 defaultScale;

	void Start () 
	{
		if(!target)
		{
			target = Player.singleton.instance.transform;
		}

		defaultPos = transform.parent.position;
		defaultHeight = transform.position.z;
		defaultScale = transform.localScale;
	}

	void Update () 
	{
		if(target && Vector3.Distance(target.position, transform.position) < 35.0f)
		{
			float targetDistance = Vector3.Distance(target.position, defaultPos);
			useMinHeight = defaultHeight - minHeight;

			targetDistance = (targetDistance - minDistance)/(maxDistance - minDistance);

			transform.position = new Vector3(transform.position.x, 
			                                 transform.position.y, 
			                                 Mathf.Lerp(defaultHeight, useMinHeight, targetDistance));

			transform.localScale = Vector3.Lerp( defaultScale, Vector3.zero, targetDistance);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow - new Color(0.0f, 0.0f, 0.0f, 0.7f);
		Gizmos.DrawCube(defaultPos, Vector3.one);
	}
}
