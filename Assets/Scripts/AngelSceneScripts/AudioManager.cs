using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Start()
    {
      //Scene Detectors - loads correct scripts for scenes.
      Scene currentScene = SceneManager.GetActiveScene();
      string sceneName = currentScene.name;
      if(sceneName == "MainMenu")
      {
        Play("MainMenuTheme");
      }
      if(sceneName == "Maze")
      {
        Play("MazeTheme");
      }
      if(sceneName == "Boss")
      {
        Play("BossTheme");
      }
      if(sceneName == "W1nn3r Screen")
      {
        Play("Theme2");
      }

    }
    public void OnButtonClick()
    {
      Play("UIButtonClick");
    }
    public void OnButtonHover()
    {
      Play("UIButtonHover");
    }

    public void ActivateEnergyBall()
    {
      Play("PlayerEnergyBall");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
