using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    void Fire()
    {
        //Debug.Log("Pow");
        Vector3 spawnPos = new Vector3(transform.position.x, -0.4895477f, transform.position.z);
        
        Bullet shotJustFired = Instantiate(bullet, transform.position, transform.parent.rotation).GetComponent<Bullet>();
        //shotJustFired.myWeapon = transform.parent.GetComponent<WeaponFSM>();
    }
}
