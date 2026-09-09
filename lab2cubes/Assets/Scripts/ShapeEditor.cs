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
            ShapeData.shapeType ShapeName = ShapeData.shapeType.cube;

            ButtonFunction(ShapeName);
            
        }

        if (GUILayout.Button("Select all Spheres"))
        {
            ShapeData.shapeType ShapeName = ShapeData.shapeType.sphere;
            ButtonFunction(ShapeName);
  
        }
    }
    void ButtonFunction(ShapeData.shapeType ShapeName)
    {
        ShapeData[] allShapes = GameObject.FindObjectsByType<ShapeData>(); // Finds all objects with ShapeData script
        List<ShapeData> allType = new List<ShapeData>(); // List of cubes or spheres

        foreach (ShapeData data in allShapes)
        {
            // Check if the ShapeData shapeType is a the selected shape
            if (data.shape == ShapeName)
            {
                allType.Add(data);
            }
        }
        /*if (allType == null)
        {
            Debug.Log("No cubes found.");
        }*/
        

        GameObject[] shapeSelect = allType.Select(shape => shape.gameObject).ToArray(); // Gets gameObjects of all ShapeData cubes or spheres
        Selection.objects = shapeSelect; // Select the chosen shape
    }
}
