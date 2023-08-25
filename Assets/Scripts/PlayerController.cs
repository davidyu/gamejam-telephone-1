using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;
    public float bombCooldown = 0.2f;
    public GameObject bomb;
    public float bombExplosionRange = 1f;
    public int numBombs = 2;
    public int numEnergyBalls = 3;
    public AudioClip placeBombClip;
    public LayerMask wallLayerMask;
    public int _numberOfPowerups;//powerups collected
    public GameObject energyballProjectile;

    [SerializeField]
    private int _powerupsTaken;
    [SerializeField]
    private int _enemiesKilled;

    //Input Held down variables
    //private float _minimumHeldDuration = 2.5f;
    //private float _buttonPressedTime = 0f;
    //private bool _buttonHeld = false;
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
        if ( bomb == null )
        {
            Debug.Log( "bomb is null! Player cannot lay bombs" );
        }
        audioSource = GetComponent<AudioSource>();
        if ( audioSource == null )
        {
            Debug.Log( "player has no audio source" );
        }
        else
        {
            audioSource.clip = placeBombClip;
        }
        animatorMovement = GameObject.Find( "Player" ).GetComponent<Animator>();
        if ( animatorMovement == null )
        {
            Debug.LogError( "Animator is NULL" );
        }
        _uiManager = GameObject.Find( "UI_Manager" ).GetComponent<UIManager>();
        if ( _uiManager == null )
        {
            Debug.LogError( "UI_Manager is NULL" );
        }
        // Update Bombs and energyballs
        _uiManager.UpdateNumBombs( numBombs );
        _uiManager.UpdateNumEnergyballs( numEnergyBalls );

        _gameManager = GameObject.Find( "Game_Manager" ).GetComponent<GameManager>();
        if ( _gameManager == null )
        {
            Debug.LogError( "Game_Manager is NULL" );
        }
        _audioManager = GameObject.Find( "Audio_Manager" ).GetComponent<AudioManager>();
        if ( _audioManager == null )
        {
            Debug.LogError( "Audio Manager is NULL" );
        }
        _particleSystem.SetActive( false );
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if ( Input.GetButton( "Jump" ) && cooldown <= 0 && numBombs > 0 )
        {
            DroppedBombTrueAnimation();
            __DropBomb();
        }
        else
        {
            DroppedBombFalseAnimation();
        }

        InstantiateEnergyBall();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void InstantiateEnergyBall()
    {
        if( numEnergyBalls > 0 )
        {
            if ( Input.GetKeyDown( KeyCode.LeftShift ) )
            {
                // Start the energy ball audio
                _audioManager.ActivateEnergyBall();
            }
            else if ( Input.GetKeyUp( KeyCode.LeftShift ) )
            {
                speed = 5.0f;
                animatorMovement.SetBool( "isRaising", false );
                _particleSystem.SetActive( false );

                GameObject leftChild = Helper.FindInChildren( gameObject, "L_Hand" );

                GameObject energyball = Instantiate( energyballProjectile, leftChild.transform.position, leftChild.transform.rotation );
                //energyball.transform.Rotate( Vector3.up );
                Rigidbody rb = energyball.GetComponent<Rigidbody>();
                rb.velocity = -energyball.transform.forward * 5;

                AddEnergyballs( -1 );
            }
            else if ( Input.GetKey( KeyCode.LeftShift ) )
            {
                // Get it expand
                __RaisingAttack();
                _particleSystem.SetActive( true );
            }
        }   
    }

    void Movement()
    {
        //Input
        Vector3 moveDirection = Input.GetAxisRaw( "Vertical" ) * Vector3.forward + Input.GetAxisRaw( "Horizontal" ) * Vector3.right;

        //Moving
        RaycastHit hit;
        Physics.Raycast( transform.position, moveDirection, out hit, 1.0f, wallLayerMask );
        rb.velocity = ( hit.collider ? moveDirection : moveDirection.normalized ) * speed;

        //Looking
        if ( moveDirection != Vector3.zero )
        {
            Quaternion toRotation = Quaternion.LookRotation( moveDirection, Vector3.up );
            transform.rotation = Quaternion.RotateTowards( transform.rotation, toRotation, rotationSpeed * Time.deltaTime );
        }

        //move animation
        __UpdateAnimator( moveDirection );
    }

    void OnDestroy()
    {
        GameObject UI = GameObject.FindWithTag( "UI" );
        if ( UI )
        {
            UI.SendMessage( "GameOver" );
        }
    }

    private void __UpdateAnimator( Vector3 moveDirection )
    {
        animatorMovement.SetFloat( "MoveX", Vector3.Dot( moveDirection, transform.forward ) );
        animatorMovement.SetFloat( "MoveZ", Vector3.Dot( moveDirection, transform.right ) );
    }

    private void __DropBomb()
    {
        cooldown = bombCooldown;
        if ( bomb == null ) return;
        audioSource?.Play();
        Vector3 bombPosition = Vector3Int.RoundToInt( transform.position );
        bombPosition.y = transform.position.y;
        GameObject bombInstance = Instantiate( bomb, bombPosition, Quaternion.identity );
        bombInstance.GetComponent<Bomb>().explosionRange = bombExplosionRange;
        AddBombs( -1 );
    }

    public void AddPowerScore( int points )
    {
        _powerupsTaken += points;
        _uiManager.UpdatePowerScore( _powerupsTaken );
    }

    public void AddEnemyScore( int points )
    {
        _enemiesKilled += points;
        _uiManager.UpdateEnemyScore( _enemiesKilled );
        if ( points >= 1 )
        {
            _gameManager.EnableExitTriggerBox();
            Debug.Log( "gate open" );
        }
    }

    public void AddBombs( int numAddedBombs )
    {
        numBombs += numAddedBombs;
        _uiManager.UpdateNumBombs( numBombs );
    }

    public void AddEnergyballs( int numAddedBalls )
    {
        numEnergyBalls += numAddedBalls;
        _uiManager.UpdateNumEnergyballs( numEnergyBalls );
    }

    private void DroppedBombTrueAnimation()
    {
        animatorMovement.SetBool( "isYes", true );
        //animatorMovement.SetLayerWeight(animatorMovement.GetLayerIndex("Yes"), 1f);
    }
    private void DroppedBombFalseAnimation()
    {
        animatorMovement.SetBool( "isYes", false );
        //animatorMovement.SetLayerWeight(animatorMovement.GetLayerIndex("Yes"), 0f);
    }

    //Gets called from Enemy Script and on the update
    public void __RaisingAttack()
    {
        speed = 0f;
        animatorMovement.SetBool( "isRaising", true );
    }
}

public static class Helper
{
    public static GameObject FindInChildren( this GameObject obj, string name )
    {
        return ( from x in obj.GetComponentsInChildren<Transform>()
                 where x.gameObject.name == name
                 select x.gameObject ).First();
    }
}
