/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Component that reads and exposes pulse data from a CSV file
[ExecuteInEditMode]
public class PulseCSVReader : PulseDataSource
{
  public TextAsset CSVInput;          // Input CSV file Asset
  public float timeElapsedAtStart;    // Simulation time at start

  List<string[]> CSVValues;           // Partially parsed CSVInput
  int lineId;                         // Next line (time point) to read from
  float startTime;                    // Application time at start


  // MARK: Monobehavior methods

  // Called when application or editor opens
  void Awake()
  {
    // Create our data container
    data = ScriptableObject.CreateInstance<PulseData>();

    // Compute data fields from existing CSV header
    ComputeHeaders();
  }

  // Called when the inspector inputs are modified
  void OnValidate()
  {
    if (Application.isPlaying)
      return;

    // Compute data fields from new CSV header
    ComputeHeaders();
  }

  // Called at the first frame when the component is enabled
  void Start()
  {
    // Ensure we only read data if the application is playing
    // and there is a CSV input to read from
    if (!Application.isPlaying || CSVInput == null)
      return;

    startTime = Time.time;

    // Check that we have some values in the CSV file
    string[] lines = CSVInput.text.Split('\n');
    if (lines == null || lines.Length < 2)
      return;

    // Allocate space for data times and values
    data.timeStampList = new DoubleList(lines.Length - 1);
    int numberOfColumns = data.fields.Length;
    data.valuesTable = new List<DoubleList>(numberOfColumns);
    for (int columnId = 0; columnId < numberOfColumns; ++columnId)
      data.valuesTable.Add(new DoubleList(lines.Length - 1));

    // Go through all lines to store string content in CSVValues
    CSVValues = new List<string[]>(lines.Length - 1);
    for (int lineId = 1; lineId < lines.Length; ++lineId)
    {
      // Split line into data values
      var lineData = lines[lineId].Trim();
      var values = lineData.Split(',');

      // Allocate space for data values (just once, first line)
      if (values.Length != numberOfColumns)
        continue;

      // Fill values
      CSVValues.Add(values);
    }
  }

  // Called before every frame
  void Update()
  {
    // Ensure we only broadcast data if the application is playing
    // and there are CSVValues to read from
    if (!Application.isPlaying || CSVValues == null)
      return;

    // Empty data container from previous frame
    data.timeStampList.Clear();
    foreach (DoubleList column in data.valuesTable)
      column.Clear();

    // Ensure data left to read
    if (lineId >= CSVValues.Count)
      return;

    // Figure out current time and data point time
    var currentTime = Time.time;
    var lineValues = CSVValues[lineId];
    string dataTimeStr = lineValues[0];
    float dataTime = float.Parse(dataTimeStr);

    // Broadcast all data points that precede the current time,
    // taking the component start time and the simulation time at start
    // into account
    while (dataTime - timeElapsedAtStart <= currentTime - startTime)
    {
      // Append time and values to data container
      data.timeStampList.Add(dataTime);
      for (int columnId = 0; columnId < lineValues.Length; ++columnId)
      {
        string valueStr = lineValues[columnId];
        float value = float.Parse(valueStr);
        data.valuesTable[columnId].Add(value);
      }

      // Stop now if ther is not more data left to read
      if (++lineId >= CSVValues.Count)
        return;

      // Check the next data point time
      lineValues = CSVValues[lineId];
      dataTimeStr = lineValues[0];
      dataTime = float.Parse(dataTimeStr);
    }
  }


  // MARK: Custom methods

  // Utility function to generate an array of data field names
  // from the input CSV file headers
  void ComputeHeaders()
  {
    // Ensure we have a CSV file
    if (CSVInput == null)
    {
      data.fields = null;
      return;
    }

    // Ensure there are headers in the file
    string[] lines = CSVInput.text.Split('\n');
    if (lines == null || lines.Length <= 0)
    {
      data.fields = null;
      return;
    }

    // Get values in headers
    string firstLineData = lines[0].Trim();
    data.fields = firstLineData.Split(',');

    // Fix backslash in EditorGUILayout.Popup
    for (uint headerId = 0; headerId < data.fields.Length; ++headerId)
    {
      string header = data.fields[headerId];
      data.fields[headerId] = header.Replace("(", " (").Replace("/", "\u2215");
    }
  }
}
