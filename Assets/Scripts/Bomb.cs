using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionCountdown = 2.0f;
    public float explosionDuration = 0.2f;
    public float explosionRange = 1;
    public LayerMask explosionMask;
    public GameObject explosion;

    private float timeTillExplosion;

    void Start()
    {
        timeTillExplosion = explosionCountdown;
        Invoke("Explode", explosionCountdown);
    }

    void Update()
    {
        timeTillExplosion -= Time.deltaTime;
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity); 
        StartCoroutine(ExplodeInDirection(Vector3.forward));
        StartCoroutine(ExplodeInDirection(Vector3.right));
        StartCoroutine(ExplodeInDirection(Vector3.back));
        StartCoroutine(ExplodeInDirection(Vector3.left));  
        Destroy(gameObject, explosionDuration);
    }

    IEnumerator ExplodeInDirection(Vector3 direction) 
    {
        for (int i = 1; i <= explosionRange; i++) 
        {
            RaycastHit hit; 
            Physics.Raycast(transform.position + new Vector3(0,.5f,0), direction, out hit, i, explosionMask); 

            if (explosion != null) 
            {
                Instantiate(explosion, transform.position + (i * direction), Quaternion.identity); 
            }

            if (hit.collider)
            {
                // this destroys whatever the explosions touch
                Destroy(hit.collider.gameObject, explosionDuration/2);

                break; 
            }
        }

        yield return null;
    }  
}
