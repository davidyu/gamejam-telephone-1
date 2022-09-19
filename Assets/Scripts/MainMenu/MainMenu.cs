using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  [SerializeField]
  private Animator _animator;
  [SerializeField]
  private Animator _animatorTitle;
  [SerializeField]
  private Animator _animatorBtn1;
  [SerializeField]
  private Animator _animatorBtn2;
  [SerializeField]
  private Animator _animatorText1;
  [SerializeField]
  private Animator _animatorText2;

  //Reference to managers
  private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
      _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
      if(_gameManager == null)
      {
        Debug.LogError("Game Manager is NULL");
      }
      _animator.GetComponent<Animator>();
      _animatorTitle.GetComponent<Animator>();
      _animatorBtn1.GetComponent<Animator>();
      _animatorBtn2.GetComponent<Animator>();
      _animatorText1.GetComponent<Animator>();
      _animatorText2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateCameraZoom()
    {
      _animator.SetBool("isZooming", true);
      _animatorTitle.SetBool("isHidden", true);
      _animatorBtn1.SetBool("isHidden", true);
      _animatorBtn2.SetBool("isHidden", true);
      _animatorText1.SetBool("isHidden", true);
      _animatorText2.SetBool("isHidden", true);

    }
    public void DeactivateCameraZoom()
    {
      _animator.SetBool("isZooming", false);
      _animatorTitle.SetBool("isHidden", false);
      _animatorBtn1.SetBool("isHidden", false);
      _animatorBtn2.SetBool("isHidden", false);
      _animatorText1.SetBool("isHidden", false);
      _animatorText2.SetBool("isHidden", false);
    }

    public void BtnScaleUp()
    {
      _animatorBtn1.SetBool("isScaled", true);
    }
    public void BtnScaleDown()
    {
      _animatorBtn1.SetBool("isScaled", false);
    }

    public void ButtonLoadMaze()
    {
      _gameManager.LoadSceneMaze();
    }

    public void ButtonQuitGame()
    {
      _gameManager.QuitGame();
    }
}
