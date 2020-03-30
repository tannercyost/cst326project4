using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.parent.rotation = Camera.main.transform.rotation;
        // transform.parent.transform.LookAt(transform.position - Camera.main.transform.position);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.localScale = new Vector3(transform.localScale.x - 1f, transform.localScale.y, transform.localScale.z);
        }
    }
}
