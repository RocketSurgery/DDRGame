using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Pattern))]
public class PatternEditor : Editor
{
	Pattern pattern;

	void OnEnable()
	{
		pattern = (Pattern)target;
	}
	
	public override void OnInspectorGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Pattern Width/Height");
		pattern.size = (int)GUILayout.HorizontalSlider(pattern.size, 4.0f, 15.0f, GUILayout.Width(120));

		if(pattern.size * pattern.size != pattern.patternSquare.Length)
		{
			pattern.patternSquare = new bool[pattern.size * pattern.size];
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Pattern Squares");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Clear"))
		{
			pattern.patternSquare = new bool[pattern.size * pattern.size];
		}
		GUILayout.EndHorizontal();

		for(int i = 0; i < pattern.size; i++)
		{
			GUILayout.BeginHorizontal();
			for(int j = 0; j < pattern.size; j++)
			{
				pattern.patternSquare[i * pattern.size + j] = GUILayout.Toggle(pattern.patternSquare[i * pattern.size + j], "", GUILayout.Width(13));
			}
			GUILayout.EndHorizontal();
		}
		SceneView.RepaintAll();
	}
}
