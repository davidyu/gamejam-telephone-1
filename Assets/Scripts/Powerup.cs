using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Powerup : MonoBehaviour
{
    [SerializeField]//value of powerups.
    private int _points = 1;
    public AudioClip powerupClip;

    //manager
    private UIManager _uiManager;
    private PlayerController _player;

    void Start()
    {
      _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if(_uiManager == null)
        {
          Debug.LogError("UI_Manager is NULL");
        }
      _player = GameObject.Find("Player").GetComponent<PlayerController>();
        if(_player == null)
        {
          Debug.LogError("Player is NULL");
        }
    }

    private void PowerScore(int points)
    {
      //_points += points;
      //_uiManager.UpdatePowerScore(points);
      _player.AddPowerScore(_points);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController controller = collider.gameObject.GetComponent<PlayerController>();
            if( controller )
            {
                // do something to power them up!
                int index = Random.Range( 0, 2 );
                switch ( index )
                {
                    case 0:
                        {
                            controller.bombExplosionRange++;
                            
                            break;
                        }
                    case 1:
                        {
                            controller.AddBombs( 1 );
                            break;
                        }
                    case 2:
                        {
                            controller.AddEnergyballs( 1 );
                            break;
                        }
                }
            }
    
            PowerScore(_points);
            AudioSource.PlayClipAtPoint(powerupClip, transform.position);
            Destroy(gameObject, 0f);
            Debug.Log("Power up collected");
        }
    }
}
