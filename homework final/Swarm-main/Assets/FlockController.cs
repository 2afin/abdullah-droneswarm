//Abdullah Shahir Bin Zulmajdi 24000112
//Dzuriyat Ilhan Bin Mohd Ridzuan 24000061
//Muhammad Faiq Hakeem Bin Farid 24000054
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlockController : MonoBehaviour
{
    public Flock flock; // Reference to the Flock script
    public Scrollbar sizeScrollbar; // UI Scrollbar to adjust flock size
    public TMP_Text sizeText; // TextMeshPro UI element to display the size

    [Range(10, 5000)]
    public int minSize = 10;
    [Range(10, 5000)]
    public int maxSize = 5000;

    private int currentFlockSize;
    private const float agentDensity = 0.08f; // Match the AgentDensity value in the Flock class

    void Start()
    {
        // Validate components
        if (flock == null)
        {
            Debug.LogError("Flock reference is not assigned in FlockController.");
            return;
        }

        if (sizeScrollbar == null)
        {
            Debug.LogError("Scrollbar reference is not assigned in FlockController.");
            return;
        }

        if (sizeText == null)
        {
            Debug.LogError("TextMeshPro reference is not assigned in FlockController.");
            return;
        }

        // Initialize current flock size
        currentFlockSize = flock.startingCount;

        // Update the UI
        UpdateFlockSizeText();

        // Set scrollbar value based on current size
        sizeScrollbar.value = (float)(currentFlockSize - minSize) / (maxSize - minSize);

        // Add listener to the scrollbar programmatically
        sizeScrollbar.onValueChanged.AddListener(OnFlockSizeChanged);
    }

    void OnFlockSizeChanged(float value)
    {
        // Map scrollbar value to a flock size
        int newFlockSize = Mathf.RoundToInt(Mathf.Lerp(minSize, maxSize, value));

        // If the size changes, update the flock
        if (newFlockSize != currentFlockSize)
        {
            AdjustFlockSize(newFlockSize);
        }
    }

    void AdjustFlockSize(int newSize)
    {
        int difference = newSize - currentFlockSize;

        if (difference > 0)
        {
            // Add new drones
            for (int i = 0; i < difference; i++)
            {
                Drone newAgent = Instantiate(
                    flock.agentPrefab,
                    Random.insideUnitCircle * newSize * agentDensity,
                    Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                    flock.transform
                );
                newAgent.name = "Agent " + (flock.agents.Count);
                newAgent.Initialize(flock, flock.agents.Count);
                flock.agents.Add(newAgent);
            }
        }
        else if (difference < 0)
        {
            // Remove excess drones
            for (int i = 0; i < Mathf.Abs(difference); i++)
            {
                Drone droneToRemove = flock.agents[flock.agents.Count - 1];
                Destroy(droneToRemove.gameObject);
                flock.agents.RemoveAt(flock.agents.Count - 1);
            }
        }

        currentFlockSize = newSize;

        // Update the UI text
        UpdateFlockSizeText();
    }

    void UpdateFlockSizeText()
    {
        sizeText.text = $"Flock Size: {currentFlockSize}";
    }
}
