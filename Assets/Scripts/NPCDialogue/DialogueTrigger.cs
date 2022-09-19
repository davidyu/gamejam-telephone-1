using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	[SerializeField]
  private Dialogue _dialogue;
	[SerializeField]
  private Animator _dialogueAnim;

	//managers
	private NPCDialogueScript _npcDialogueScript;
	private Dialogue _dialogueScript;
	[SerializeField]
	private UIDialogue _uiDialogue;

  void Start()
  {
		_npcDialogueScript = GameObject.Find("NPC_DialogueManager").GetComponent<NPCDialogueScript>();
		if(_npcDialogueScript == null)
		{
			Debug.LogError("NPC DialogueScript is NULL");
		}
		_dialogueAnim = GameObject.Find("UI_Manager").GetComponentInChildren<Animator>();
		if(_dialogueAnim == null)
		{
			Debug.LogError("Dialogue Animator is NULL");
		}
		_uiDialogue = GameObject.Find("UI_Manager").GetComponentInChildren<UIDialogue>();
			if(_uiDialogue == null)
			{
				Debug.LogError("UI_Dialogue is NULL");
			}
		_uiDialogue.DeactivateNPCDialogue();

  }
	
//Receives message on trigger collider box, to UI Dialogue to open dialogue, and sends message to Dilogue manager for text.
  public void TriggerDialogue()
  {
		_uiDialogue.ActivateNPCDialogue();
		_npcDialogueScript.StartDialogue(_dialogue);
		_npcDialogueScript.DisplayNextSentence();
  }


	public void ActivateDialogue()
	{
		_npcDialogueScript.StartDialogue(_dialogue);
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			TriggerDialogue();
		}
	}

}
