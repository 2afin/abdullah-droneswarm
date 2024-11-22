//DANISH SAFIN BIN ZULKARNAIN 24000149
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Drone : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    // Current drone's temperature and position-based properties
    public int Temperature { get; set; } = 0;

    // Flock-related properties
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    public Drone NextDrone { get; set; }

    // Environmental attributes influenced by position
    public float SurroundingTemperature { get; private set; }
    public float SurroundingHumidity { get; private set; }

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Randomize temperature
        Temperature = (int)(Random.value * 100);

        // Update surrounding environmental values
        UpdateSurroundingEnvironment();
    }

    public void Initialize(Flock flock, int i)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;

        // Update surrounding environmental values after movement
        UpdateSurroundingEnvironment();
    }

    public void SetColor(Color color)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    private void UpdateSurroundingEnvironment()
    {
        // Simulate surrounding temperature and humidity based on position
        Vector3 position = transform.position;

        // Example formulas for calculating temperature and humidity
        SurroundingTemperature = Mathf.PerlinNoise(position.x * 0.1f, position.y * 0.1f) * 50 + 10; // Range: 10 to 60
        SurroundingHumidity = Mathf.PerlinNoise(position.x * 0.2f, position.y * 0.2f) * 100;       // Range: 0 to 100
    }
}
