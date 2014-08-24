using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Pattern))]
public class PatternEditor : Editor
{
	Pattern pattern;
	float cubeSize = 3.0f;

	void OnEnable()
	{
		pattern = (Pattern)target;
	}
	
	public override void OnInspectorGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Pattern Width");
		pattern.width = (int)GUILayout.HorizontalSlider(pattern.width, 4.0f, 15.0f, GUILayout.Width(120));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Pattern Width");
		pattern.height = (int)GUILayout.HorizontalSlider(pattern.height, 4.0f, 15.0f, GUILayout.Width(120));

		if(pattern.width * pattern.height != pattern.patternSquare.Length)
		{
			pattern.patternSquare = new bool[pattern.width * pattern.height];
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Pattern Squares");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Clear"))
		{
			pattern.patternSquare = new bool[pattern.width * pattern.height];
		}
		GUILayout.EndHorizontal();

		for(int i = 0; i < pattern.height; i++)
		{
			GUILayout.BeginHorizontal();
			for(int j = 0; j < pattern.width; j++)
			{
				pattern.patternSquare[i * pattern.width + j] = GUILayout.Toggle(pattern.patternSquare[i * pattern.width + j], "", GUILayout.Width(13));
			}
			GUILayout.EndHorizontal();
		}
		SceneView.RepaintAll();
	}
}
