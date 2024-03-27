using Dreamteck.Splines;
using SustainTheStrain.Units;
using UnityEditor;
using UnityEngine;

namespace SustainTheStrain.Editor.Units
{
    [CustomEditor(typeof(RoadSign))]
    public class RoadSignEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            RoadSign roadSign = (RoadSign)target;
            var node = roadSign.GetComponent<Node>();

            DrawDefaultInspector();

            GUILayout.Space(10);

            if (roadSign.Guides == null || roadSign.Guides.Length != node.GetConnections().Length)
                roadSign.Guides = new bool[node.GetConnections().Length];

            for (int i = 0; i < node.GetConnections().Length; i++)
            {
                EditorGUILayout.BeginHorizontal("box");

                roadSign.Guides[i] = EditorGUILayout.Toggle(roadSign.Guides[i]);
                EditorGUILayout.LabelField($"{node.GetConnections()[i].spline.name} at point {node.GetConnections()[i].pointIndex}");

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}