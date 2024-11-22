//Abdullah Shahir Bin Zulmajdi 24000112
//DANISH SAFIN BIN ZULKARNAIN 24000149
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class DroneNetworkController : MonoBehaviour
{
    [SerializeField] private TMP_InputField searchField;
    [SerializeField] private TMP_InputField startDroneField;
    [SerializeField] private TMP_InputField endDroneField;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI timeText;
    public Flock flock;

    private string searchDroneCsvPath = "search_network.csv";
    private string findShortestPathCsvPath = "path_network.csv";

    private void Start()
    {
        // Create CSV files with headers if they don't exist
        if (!File.Exists(searchDroneCsvPath))
        {
            File.WriteAllText(searchDroneCsvPath, "SearchQuery,TimeMicroseconds\n");
        }

        if (!File.Exists(findShortestPathCsvPath))
        {
            File.WriteAllText(findShortestPathCsvPath, "StartDrone,EndDrone,TimeMicroseconds\n");
        }
    }

    // Search for a drone in the network
    public void OnSearchDrone()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        string droneName = searchField.text;
        Drone targetDrone = flock.GetDroneByName(droneName); // Get the target drone by name

        if (targetDrone != null)
        {
            Vector3 position;
            bool found = false;

            // Search in both partition networks
            found = flock.partition1NetworkCommunication.SearchDrone(targetDrone, targetDrone, out position) ||
                    flock.partition2NetworkCommunication.SearchDrone(targetDrone, targetDrone, out position);

            // Update the UI with the result
            resultText.text = "Found Drone: " + targetDrone.name +
                           " at position: " + targetDrone.transform.position.ToString() +
                           $", Temperature: {targetDrone.Temperature}°C" +
                           $", Surrounding Temperature: {targetDrone.SurroundingTemperature:F2}°C" +
                           $", Humidity: {targetDrone.SurroundingHumidity:F2}%";
        }
        else
        {
            resultText.text = "Drone not found by name!";
        }

        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);
        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";

        // Write to CSV
        File.AppendAllText(searchDroneCsvPath, $"{droneName},{elapsedMicroseconds}\n");
    }

    public void OnFindShortestPath()
    {
        string startName = startDroneField.text.Trim();
        string endName = endDroneField.text.Trim();

        Drone startDrone = flock.GetDroneByName(startName);
        Drone endDrone = flock.GetDroneByName(endName);

        if (startDrone == null || endDrone == null)
        {
            resultText.text = "Invalid drone names!";
            UnityEngine.Debug.LogWarning($"Invalid input: start='{startName}', end='{endName}'");
            return;
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Determine which partition the start and end drones belong to
        List<Drone> path = null;

        if (flock.partition1NetworkCommunication.ContainsDrone(startDrone) &&
            flock.partition1NetworkCommunication.ContainsDrone(endDrone))
        {
            path = flock.partition1NetworkCommunication.FindShortestPath(startDrone, endDrone);
        }
        else if (flock.partition2NetworkCommunication.ContainsDrone(startDrone) &&
                flock.partition2NetworkCommunication.ContainsDrone(endDrone))
        {
            path = flock.partition2NetworkCommunication.FindShortestPath(startDrone, endDrone);
        }
        else
        {
            resultText.text = "Drones are in different partitions!";
            UnityEngine.Debug.LogWarning("Cannot find a path across different partitions.");
            stopwatch.Stop();
            return;
        }

        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

        if (path != null && path.Count > 0)
        {
            resultText.text = "Path: " + string.Join(" -> ", path.Select(drone => drone.name));
        }
        else
        {
            resultText.text = "No path found!";
        }

        // Write to CSV
        File.AppendAllText(findShortestPathCsvPath, $"{startName},{endName},{elapsedMicroseconds}\n");

        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";
    }
}
