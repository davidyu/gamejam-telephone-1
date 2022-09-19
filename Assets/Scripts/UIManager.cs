using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _powerText;
    [SerializeField]
    private Text _enemyText;
    [SerializeField]
    private int _playerPowerScore;
    [SerializeField]
    private int _playerEnemyScore;

    // Start is called before the first frame update
    void Start()
    {
      _powerText.text = "Powerups: " + 0;
      _enemyText.text = "Enemies: " + 0;
    }

    public void UpdatePowerScore(int playerPowerScore)
    {
      _powerText.text = "Powerups: " + playerPowerScore;
      _playerPowerScore = playerPowerScore;
    }
    public void UpdateEnemyScore(int playerEnemyScore)
    {
      _playerEnemyScore = playerEnemyScore;
      _enemyText.text = "Enemies: " + playerEnemyScore;

    }
}
