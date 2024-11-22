//DANISH SAFIN BIN ZULKARNAIN 24000149
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class Flock : MonoBehaviour
{
    public Drone agentPrefab;
    public List<Drone> agents = new List<Drone>();
    
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
    public DroneBTCommunication partition1btCommunication = new DroneBTCommunication();
    public DroneBTCommunication partition2btCommunication = new DroneBTCommunication();
    public DroneNetworkCommunication partition1NetworkCommunication = new DroneNetworkCommunication();
    public DroneNetworkCommunication partition2NetworkCommunication = new DroneNetworkCommunication();
 
 

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
    void PartitionDronesByTemperature()
    {
        if (agents.Count == 0) return;

        int pivotTemperature = agents[0].Temperature; // Use the first drone's temperature as pivot

        partition1Communication.Clear();
        partition2Communication.Clear();
        partition1btCommunication.Clear();
        partition2btCommunication.Clear();
        partition1NetworkCommunication.Clear();
        partition2NetworkCommunication.Clear();

        foreach (Drone drone in agents)
        {
            bool isLower = drone.Temperature <= pivotTemperature;
        
            // set color once and assign to the right partition
            drone.GetComponent<SpriteRenderer>().color = isLower ? Color.blue : Color.red;

            if (isLower)
            {
                partition1Communication.AddDrone(drone);
                partition1btCommunication.AddDrone(drone);
                partition1NetworkCommunication.AddDrone(drone);
            }
            else
            {
                partition2Communication.AddDrone(drone);
                partition2btCommunication.AddDrone(drone);
                partition2NetworkCommunication.AddDrone(drone);
            }
        }
    }

    public Drone GetDroneByName(string name)
    {
        return agents.Find(drone => drone.name == name);
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