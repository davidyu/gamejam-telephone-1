using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DestroyOtherGameObjects : MonoBehaviour
{
    public float lethalDuration = 0.5f;
    public LayerMask destroyMask;

    void Update()
    {
        lethalDuration -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (lethalDuration <= 0)
        {
            return;
        }

        if ((destroyMask & (1 << collider.gameObject.layer)) != 0)
        {
            Destroy(collider.gameObject, 0f);
        }
    }
}
