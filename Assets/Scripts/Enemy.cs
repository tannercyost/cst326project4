using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, iMovement
{
  public Waypoint currentDestination;
  public WaypointManager waypointManager;
  [SerializeField] private HealthBar healthBar;
  [SerializeField] private float maxLife = 100;
  [SerializeField] private float currentLife = 0;

  public UnityEvent enemyDeath;

  private int currentIndexWaypoint = 0;
  public float speed = 1;

  public void Initialize(WaypointManager waypointManager)
  {
    currentLife = maxLife;
    healthBar.UpdateHealthBar(currentLife, maxLife);

    this.waypointManager = waypointManager;
    GetNextWaypoint();
    transform.position = currentDestination.transform.position; // Move to WP0
    GetNextWaypoint();
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
    if (currentIndexWaypoint < waypointManager.waypoints.Length)
    {
      currentDestination = waypointManager.GetNeWaypoint(currentIndexWaypoint);
      currentIndexWaypoint++;
    }
    
  }


  public GameObject GetGameObject()
  {
    return gameObject;
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.transform.tag == "Weapon")
    {
      Bullet bulletThatHitMe = collision.transform.GetComponent<Bullet>();
      currentLife -= bulletThatHitMe.Damage;
      
      healthBar.UpdateHealthBar(currentLife, maxLife);

      if (currentLife <= 0) //We are dead ... need to do book keeping
      {
        //update purse
        enemyDeath.Invoke();
        Destroy(gameObject);                                                                         

      }
      Destroy(bulletThatHitMe.gameObject);
      
    }
  }

  public UnityEvent DeathEvent()
  {
    return enemyDeath;
  }
}
