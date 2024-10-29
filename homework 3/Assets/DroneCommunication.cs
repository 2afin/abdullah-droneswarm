 //DANISH SAFIN BIN ZULKARNAIN 24000149
using UnityEngine;

public class DroneCommunication
{
    private DroneNode head;

    //linked list
    private class DroneNode
    {
        public Drone drone;
        public DroneNode next;

        public DroneNode(Drone drone)
        {
            this.drone = drone;
            this.next = null;
        }
    }

    public void AddDrone(Drone drone)
    {
        if (head == null)
        {
            head = new DroneNode(drone);
        }
        else
        {
            DroneNode current = head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = new DroneNode(drone);
        }
    }

    public Drone FindDrone(string name)
    {
        DroneNode current = head;
        while (current != null)
        {
            if (current.drone.name == name)
            {
                return current.drone;
            }
            current = current.next;
        }
        return null;
    }

    public void SelfDestruct(string name)
    {
        DroneNode current = head;
        DroneNode previous = null;

        while (current != null)
        {
            if (current.drone.name == name)
            {
                if (previous == null)
                {
                    head = current.next;
                }
                else
                {
                    previous.next = current.next;
                }

                current.drone.gameObject.SetActive(false);
                return;
            }
            previous = current;
            current = current.next;
        }
    }
}
