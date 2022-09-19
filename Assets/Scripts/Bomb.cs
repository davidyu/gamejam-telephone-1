using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Explosion")]
    public float explosionCountdown = 2.0f;
    public float explosionDuration = 0.2f;
    public float explosionRange = 1;
    public AudioClip smallExplosionClip;
    public AudioClip bigExplosionClip;
    public AudioClip hugeExplosionClip;
    public LayerMask explosionMask;
    public GameObject explosion;

    [Header("Enemy")]
    public LayerMask _unitsLayer;
    private int _points = 1;//value of enemy death.

    //mangers
    private UIManager _uiManager;
    private PlayerController _player;

    void Start()
    {
        Invoke(nameof(Explode), explosionCountdown);
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
          if(_player == null)
          {
            Debug.LogError("Player is NULL");
          }
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
          if(_uiManager == null)
          {
            Debug.LogError("UI_Manager is NULL");
          }
    }

    void Explode()
    {
        if (explosionRange >= 3)
        {
            AudioSource.PlayClipAtPoint(hugeExplosionClip, transform.position);
        }
        else if (explosionRange >= 2)
        {
            AudioSource.PlayClipAtPoint(bigExplosionClip, transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(smallExplosionClip, transform.position);
        }

        //destroys anything collided with.
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
            //Physics.Raycast(transform.position + new Vector3(0,.5f,0), direction, out hit1, i, _unitsLayer);
            if (explosion != null)
            {
                Instantiate(explosion, transform.position + (i * direction), Quaternion.identity);
            }
            if (hit.collider)
            {
                //tried to have bombs kill enemies and give player score - couldnt get it to work right.
                //EnemyScore(_points);//when bomb collides with enemy, add point to player.
                //Debug.Log("Added point");

                // this destroys whatever the explosions touch
                Destroy(hit.collider.gameObject, explosionDuration/2);
                break;
            }
        }
        yield return null;
    }
          /*if (Physics.Raycast(transform.position, direction, _unitsLayer))//raycast only collides with layermask, Units layer represents enemy.
          {
            EnemyScore(_points);//when bomb collides with enemy, add point to player.
            Debug.Log("Added point");
            break;
          }*/

    //keeps track of enemies killed by bombs.
    private void EnemyScore(int points)
    {
      _player.AddEnemyScore(_points);//tells player to update point system.
    }
}
