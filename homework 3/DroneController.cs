using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using TMPro;

public class DroneController : MonoBehaviour
{
    public Flock flock;
    public DroneCommunication droneCommunication;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button searchButton;
    [SerializeField] private Button destructButton;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TextMeshProUGUI timeText;

    //AHMAD AQIL FAHMI BIN AHMAD NOR 24000235
    public void SearchDrone()
    {
        string droneName = inputField.text;
        Stopwatch stopwatch = new Stopwatch(); 
        stopwatch.Start(); 

        Drone foundDrone = flock.partition1Communication.FindDrone(droneName);
        if (foundDrone != null)
        {
            resultsText.text = "Found Drone: " + foundDrone.name + " at position: " + foundDrone.transform.position.ToString();
        }
        else
        {
            resultsText.text = "Drone not found!";
        }

        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";
    }
    //DZURIYAT ILHAN BIN MOHD RIDZUAN 24000061
    public void SelfDestructDrone()
    {
        string droneName = inputField.text;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        flock.partition1Communication.SelfDestruct(droneName);
        resultsText.text = droneName + " has been self-destructed!";

        stopwatch.Stop();
        long elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

        timeText.text = $"Operation Time: {elapsedMicroseconds:F4} microseconds";
    }
}
