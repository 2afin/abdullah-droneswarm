//Abdullah Shahir Bin Zulmajdi 24000112
//Muhammad Faiq Hakeem Bin Farid 24000054
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using TMPro;
using System.IO;

public class DroneController : MonoBehaviour
{
    public Flock flock;
    public DroneCommunication droneCommunication;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TextMeshProUGUI timeText;

    private string searchCsvPath = "linked_search.csv";
    private string destructCsvPath = "linked_destruct.csv";

    private void Start()
    {
        // Initialize CSV files if they don't exist
        if (!File.Exists(searchCsvPath))
        {
            File.WriteAllText(searchCsvPath, "DroneName,OperationTime(microseconds)\n");
        }

        if (!File.Exists(destructCsvPath))
        {
            File.WriteAllText(destructCsvPath, "DroneName,OperationTime(microseconds)\n");
        }
    }

    public void SearchDrone()
    {
        string droneName = inputField.text;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Drone foundDrone = flock.partition1Communication.FindDrone(droneName);
        if (foundDrone != null)
        {
            resultsText.text = "Found Drone: " + foundDrone.name +
                               " at position: " + foundDrone.transform.position.ToString() +
                               $", Temperature: {foundDrone.Temperature}°C" +
                               $", Surrounding Temperature: {foundDrone.SurroundingTemperature:F2}°C" +
                               $", Humidity: {foundDrone.SurroundingHumidity:F2}%";
        }
        else
        {
            foundDrone = flock.partition2Communication.FindDrone(droneName);
            if (foundDrone != null)
            {
                resultsText.text = "Found Drone: " + foundDrone.name +
                                   " at position: " + foundDrone.transform.position.ToString() +
                                   $", Temperature: {foundDrone.Temperature}°C" +
                                   $", Surrounding Temperature: {foundDrone.SurroundingTemperature:F2}°C" +
                                   $", Humidity: {foundDrone.SurroundingHumidity:F2}%";
            }
            else
            {
                resultsText.text = "Drone not found!";
            }
        }

        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);
        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";

        // Write to CSV
        File.AppendAllText(searchCsvPath, $"{droneName},{elapsedMicroseconds}\n");
    }

    public void SelfDestructDrone()
    {
        string droneName = inputField.text;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        flock.partition1Communication.SelfDestruct(droneName);
        flock.partition2Communication.SelfDestruct(droneName);

        resultsText.text = droneName + " has been self-destructed!";

        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";

        // Write to CSV
        File.AppendAllText(destructCsvPath, $"{droneName},{elapsedMicroseconds}\n");
    }
}
