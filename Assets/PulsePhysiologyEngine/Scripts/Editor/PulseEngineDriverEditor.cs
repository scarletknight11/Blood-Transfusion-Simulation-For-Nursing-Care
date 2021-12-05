/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(PulseEngineDriver), true)]
public class PulseEngineDriverEditor : Editor
{
  SerializedProperty stateFileProperty;   // serialized initial state file

  void OnEnable()
  {
    stateFileProperty = serializedObject.FindProperty("initialStateFile");
  }

  public override void OnInspectorGUI()
  {
    // Ensure serialized properties are up to date with component
    serializedObject.Update();

    // Draw UI to select initial state file
    EditorGUILayout.PropertyField(stateFileProperty,
                                  new GUIContent("Initial State File"));
    var state = stateFileProperty.objectReferenceValue as TextAsset;
    if (state == null)
    {
      string message = "A state file is required to initialize the " +
          "Pulse engine. You can find an example file in 'Data/states' " +
          "or generate one by running the Pulse engine and save a " +
          "state to file (Unity will only accept '.txt' files).";
      EditorGUILayout.HelpBox(message, MessageType.Warning);
      return;
    }

    // Show the default inspector property editor without the script field
    DrawPropertiesExcluding(serializedObject, "m_Script", "initialStateFile");

    // Apply modifications back to the component
    serializedObject.ApplyModifiedProperties();
  }
}
