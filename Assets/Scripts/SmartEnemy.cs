using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


[RequireComponent(typeof(NavMeshAgent))]
public class SmartEnemy : MonoBehaviour, iMovement
{
    private NavMeshAgent agent;
    [SerializeField] private AudioSource oof;
    [SerializeField] private float maxLife;
    [SerializeField] private float currentLife;
    [SerializeField] private GameManager gm;
    [SerializeField] private HealthBar healthBar;

    private bool immune;

    public UnityEvent enemyDeath;
    void Awake()
    {
        immune = false;
        agent = GetComponent<NavMeshAgent>();
        oof = GetComponent<AudioSource>();
        
        currentLife = maxLife;
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

    public void DamageEnemy(float damage)
    {
        if (!immune)
        {
            currentLife -= damage;
            healthBar.UpdateHealthBar(currentLife, maxLife);



            if (currentLife <= 0) //We are dead ... need to do book keeping
            {
                immune = true;
                oof.Play(0);
                GameObject a = gameObject.transform.GetChild(0).gameObject;
                a.SetActive(false);
                StartCoroutine(death());
            }
        }

    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
        enemyDeath.Invoke();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Weapon")
        {
            Bullet bulletThatHitMe = collision.transform.GetComponent<Bullet>();
            DamageEnemy(bulletThatHitMe.Damage);
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
