using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(ShapeData)),CanEditMultipleObjects]
public class ShapeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Select all Cubes"))
        {
            ShapeData[] allShapes = GameObject.FindObjectsByType<ShapeData>(); // Finds all objects with ShapeData script
            List<ShapeData> allCubes = new List<ShapeData>(); // List of cubes
            foreach (ShapeData data in allShapes)
            {
                // Check if the ShapeData shapeType is a cube or not
                if (data.shape == ShapeData.shapeType.cube)
                {
                    allCubes.Add(data);
                }
            }

            GameObject[] cubes = allCubes.Select(shape => shape.gameObject).ToArray(); // Gets gameObjects of all ShapeData cubes
            Selection.objects = cubes; // Select cubes
        }
    }
}
