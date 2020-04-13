using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    void Fire()
    {     
        Bullet shotJustFired = Instantiate(bullet, transform.position, transform.parent.rotation).GetComponent<Bullet>();
    }
}
