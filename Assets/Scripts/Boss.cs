using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Boss : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;
    public AudioClip oinkClip;
    public AudioClip squealClip;

    //Bomb
    public float bombCooldown = 0.2f;
    public GameObject bomb;
    public float bombExplosionRange = 1f;
    public AudioClip placeBombClip;
    public LayerMask wallLayerMask;
    private AudioSource audioSource;

    [SerializeField]
    private int _points = 1;//value of enemy death.
    private float cooldown;

    //Input Held down variables
    [SerializeField]
    private float _buttonPressedTime;
    [SerializeField]
    private bool playerInRadius = false;

    [SerializeField]
    private float _speedEnemyRaised = 5.0f;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _damageVFXPrefab;
    private Rigidbody rb;
    private enum State
    {
        IDLE,
        MOVING,
        ATTACK,
        COUNT, // always keep me last
    };
    private State state;
    private float timeTillNextStateUpdate = 0;
    private Vector3 direction;

    //Managers
    private PlayerController _player;
    private UIManager _uiManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
          if(_player == null)
          {
            Debug.LogError("Player is NULL");
          }
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
          if(_uiManager == null)
          {
            Debug.LogError("UI Manager is NULL");
          }
        state = State.IDLE;
        timeTillNextStateUpdate = Random.Range(0.5f, 2f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject, 0); // this kills the crab
        }
    }
    void OnTriggerEnter(Collider collider)
    {
      if(collider.gameObject.tag == "Player")
      {
        playerInRadius = true;
      }
    }
    void OnTriggerExit(Collider collider)
    {
      if(collider.gameObject.tag == "Player")
      {
        playerInRadius = false;
      }
    }

    private void EnemyScore(int points)
    {
      _points += points;
      _uiManager.UpdateEnemyScore(points);//tells HUD UI_Manager to update point system.
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(squealClip, transform.position);
        EnemyScore(_points);
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        timeTillNextStateUpdate -= Time.deltaTime;
        if (timeTillNextStateUpdate < 0)
        {
            AudioSource.PlayClipAtPoint(oinkClip, transform.position);
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
            case State.ATTACK:
                if(cooldown <= 0)
                {
                  __DropBomb();
                }
                break;
        }
//Rising attack
        if(Input.GetKey(KeyCode.LeftShift) && playerInRadius == true)
        {
          _buttonPressedTime += 10 * Time.deltaTime;
          speed = 0.0f;
          _animator.SetBool("isEnemyRaised", true);//enemy shakes in fear.
          transform.Translate(Vector3.up * _speedEnemyRaised * Time.deltaTime);
          if(_buttonPressedTime >= 15.0f)
          {
            //StartCoroutine(ActivateTeleDeathRoutine());
            ActivateTeleDeath();
          }
        }
        else
        {
          speed = 5.0f;
        }
    }
    private void ActivateTeleDeath()
    {
      Destroy(this.gameObject);
      OnDestroy();
      Instantiate(_damageVFXPrefab, transform.position, Quaternion.identity);
    }
    IEnumerator ActivateTeleDeathRoutine()
    {
        yield return new WaitForSeconds(1f);
        ActivateTeleDeath();
    }
    private void __DropBomb()
    {
        cooldown = bombCooldown;
        if (bomb == null) return;
        audioSource?.Play();
        Vector3 bombPosition = Vector3Int.RoundToInt(transform.position);
        bombPosition.y = transform.position.y;
        GameObject bombInstance = Instantiate(bomb, bombPosition, Quaternion.identity);
        bombInstance.GetComponent<BossBomb>().explosionRange = bombExplosionRange;
    }
}