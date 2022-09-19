using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersManager : MonoBehaviour
{
    //managers
    private PlayerController _player;
    private Enemy _enemy;

    void OnStart()
    {
      _player = GameObject.Find("Player").GetComponent<PlayerController>();
        if(_player == null)
        {
          Debug.LogError("Player is NULL");
        }
      _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        if(_enemy == null)
        {
          Debug.LogError("Enemy is NULL");
        }
    }
    //This gets called from the PlayerController script.
    //If the enemy is within the players radius and presses LeftShift,
    //Enemy dies.
    public void TeleDeathActivated()
    {
      //IENUMERATOR
      //_enemy.ActivateTeleDeath();
    }
    public void PlayerWithinRadius()
    {
      //_player.EnemyWithinRadius();
    }
}
