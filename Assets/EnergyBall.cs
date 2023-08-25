using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float lifttime = 2.0f;
    public float explosionDuration = 0.2f;
    public AudioClip smallExplosionClip;
    public GameObject explosion;

    private float start_time;

    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find( "Player" ).GetComponent<PlayerController>();
        if ( _player == null )
        {
            Debug.LogError( "Player is NULL" );
        }

        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Time.time - start_time >= lifttime )
        {
            Destroy( this.gameObject );
            Explode();
        }
    }
    private void EnemyScore( int points )
    {
        //_points += points;
        _player.AddEnemyScore( points );//tells HUD UI_Manager to update point system.
    }

    void OnTriggerEnter( Collider other )
    {
        if ( other.gameObject.tag == "Enemy" )
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if ( enemy )
            {
                enemy.ActivateTeleDeath();
            }

            Boss boss = other.gameObject.GetComponent<Boss>();
            if ( boss )
            {
                boss.ActivateTeleDeath();
            }

            Explode();
        }
    }

    void Explode()
    {
        AudioSource.PlayClipAtPoint( smallExplosionClip, transform.position );

        //destroys anything collided with.
        Instantiate( explosion, transform.position, Quaternion.identity );
        Destroy( gameObject, explosionDuration );
    }
}
