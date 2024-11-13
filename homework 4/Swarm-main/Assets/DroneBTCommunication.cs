//DZURIYAT ILHAN BIN MOHD RIDZUAN 24000061
//AHMAD AQIL FAHMI BIN AHMAD NOR 24000235
//DANISH SAFIN BIN ZULKARNAIN 24000149
using UnityEngine;

public class DroneBTCommunication
{
    private DroneBTNode root;

    // Binary Tree Node
    private class DroneBTNode
    {
        public Drone drone;
        public DroneBTNode left;
        public DroneBTNode right;

        public DroneBTNode(Drone drone)
        {
            this.drone = drone;
            this.left = null;
            this.right = null;
        }
    }

    // Add drone to the binary tree based on the temperature
    public void AddDrone(Drone drone)
    {
        if (root == null)
        {
            root = new DroneBTNode(drone);
        }
        else
        {
            AddDroneRecursive(root, drone);
        }
    }

    private void AddDroneRecursive(DroneBTNode node, Drone drone)
    {
        if (drone.Temperature < node.drone.Temperature)
        {
            if (node.left == null)
                node.left = new DroneBTNode(drone);
            else
                AddDroneRecursive(node.left, drone);
        }
        else
        {
            if (node.right == null)
                node.right = new DroneBTNode(drone);
            else
                AddDroneRecursive(node.right, drone);
        }
    }

    // Find drone by temperature (BST search)
    public Drone FindDroneByTemperature(int temperature)
    {
        return FindDroneByTemperatureRecursive(root, temperature);
    }

    private Drone FindDroneByTemperatureRecursive(DroneBTNode node, int temperature)
    {
        if (node == null)
        {
            return null;
        }

        if (node.drone.Temperature == temperature)
        {
            return node.drone;
        }

        if (temperature < node.drone.Temperature)
        {
            return FindDroneByTemperatureRecursive(node.left, temperature);
        }
        else
        {
            return FindDroneByTemperatureRecursive(node.right, temperature);
        }
    }

    // Exhaustive search for drone based on name (or any other attribute)
    public Drone FindDroneExhaustive(string name)
    {
        return FindDroneExhaustiveRecursive(root, name);
    }

    private Drone FindDroneExhaustiveRecursive(DroneBTNode node, string name)
    {
        if (node == null)
        {
            return null;
        }

        if (node.drone.name == name)
        {
            return node.drone;
        }

        // Search both sides for name
        Drone foundDrone = FindDroneExhaustiveRecursive(node.left, name);
        if (foundDrone != null)
        {
            return foundDrone;
        }
        return FindDroneExhaustiveRecursive(node.right, name);
    }

    // Send self-destruct message to a drone by name
    public void SelfDestruct(string name)
    {
        root = SelfDestructRecursive(root, name);
    }

    private DroneBTNode SelfDestructRecursive(DroneBTNode node, string name)
    {
        if (node == null)
        {
            return null;
        }

        if (node.drone.name == name)
        {
            node.drone.gameObject.SetActive(false);  // Self-destruct (hide the drone)
            return null;  // Remove the drone from the tree
        }

        // Recursively search and remove
        node.left = SelfDestructRecursive(node.left, name);
        node.right = SelfDestructRecursive(node.right, name);
        return node;
    }

    public void Clear()
    {
        root = null;
    }
}
