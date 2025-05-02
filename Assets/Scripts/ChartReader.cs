using System;
using System.IO;
using UnityEngine;

public class ChartReader : MonoBehaviour
{
    public string filePath = "Assets/Resources/chart.txt";

    public string[] ReadFile()
    {
        if (File.Exists(filePath))
        {
            string chartContent = File.ReadAllText(filePath);
            string[] chartLines = chartContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            return chartLines;
        }
        else
        {
            Debug.LogError("Song file not found.");
            return Array.Empty<string>();
        }
    }
}
