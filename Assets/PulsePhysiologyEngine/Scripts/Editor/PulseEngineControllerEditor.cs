/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PulseEngineController), true)]
public abstract class PulseEngineControllerEditor : Editor
{
  SerializedProperty driverProperty;  // serialized pulse driver

  void OnEnable()
  {
    driverProperty = serializedObject.FindProperty("driver");
  }

  public override void OnInspectorGUI()
  {
    // Ensure serialized properties are up to date with component
    serializedObject.Update();

    // Draw UI to select data source
    EditorGUILayout.PropertyField(driverProperty, new GUIContent("Engine driver"));

    // Display error message if data source is invalid then return
    var driver = driverProperty.objectReferenceValue as PulseEngineDriver;
    if (driver == null)
    {
      serializedObject.ApplyModifiedProperties();
      EditorGUILayout.LabelField("Error", "Input driver missing");
      return;
    }

    // Show the default inspector property editor without the script field
    DrawPropertiesExcluding(serializedObject, "m_Script");

    DrawProperties();

    // Apply modifications back to the component
    serializedObject.ApplyModifiedProperties();
  }

  internal abstract void DrawProperties();
}
