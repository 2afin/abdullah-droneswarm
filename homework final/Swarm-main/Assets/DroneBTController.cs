//Abdullah Shahir Bin Zulmajdi 24000112
//Muhammad Faiq Hakeem Bin Farid 24000054
//Ahmad Aqil Fahmi bin Ahmad Nor 24000235
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using TMPro;
using System.IO;

public class DroneBTController : MonoBehaviour
{
    public Flock flock;

    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private TextMeshProUGUI timeText;

    private string searchByTemperatureCsvPath = "binary_temp.csv";
    private string searchByNameCsvPath = "binary_search.csv";
    private string selfDestructCsvPath = "binary_destruct.csv";

    private void Start()
    {
        // Initialize CSV files with headers if they don't exist
        InitializeCsv(searchByTemperatureCsvPath, "Temperature,Time(microseconds)\n");
        InitializeCsv(searchByNameCsvPath, "DroneName,Time(microseconds)\n");
        InitializeCsv(selfDestructCsvPath, "DroneName,Time(microseconds)\n");
    }

    private void InitializeCsv(string path, string header)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, header);
        }
    }

    public void SearchByTemperature()
    {
        int temperature = int.Parse(searchInputField.text);
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Drone drone = flock.partition1btCommunication.FindDroneByTemperature(temperature) ?? 
                      flock.partition2btCommunication.FindDroneByTemperature(temperature);

        if (drone != null)
        {
            outputText.text = "Found Drone: " + drone.name + 
                           " at position: " + drone.transform.position.ToString() + 
                           $", Temperature: {drone.Temperature}°C" + 
                           $", Surrounding Temperature: {drone.SurroundingTemperature:F2}°C" + 
                           $", Humidity: {drone.SurroundingHumidity:F2}%";
        }
        else
        {
            outputText.text = "Drone not found.";
        }

        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";

        // Log timing to CSV
        File.AppendAllText(searchByTemperatureCsvPath, $"{temperature},{elapsedMicroseconds}\n");
    }

    public void SearchByName()
    {
        string name = searchInputField.text;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Drone drone = flock.partition1btCommunication.FindDroneExhaustive(name) ??
                      flock.partition2btCommunication.FindDroneExhaustive(name);

        if (drone != null)
        {
            outputText.text = "Found Drone: " + drone.name + 
                           " at position: " + drone.transform.position.ToString() + 
                           $", Temperature: {drone.Temperature}°C" + 
                           $", Surrounding Temperature: {drone.SurroundingTemperature:F2}°C" + 
                           $", Humidity: {drone.SurroundingHumidity:F2}%";
        }
        else
        {
            outputText.text = "Drone not found.";
        }

        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";

        // Log timing to CSV
        File.AppendAllText(searchByNameCsvPath, $"{name},{elapsedMicroseconds}\n");
    }

    public void SelfDestruct()
    {
        string name = searchInputField.text;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        flock.partition1btCommunication.SelfDestruct(name);
        flock.partition2btCommunication.SelfDestruct(name);

        outputText.text = $"{name} has been self-destructed!";

        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";

        // Log timing to CSV
        File.AppendAllText(selfDestructCsvPath, $"{name},{elapsedMicroseconds}\n");
    }
}
