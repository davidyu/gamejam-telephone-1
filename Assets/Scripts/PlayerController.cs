using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float bombCooldown = 0.2f;
    public GameObject bomb;
    public float bombExplosionRange = 1f;
    public LayerMask wallLayerMask;

    private Animator animatorMovement;
    private Rigidbody rb;
    private float cooldown;

    void Start()
    {
        if (bomb == null)
        {
            Debug.Log("bomb is null! Player cannot lay bombs");
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (Input.GetButton("Jump") && cooldown <= 0)
        {
            __DropBomb();
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = Input.GetAxisRaw("Vertical") * Vector3.forward + Input.GetAxisRaw("Horizontal") * Vector3.right;

        RaycastHit hit; 
        Physics.Raycast(transform.position, moveDirection, out hit, 1.0f, wallLayerMask);

        rb.velocity = (hit.collider ? moveDirection : moveDirection.normalized) * speed;

        __UpdateAnimator(moveDirection);
    }

    void OnDestroy()
    {
        GameObject UI = GameObject.FindWithTag("UI");
        if (UI)
        {
            UI.SendMessage("GameOver");
        }
    }

    private void __UpdateAnimator(Vector3 moveDirection)
    {
        if (animatorMovement != null)
        {
            animatorMovement.SetFloat("MoveX", Vector3.Dot(moveDirection, transform.right));
            animatorMovement.SetFloat("MoveZ", Vector3.Dot(moveDirection, transform.forward));
        }
    }

    private void __DropBomb()
    {
        cooldown = bombCooldown;
        if (bomb != null)
        {
            Vector3 bombPosition = Vector3Int.RoundToInt(transform.position);
            bombPosition.y = transform.position.y;
            GameObject bombInstance = Instantiate(bomb, bombPosition, Quaternion.identity);
            bombInstance.GetComponent<Bomb>().explosionRange = bombExplosionRange;
        }
    }
}
