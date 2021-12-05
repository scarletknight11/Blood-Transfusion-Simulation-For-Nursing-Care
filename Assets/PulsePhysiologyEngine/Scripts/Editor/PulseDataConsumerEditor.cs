/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PulseDataConsumer), true)]
public class PulseDataConsumerEditor : Editor
{
  SerializedProperty sourceProp;          // serialized data source
  SerializedProperty dataFieldIndexProp;  // serialized data field index

  void OnEnable()
  {
    sourceProp = serializedObject.FindProperty("source");
    dataFieldIndexProp = serializedObject.FindProperty("dataFieldIndex");
  }

  public override void OnInspectorGUI()
  {
    // Ensure serialized properties are up to date with component
    serializedObject.Update();

    // Draw UI to select data source
    EditorGUILayout.PropertyField(sourceProp, new GUIContent("Data source"));

    // Display error message if data source is invalid then return
    var source = sourceProp.objectReferenceValue as PulseDataSource;
    if (source == null || source.data == null || source.data.fields == null)
    {
      dataFieldIndexProp.intValue = 0;
      serializedObject.ApplyModifiedProperties();
      EditorGUILayout.LabelField("Error",
                                 source == null ?
                                 "Data source missing" :
                                 "Data source could not generate valid data fields");
      return;
    }

    // Draw UI to select datafield
    string[] fields = source.data.fields;
    dataFieldIndexProp.intValue = Mathf.Clamp(dataFieldIndexProp.intValue,
                                              0,
                                              fields.Length - 1);
    dataFieldIndexProp.intValue = EditorGUILayout.Popup("Data field",
                                                        dataFieldIndexProp.intValue,
                                                        fields);

    // Show the default inspector property editor without the script field
    DrawPropertiesExcluding(serializedObject, "m_Script");

    // Apply modifications back to the component
    serializedObject.ApplyModifiedProperties();
  }
}
