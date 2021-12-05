/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using UnityEngine;
using UnityEngine.UI;

// Pulse data consumer which displays the data value as a number
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(UnityEngine.UI.Text))]
public class PulseDataNumberRenderer : PulseDataConsumer
{
  public string prefix = "";      // String placed before value
  public string suffix = "";      // String placed after value
  public float multiplier = 1;    // = displayed value / data value
  public uint decimals = 0;       // Number of decimals displayed
  [Range(0f, 120f)]
  public float frequency = 0;     // Update rate to display new value

  Text textRenderer;              // Text component to update
  float previousTime = 0;         // Used to match the requested frequency


  // MARK: Monobehavior methods

  // Called at the first frame when the component is enabled
  void Start()
  {
    textRenderer = gameObject.GetComponent<UnityEngine.UI.Text>();
  }


  // MARK: PulseDataConsumer methods

  // Consume pulse data
  override internal void UpdateFromPulse(DoubleList times, DoubleList values)
  {
    // Update display at a certain frequency
    float currentTime = Time.time;
    if (frequency > 0 && currentTime < previousTime + 1 / frequency)
      return;

    previousTime = currentTime;

    // Only display last value from list
    int lastIndex = values.Count - 1;
    double dataValue = values.Get(lastIndex);

    // Apply multiplier, decimals truncating
    dataValue *= multiplier;
    string decimalCode = "F" + decimals.ToString();
    string dataString = dataValue.ToString(decimalCode);

    // Update displayed value with prefix and suffix
    textRenderer.text = prefix + dataString + suffix;
  }
}
