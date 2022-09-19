using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialogue : MonoBehaviour
{
  [SerializeField]
  private GameObject _uiDialogue;
  
    public void ActivateNPCDialogue()
    {
      _uiDialogue.SetActive(true);
    }
    public void DeactivateNPCDialogue()
    {
      _uiDialogue.SetActive(false);
    }
}
