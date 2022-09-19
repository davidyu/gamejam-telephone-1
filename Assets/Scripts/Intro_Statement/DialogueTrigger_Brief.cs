using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_Brief : MonoBehaviour
{
	[SerializeField]
  private Dialogue _dialogue;

  public void TriggerDialogue ()
  {
		StartCoroutine(ActivateIntroDialogue());
  }

	IEnumerator ActivateIntroDialogue()
	{
		yield return new WaitForSeconds(3f);
		FindObjectOfType<DialogueManager_Brief>().StartDialogue(_dialogue);
	}
}
