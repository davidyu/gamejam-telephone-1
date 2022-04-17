using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Powerup : MonoBehaviour
{
    // customize me!
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // do something to power them up!
            collider.gameObject.GetComponent<PlayerController>().bombExplosionRange++;
            Destroy(gameObject, 0f);
        }
    }
}
