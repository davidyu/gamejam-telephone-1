using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _numBombsText;
    [SerializeField]
    private Text _numEnergyballsText;
    [SerializeField]
    private Text _numEnemiesText;
    [SerializeField]
    private Text _numPowerUpsText;

    // Start is called before the first frame update
    void Start()
    {
        _numPowerUpsText.text = "Powerups Taken: " + 0;
        _numEnemiesText.text = "Enemies Killed: " + 0;
        _numBombsText.text = "Bombs Remaining: " + 0;
        _numEnergyballsText.text = "Energyballs: " + 0;
    }

    public void UpdatePowerScore( int numPowerupsTaken )
    {
        if ( _numPowerUpsText )
        {
            _numPowerUpsText.text = "Powerups Taken: " + numPowerupsTaken;
        }
    }
    public void UpdateEnemyScore( int numEnemiesKilled )
    {
        if ( _numEnemiesText )
        {
            _numEnemiesText.text = "Enemies: " + numEnemiesKilled;
        }
    }
    public void UpdateNumBombs( int numBombs )
    {
        if ( _numBombsText )
        {
            _numBombsText.text = "Bombs Remaining: " + numBombs;
        }
    }
    public void UpdateNumEnergyballs( int numEnergyballs )
    {
        if ( _numEnergyballsText )
        {
            _numEnergyballsText.text = "Energyballs: " + numEnergyballs;
        }
    }
}
