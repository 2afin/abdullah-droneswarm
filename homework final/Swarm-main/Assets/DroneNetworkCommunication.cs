//DZURIYAT ILHAN BIN MOHD RIDZUAN 24000061
//AHMAD AQIL FAHMI BIN AHMAD NOR 24000235
//Muhammad Faiq Hakeem Bin Farid 24000054
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneNetworkCommunication
{
    private Dictionary<Drone, List<Drone>> network = new Dictionary<Drone, List<Drone>>();

    // Add a drone to the network and connect it with all other drones
    public void AddDrone(Drone drone)
    {
        if (!network.ContainsKey(drone))
        {
            network[drone] = new List<Drone>();
        }

        foreach (var existingDrone in network.Keys)
        {
            if (existingDrone != drone)
            {
                network[drone].Add(existingDrone);
                network[existingDrone].Add(drone); // Connect both ways
            }
        }
    }

    // Perform BFS to find a drone in the network and return its position
    public bool SearchDrone(Drone startDrone, Drone targetDrone, out Vector3 position)
    {
        var visited = new HashSet<Drone>();
        var queue = new Queue<Drone>();
        var parent = new Dictionary<Drone, Drone>();

        queue.Enqueue(startDrone);
        visited.Add(startDrone);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == targetDrone)
            {
                position = current.transform.position; // Correctly return the position
                return true;
            }

            foreach (var neighbor in network[current])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    parent[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        position = Vector3.zero;
        return false;
    }

    // Find the shortest path from one drone to another (BFS)
    public List<Drone> FindShortestPath(Drone startDrone, Drone endDrone)
    {
        if (startDrone == null || endDrone == null || !network.ContainsKey(startDrone))
            return new List<Drone>(); // Return empty path for invalid input

        if (startDrone == endDrone)
            return new List<Drone> { startDrone }; // Handle start and end being the same

        var visited = new HashSet<Drone>();
        var queue = new Queue<Drone>();
        var parent = new Dictionary<Drone, Drone>();
        var path = new List<Drone>();

        queue.Enqueue(startDrone);
        visited.Add(startDrone);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            Debug.Log($"Current Drone: {current.name}");

            if (current == endDrone)
            {
                // Reconstruct path
                while (current != null)
                {
                    path.Add(current);
                    parent.TryGetValue(current, out current);
                }
                path.Reverse(); // Reverse to get path from start to end
                Debug.Log($"Path Found: {string.Join(" -> ", path.Select(drone => drone.name))}");
                return path;
            }

            if (network.TryGetValue(current, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                {
                    if (visited.Add(neighbor))
                    {
                        parent[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        Debug.Log("No path found.");
        return path; // Empty path if no path is found
    }

    // Clear the network
    public void Clear()
    {
        network.Clear();
    }

    public bool ContainsDrone(Drone drone)
    {
        return network.ContainsKey(drone); // Assumes `network` is a dictionary of drones in the partition
    }

}
