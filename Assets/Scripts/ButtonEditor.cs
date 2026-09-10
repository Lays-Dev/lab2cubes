using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

//[CustomEditor(typeof(ShapeData)), CanEditMultipleObjects]
public class ButtonEditor : Editor
{
    bool disabledCubes = false;
    //bool disabledSpheres = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Color buttonColor;
        if (disabledCubes)
        {
            buttonColor = Color.red;
        }
        else
        {
            buttonColor = Color.green;
        }
        GUI.backgroundColor = buttonColor;

        if (GUILayout.Button("Enable/Disable Cube", GUILayout.Height(40)))
        {
            disabledCubes = !disabledCubes;
            ShapeData[] allShapes = GameObject.FindObjectsByType<ShapeData>(FindObjectsInactive.Include); // Finds all objects with ShapeData script
            List<ShapeData> allCubes = new List<ShapeData>(); // List of cubes
            foreach (ShapeData data in allShapes)
            {
                // Check if the ShapeData shapeType is a cube or not
                if (data.shape == ShapeData.shapeType.Cube)
                {
                    allCubes.Add(data);
                }
            }

            GameObject[] cubes = allCubes.Select(shape => shape.gameObject).ToArray(); // Gets gameObjects of all ShapeData cubes
            foreach (GameObject cube in cubes)
            {
                cube.SetActive(!disabledCubes);
            }
        }
    }
}
