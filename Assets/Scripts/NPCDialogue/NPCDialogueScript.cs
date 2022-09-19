using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueScript : MonoBehaviour
{
    [SerializeField]
    private Text _nameText;
    [SerializeField]
	   private Text _bodyText;
    [SerializeField]
    private Animator _animator;
    private Queue<string> _sentences;

    void Start ()
    {
      _animator.gameObject.GetComponent<Animator>();
      _animator.SetBool("IsOpen", false);
      _sentences = new Queue<string>();
    }

    void Update()
    {
      if(Input.GetKey(KeyCode.Escape))
      {
        EndDialogue();
      }
    }

    public void StartDialogue (Dialogue dialogue)
	{
		_animator.SetBool("IsOpen", true);
		_nameText.text = dialogue.name;
		_sentences.Clear();
		foreach (string sentence in dialogue.sentences)
		{
			_sentences.Enqueue(sentence);
		}
		//StartCoroutine(ActivateTimer());
	}
  IEnumerator ActivateTimer()
	{
		yield return new WaitForSeconds(5f);
		DisplayNextSentence();
	}
  public void DisplayNextSentence ()
  {
    if (_sentences.Count == 0)
    {
      EndDialogue();
      return;
    }

    string sentence = _sentences.Dequeue();
    StopAllCoroutines();
    StartCoroutine(TypeSentence(sentence));
  }

  IEnumerator TypeSentence (string sentence)
  {
      _bodyText.text = "";
      foreach (char letter in sentence.ToCharArray())
    {
      _bodyText.text += letter;
      yield return null;
    }
  }

  public void EndDialogue()
	{
		_animator.SetBool("IsOpen", false);
	}
}
