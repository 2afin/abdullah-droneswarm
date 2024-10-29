using System;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class Flock : MonoBehaviour
{
    public Drone agentPrefab;
    List<Drone> agents = new List<Drone>();
    public FlockBehavior behavior;

    [Range(10, 5000)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    public DroneCommunication partition1Communication = new DroneCommunication();
    public DroneCommunication partition2Communication = new DroneCommunication();


    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            Drone newAgent = Instantiate(
                agentPrefab,
                UnityEngine.Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
                transform
            );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this, i);
            agents.Add(newAgent);
        }
    }
    //MUHAMMAD FAIQ HAKEEM BIN FARID 24000054
    void PartitionDronesByTemperature()
    {
        if (agents.Count == 0) return;

        int pivotTemperature = agents[0].Temperature; //temperature of the first drone will act as pivot
        List<Drone> lower = new List<Drone>();
        List<Drone> higher = new List<Drone>();

        foreach (Drone drone in agents)
        {
            if (drone.Temperature <= pivotTemperature) //sorting drones based on th pivots temperature
            {
                lower.Add(drone);
                drone.GetComponent<SpriteRenderer>().color = Color.blue;
                partition1Communication.AddDrone(drone);
            }
            else
            {
                higher.Add(drone);
                drone.GetComponent<SpriteRenderer>().color = Color.red;
                partition2Communication.AddDrone(drone);
            }
        }
    }

    void Update()
    {
        Drone[] drones = agents.ToArray();

        
        PartitionDronesByTemperature();

        foreach (Drone agent in agents)
        {
            // Decide on next movement direction
            List<Transform> context = GetNearbyObjects(agent);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(Drone agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}