/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PulseActionOnClick), true)]
public class PulseActionOnClickEditor : PulseEngineControllerEditor
{
  override internal void DrawProperties()
  {
    GUI.enabled = Application.isPlaying;

    var obj = target as PulseActionOnClick;
    if (GUILayout.Button("Run Action"))
    {
      obj.RunAction();
    }
  }
}
