//Dzuriyat Ilhan bin Mohd Ridzuan 24000061
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class FPSDisplay : MonoBehaviour
{
    float fps;
    float updateTimer = 0.2f;

    [SerializeField] TextMeshProUGUI fpsTitle;

    private List<float> fpsValues = new List<float>(); // List to store FPS values
    private float collectionDuration = 30f; // Duration to collect FPS values
    private bool isCollectionComplete = false; // To ensure the calculation happens only once

    private void updateFPS()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer < 0)
        {
            updateTimer = 0.2f;

            fps = 1f / Time.unscaledDeltaTime;
            fpsTitle.text = "FPS: " + Mathf.Round(fps);

            if (!isCollectionComplete)
            {
                fpsValues.Add(fps); // Add current FPS to the list
            }
        }
    }

    void Update()
    {
        if (!isCollectionComplete)
        {
            collectionDuration -= Time.deltaTime;

            if (collectionDuration <= 0)
            {
                CalculateAndSaveAverageFPS();
                isCollectionComplete = true;
            }
        }

        updateFPS();
    }

    private void CalculateAndSaveAverageFPS()
    {
        float totalFPS = 0f;

        foreach (float value in fpsValues)
        {
            totalFPS += value;
        }

        float averageFPS = totalFPS / fpsValues.Count;

        // Save the average FPS to a CSV file
        string filePath = Application.dataPath + "/AverageFPS.csv";
        string csvContent = "Average FPS\n" + averageFPS.ToString("F2");
        File.WriteAllText(filePath, csvContent);

        Debug.Log($"Average FPS calculated and saved to {filePath}");
    }
}
