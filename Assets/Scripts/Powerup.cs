using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Powerup : MonoBehaviour
{
    public AudioClip powerupClip;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // do something to power them up!
            collider.gameObject.GetComponent<PlayerController>().bombExplosionRange++;
            AudioSource.PlayClipAtPoint(powerupClip, transform.position);
            Destroy(gameObject, 0f);
        }
    }
}
