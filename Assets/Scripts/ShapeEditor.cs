using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(ShapeData)), CanEditMultipleObjects]
public class ShapeEditor : Editor
{
    bool disabledShapes = false;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SerializedProperty shapeProp = serializedObject.FindProperty("shape");
        SerializedProperty sizeProp = serializedObject.FindProperty("size");

        EditorGUILayout.PropertyField(shapeProp);

        // start of size change
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(sizeProp);
        bool sizeChanged = EditorGUI.EndChangeCheck();

        if (shapeProp.enumValueIndex == (int)ShapeData.shapeType.Cube) // warning for cube size
        {
            if (sizeProp.floatValue > 2f)
            {
                EditorGUILayout.HelpBox("The cubes' sizes cannot be bigger than 2!", MessageType.Warning);
            }
        }
        else if (shapeProp.enumValueIndex == (int)ShapeData.shapeType.Sphere) // warning for sphere size
        {
            if (sizeProp.floatValue < 1f)
            {
                EditorGUILayout.HelpBox("The spheres' radius cannot be smaller than 1!", MessageType.Warning);
            }
        }

        serializedObject.ApplyModifiedProperties();

        // if the sizes actually changed apply those changes
        if (sizeChanged)
        {
            foreach (Object obj in targets)
            {
                ShapeData data = (ShapeData)obj;
                data.transform.localScale = Vector3.one * data.size;
            }
        }
        // end of size change

        if (GUILayout.Button("Select all " + Selection.activeGameObject.GetComponent<ShapeData>().shape.ToString() + "s"))
        {
            ShapeData.shapeType ShapeName = Selection.activeGameObject.GetComponent<ShapeData>().shape;

            ButtonFunction(ShapeName);

        }

        if (GUILayout.Button("Deselect All"))
        {
            Selection.activeGameObject = null;
        }

        // Change button color if shape is disabled or not
        Color buttonColor;
        if (disabledShapes)
        {
            buttonColor = Color.red;
        }
        else
        {
            buttonColor = Color.green;
        }
        GUI.backgroundColor = buttonColor;

        if (GUILayout.Button("Enable/Disable " + Selection.activeGameObject.GetComponent<ShapeData>().shape.ToString() + "s", GUILayout.Height(40)))
        {
            // Get the shape type and disable/enable all shapes of that type
            ShapeData.shapeType shape = Selection.activeGameObject.GetComponent<ShapeData>().shape;
            OnEnableButton(shape);
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

        // Debug warning if no shapes present
        if (allType.Count == 0)
        {
            Debug.Log("No " + ShapeName.ToString() + "s found.");
            return;
        }


        GameObject[] shapeSelect = allType.Select(shape => shape.gameObject).ToArray(); // Gets gameObjects of all ShapeData cubes or spheres
        Selection.objects = shapeSelect; // Select the chosen shape
    }

    void OnEnableButton(ShapeData.shapeType shapeName)
    {
        
        ShapeData[] allShapes = GameObject.FindObjectsByType<ShapeData>(FindObjectsInactive.Include); // Finds all objects with ShapeData script
        List<ShapeData> allShapeType = new List<ShapeData>(); // List of shapes
        foreach (ShapeData data in allShapes)
        {
            // Check if the ShapeData shapeType is the chosen shape or not
            if (data.shape == shapeName)
            {
                allShapeType.Add(data);
            }
        }

        GameObject[] shapes = allShapeType.Select(shape => shape.gameObject).ToArray(); // Gets gameObjects of all chosen shape

        // Modify variables and enable/disable gameObjects
        disabledShapes = !disabledShapes;
        foreach (GameObject shape in shapes)
        {
            shape.SetActive(!disabledShapes);
        }
    }
}
