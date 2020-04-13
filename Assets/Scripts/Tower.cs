using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float maxLife = 100;
    private float curLife;
    [SerializeField] private HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        curLife = maxLife;
        healthBar.UpdateHealthBar(curLife, maxLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            curLife -= 10;
            healthBar.UpdateHealthBar(curLife, maxLife);
            Destroy(collision.gameObject);
        }
        if (curLife <= 0)
        {
            // lose game
            Debug.Log("You lost.");
        }
    }
}
