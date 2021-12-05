/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using UnityEngine;

// Abstract class for any pulse data source that holds a reference
// to the pulse data container
public abstract class PulseDataSource : MonoBehaviour
{
  [HideInInspector]
  public PulseData data;
}
