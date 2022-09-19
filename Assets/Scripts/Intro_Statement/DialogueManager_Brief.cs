using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager_Brief : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator _animatorBtn;
	public Animator _animatorText;
	public Image _btnImage;
	public Text _textButton;
	public Text _textBody;

	private Queue<string> sentences;

	// Use this for initialization
	void Start ()
	{
		_animatorBtn.GetComponent<Animator>();
		_animatorText.GetComponent<Animator>();
		_animatorBtn.SetBool("isShowing", false);
		_animatorText.SetBool("isShowing", false);
		_btnImage.GetComponent<Image>();
		_btnImage.enabled = false;
		_textButton.GetComponent<Text>();
		_textButton.enabled = false;
		_textBody.GetComponent<Text>();
		_textBody.enabled = false;
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		//_animatorBtn.SetBool("isShowing", true);
		//_animatorText.SetBool("isShowing", true);
		_textBody.enabled = true;
		_btnImage.enabled = true;
		_textButton.enabled = true;

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);

		}

		DisplayNextSentence();

	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{

			EndDialogue();
			return;
	}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{

			dialogueText.text += letter;
			//Need to figure out better per character  sound. Maybe slow down text?
			//FindObjectOfType<AudioManager>().Play("dialoguetype_fx");
			yield return null;
		}
	}

	void EndDialogue()
	{
		_animatorBtn.SetBool("isShowing", true);
		_animatorText.SetBool("isShowing", true);
		//_btnGO.SetActive(true);
	}

}
