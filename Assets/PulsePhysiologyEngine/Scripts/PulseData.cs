/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using System.Collections.Generic;
using UnityEngine;

// Serializable List<double> wrapper
// https://answers.unity.com/questions/289692/serialize-nested-lists.html
[System.Serializable]
public class DoubleList
{
  public List<double> list;
  public DoubleList()
  {
    list = new List<double>();
  }
  public DoubleList(int capacity)
  {
    list = new List<double>(capacity);
  }
  public void Clear()
  {
    list.Clear();
  }
  public void Add(double value)
  {
    list.Add(value);
  }
  public void Set(int index, double value)
  {
    list[index] = value;
  }
  public double Get(int index)
  {
    return list[index];
  }
  public int Count
  {
    get
    {
      return list.Count;
    }
  }
  public bool IsEmpty()
  {
    return Count == 0;
  }
}

// Data container for Pulse vitals
public class PulseData : ScriptableObject
{
  public string[] fields;             // name of the data fields
  public DoubleList timeStampList;     // list of time points
  public List<DoubleList> valuesTable; // table holding a value for each time point for each data field
}
