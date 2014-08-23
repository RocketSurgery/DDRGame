using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Grid))]
public class PatternEditor : Editor
{
	Vector3 mousePos;
	Grid grid;

	void OnEnable()
	{
		grid = (Grid)target;
		SceneView.onSceneGUIDelegate = GridUpdate;
	}

	void GridUpdate(SceneView sceneView)
	{
		Event e = Event.current;

		if (e.isKey && e.character == 'p')
		{
			Debug.Log(mousePos);

			GameObject obj;
			Object prefab = PrefabUtility.GetPrefabParent(Selection.activeObject);
		
			if (prefab)
			{
				obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
				Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x/grid.width)*grid.width + grid.width/2.0f,
				                              Mathf.Floor(mousePos.y/grid.height)*grid.height + grid.height/2.0f, 0.0f);

				obj.transform.position = mousePos;
				SceneView.RepaintAll();
			}
		}

		mousePos = Camera.current.ScreenToWorldPoint(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));

	}
	
	public override void OnInspectorGUI()
	{
		GUILayout.BeginHorizontal();

		GUILayout.Label("Width");
		grid.width = EditorGUILayout.FloatField(grid.width, GUILayout.Width(70));

		GUILayout.Label("Height");
		grid.height = EditorGUILayout.FloatField(grid.height, GUILayout.Width(70));

		GUILayout.EndHorizontal();

		SceneView.RepaintAll();
	}
}
