using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggers : MonoBehaviour
{
    public Dialogue dialogue;
   
    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueScript>().StartDialogue(dialogue);
    }
}
