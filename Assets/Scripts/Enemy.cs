using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Waypoint currentDestination;
    public WaypointManager waypointManager;
    private int currentIndexWaypoint = 0;
    public float speed = 1;
    public int health;
    public int value;

    public void Initialize(WaypointManager waypointManager, int health, int value)
    {
        this.waypointManager = waypointManager;
        this.health = health;
        this.value = value;
        GetNextWaypoint();
        transform.position = currentDestination.transform.position; // Move to WP0
        GetNextWaypoint();
    }

    public bool Damage(int amt)
    {
        Debug.Log("Damaged " + gameObject.name + " by " + amt + " points.");
        health -= amt;
        if (health <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    void Update()
    {
        Vector3 direction = currentDestination.transform.position - transform.position;
        if (direction.magnitude < .2f)
        {
            GetNextWaypoint();
        }

        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void GetNextWaypoint()
    {
        currentDestination = waypointManager.GetNeWaypoint(currentIndexWaypoint);
        currentIndexWaypoint++;
    }
}
