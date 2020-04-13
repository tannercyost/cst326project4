using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


[RequireComponent(typeof(NavMeshAgent))]
public class SmartEnemy : MonoBehaviour, iMovement
{
    private NavMeshAgent agent;

    [SerializeField] private float maxLife = 100;
    [SerializeField] private float currentLife = 0;

    [SerializeField] private HealthBar healthBar;

    public UnityEvent enemyDeath;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Initialize(WaypointManager waypointManager)
    {
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(waypointManager.waypoints[0].transform.position, out closestHit, 100f, NavMesh.AllAreas))
        {
            transform.position = closestHit.position;
            StartCoroutine(Spawn(waypointManager));
        }

    }

    IEnumerator Spawn(WaypointManager waypointManager)
    {
        yield return new WaitForSeconds(.2f);
        agent.enabled = true;
        agent.SetDestination(waypointManager.waypoints[waypointManager.waypoints.Length - 1].transform.position);
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


    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public UnityEvent DeathEvent()
    {
        return enemyDeath;
    }
}
