using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //manager
    private UIManager _uiManager;
    private VictoryCondition _victoryCondition;

    void Start()
    {
        //Scene Detectors - loads correct scripts for scenes.
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if ( sceneName == "Maze" || sceneName == "Boss" )//If scene is loaded, looks for neccessary scripts.
        {
            _uiManager = GameObject.Find( "UI_Manager" ).GetComponent<UIManager>();
            if ( _uiManager == null )
            {
                Debug.LogError( "UI_Manager is NULL" );
            }
        }
    }

    public void LoadSceneMainMenu()//Main Menu
    {
        SceneManager.LoadScene( "MainMenu" );
    }
    public void LoadSceneMaze()//Maze
    {
        SceneManager.LoadScene( "Maze" );
    }
    public void LoadSceneW1nn3rScreen()//W1nn3r Screen
    {
        SceneManager.LoadScene( "W1nn3r Screen" );
    }
    public void QuitGame()//Quits game
    {
        Application.Quit();
    }
    public void EnableExitTriggerBox()//TODO: if time allows, was going to add a trigger for the exit. If all enemies are dead, open gate.
    {
        _victoryCondition.ActivateExitTriggerBox();
    }
}
