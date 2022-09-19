using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyBall : MonoBehaviour
{

  private Animator _animatorEnergyBall;

    // Start is called before the first frame update
    void Start()
    {
      _animatorEnergyBall = this.GetComponent<Animator>();
      if(_animatorEnergyBall == null)
      {
        Debug.LogError("Animator is NULL");
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey(KeyCode.LeftShift))
      {
        _animatorEnergyBall.SetBool("isEnergyBallShowing", true);
      }
      else
      {
        _animatorEnergyBall.SetBool("isEnergyBallShowing", false);
      }
    }
}
