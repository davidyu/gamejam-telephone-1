using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;
    public float bombCooldown = 0.2f;
    public GameObject bomb;
    public float bombExplosionRange = 1f;
    public AudioClip placeBombClip;
    public LayerMask wallLayerMask;
    public int _numberOfPowerups;//powerups collected

    [SerializeField]
    private int _powerupPoints;
    [SerializeField]
    private int _enemyPoints;

    //Input Held down variables
    private float _minimumHeldDuration = 2.5f;
    private float _buttonPressedTime = 0f;
    private bool _buttonHeld = false;
    [SerializeField]
    private GameObject _particleSystem;


    private AudioSource audioSource;
    private Animator animatorMovement;
    private Rigidbody rb;
    private float cooldown;

    //managers
    private UIManager _uiManager;
    private GameManager _gameManager;
    private AudioManager _audioManager;

    void Start()
    {
        if (bomb == null)
        {
            Debug.Log("bomb is null! Player cannot lay bombs");
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("player has no audio source");
        }
        else
        {
            audioSource.clip = placeBombClip;
        }
        animatorMovement = GameObject.Find("Player").GetComponent<Animator>();
        if (animatorMovement == null)
        {
            Debug.LogError("Animator is NULL");
        }
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("UI_Manager is NULL");
        }
      _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
          Debug.LogError("Game_Manager is NULL");
        }
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        if(_audioManager == null)
        {
          Debug.LogError("Audio Manager is NULL");
        }
        _particleSystem.SetActive(false);
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
          DroppedBombTrueAnimation();
          __DropBomb();
        }
        else
        {
          DroppedBombFalseAnimation();
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
          __RaisingAttack();
          _particleSystem.SetActive(true);
        }
        else
        {
          speed = 5.0f;
          animatorMovement.SetBool("isRaising", false);
          _particleSystem.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
          _audioManager.ActivateEnergyBall();
        }
    }

    void FixedUpdate()
    {
      Movement();
    }

    void Movement()
    {
      //Input
      Vector3 moveDirection = Input.GetAxisRaw("Vertical") * Vector3.forward + Input.GetAxisRaw("Horizontal") * Vector3.right;

      //Moving
      RaycastHit hit;
      Physics.Raycast(transform.position, moveDirection, out hit, 1.0f, wallLayerMask);
      rb.velocity = (hit.collider ? moveDirection : moveDirection.normalized) * speed;

      //Looking
      if(moveDirection != Vector3.zero)
      {
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
      }

      //move animation
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
        animatorMovement.SetFloat("MoveX", Vector3.Dot(moveDirection, transform.forward));
        animatorMovement.SetFloat("MoveZ", Vector3.Dot(moveDirection, transform.right));
    }

    private void __DropBomb()
    {
        cooldown = bombCooldown;
        if (bomb == null) return;
        audioSource?.Play();
        Vector3 bombPosition = Vector3Int.RoundToInt(transform.position);
        bombPosition.y = transform.position.y;
        GameObject bombInstance = Instantiate(bomb, bombPosition, Quaternion.identity);
        bombInstance.GetComponent<Bomb>().explosionRange = bombExplosionRange;
    }

    public void AddPowerScore(int points)
    {
      _powerupPoints += points;
      _uiManager.UpdatePowerScore(_powerupPoints);
    }

    public void AddEnemyScore(int points)
    {
      _enemyPoints += points;
      _uiManager.UpdateEnemyScore(_enemyPoints);
      if(points >= 8)
      {
        _gameManager.EnableExitTriggerBox();//tells gamemanger that all enemies are dead and allow gate to open.
        Debug.Log("gate open");
      }
    }

    private void DroppedBombTrueAnimation()
    {
      animatorMovement.SetBool("isYes", true);
      //animatorMovement.SetLayerWeight(animatorMovement.GetLayerIndex("Yes"), 1f);
    }
    private void DroppedBombFalseAnimation()
    {
      animatorMovement.SetBool("isYes", false);
      //animatorMovement.SetLayerWeight(animatorMovement.GetLayerIndex("Yes"), 0f);
    }

    //Gets called from Enemy Script and on the update
    public void __RaisingAttack()
    {
      speed = 0f;
      animatorMovement.SetBool("isRaising", true);
    }
}
