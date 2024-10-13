//MUHAMMAD FAIQ HAKEEM BIN FARID 24000054
//ABDULLAH SHAHIR BIN ZULMAJDI 24000112

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Drone : MonoBehaviour
{
    public int Temperature { get; private set; } = 0; // Temperature of the drone
    public float SurroundingTemperature { get; private set; } = 0.0f; // Surrounding temperature

    public float Humidity { get; private set; } = 0.0f; // Humidity in percentage
    public float PollutionLevel { get; private set; } = 0.0f; // Pollution level indicator

    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        UpdateEnvironmentalData(); // Initialize environmental data
    }

    private void Update()
    {
        // Update environmental data every frame (or at a specified interval)
        UpdateEnvironmentalData();

        // Simulate changing drone temperature for demonstration purposes
        Temperature = (int)(Random.value * 100);
    }

    private void UpdateEnvironmentalData()
    {
        // Simulate reading surrounding environmental data
        SurroundingTemperature = Random.Range(-10.0f, 40.0f); // Random surrounding temperature in Celsius
        Humidity = Random.value * 100; // Random value for humidity
        PollutionLevel = Random.value; // Random value for pollution level (0 to 1)
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        // Detect obstacles and avoid them
        DetectObstacles();

        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void DetectObstacles()
    {
        // Raycast to detect obstacles in front of the drone
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1f);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle")) // Assuming trees/obstacles have the tag "Obstacle"
        {
            // Implement obstacle avoidance logic here(change direction)
            Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal);
            Move(avoidanceDirection);
        }
    }

    public void ReportData()
    {
        //eport collected data
        Debug.Log($"Drone {name} - Drone Temperature: {Temperature}, Surrounding Temperature: {SurroundingTemperature}, Humidity: {Humidity}, Pollution Level: {PollutionLevel}");
        
    }
}
