//MUHAMMAD FAIQ HAKEEM BIN FARID 24000054
//ABDULLAH SHAHIR BIN ZULMAJDI 24000112
//DANISH SAFIN BIN ZULKARNAIN 24000149
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using TMPro;

public class DroneBTController : MonoBehaviour
{
    public Flock flock;

    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private Button searchByTemperatureButton;
    [SerializeField] private Button searchByNameButton;
    [SerializeField] private Button selfDestructButton;
    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private TextMeshProUGUI timeText;


    public void SearchByTemperature()
    {
        int temperature = int.Parse(searchInputField.text);
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Drone drone = flock.partition1btCommunication.FindDroneByTemperature(temperature) ?? 
                      flock.partition2btCommunication.FindDroneByTemperature(temperature);

        if (drone != null)
        {
            outputText.text = $"Found Drone: {drone.name} is at position {drone.transform.position}.";
        }
        else
        {
            outputText.text = "Drone not found.";
        }
        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";

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
            outputText.text = $"Found Drone: {drone.name} is at position {drone.transform.position}.";
        }
        else
        {
            outputText.text = "Drone not found.";
        }
        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";

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

    }
}
