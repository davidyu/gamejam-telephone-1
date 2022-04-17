using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody rb;
    private enum State
    {
        IDLE,
        MOVING,

        COUNT, // always keep me last
    };
    private State state;
    private float timeTillNextStateUpdate = 0;
    private Vector3 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Start()
    {
        state = State.IDLE;
        timeTillNextStateUpdate = Random.Range(0.5f, 2f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // kill 'em!
            Destroy(collision.gameObject, 0f);
        }
    }

    void Update()
    {
        timeTillNextStateUpdate -= Time.deltaTime;
        if (timeTillNextStateUpdate < 0)
        {
            state = (State)Random.Range(0, (int)State.COUNT);
            Vector3[] directions = {Vector3.forward, Vector3.right, Vector3.back, Vector3.left};
            direction = directions[Random.Range(0, directions.Length)];
            timeTillNextStateUpdate = Random.Range(0.5f, 2f);
        }

        switch (state)
        {
            case State.IDLE:
                break;
            case State.MOVING:
                rb.velocity = direction * speed;
                break;
        }
    }
}
