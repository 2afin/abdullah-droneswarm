using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Drone : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int Temperature { set; get; } = 0;
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    public Drone NextDrone { get; set; }

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Temperature = (int) (Random.value * 100);
    }

    public void Initialize(Flock flock, int i)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    public void SetColor(Color color)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
}