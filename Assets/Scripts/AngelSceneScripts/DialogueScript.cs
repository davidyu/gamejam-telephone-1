using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator ImageAnimator;
    public Animator animator;
    public Animator buttonClickAnimator;
    public Animator yesButtonAnimator;
    public Animator noButtonAnimator;
    public Animator whoButtonAnimator;

    public GameObject _buttonContinue;

    private Queue<string> sentences;

    int Counter = 0;

    void Start ()
    {
        sentences = new Queue<string>();
        _buttonContinue.GetComponent<Transform>();
        _buttonContinue.SetActive(false);
    }

    public void StartDialogue (Dialogue dialogue)
    {
        if(Counter == 1)
        {
            yesButtonAnimator.SetBool("ChoicesOpen", false);
            noButtonAnimator.SetBool("ChoicesOpen", false);
            whoButtonAnimator.SetBool("ChoicesOpen", false);
        }
        buttonClickAnimator.SetBool("ButtonClicked", true);
        animator.SetBool("IsOpen", true);

        Debug.Log("Starting convo with: " + dialogue.name);
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
        if(sentences.Count == 5)
        {
            ImageAnimator.SetBool("ImageTransition", true);
            FindObjectOfType<AudioManager>().Play("ILoveYouJesus");
        }


        if(sentences.Count == 0)
        {
            if (Counter == 1)
            {
                ImageAnimator.SetBool("LastTransition", true);
            }
            Debug.Log("Count == 0");
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log("More sentences:" + sentence);
        StopAllCoroutines();
        StartCoroutine(TypeOutSentence(sentence));
        FindObjectOfType<AudioManager>().Play("Undertale Sound Effect - Monster Voice");
    }

    IEnumerator TypeOutSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {

            dialogueText.text += letter;
            yield return new WaitForSeconds(0.050F);
        }
    }

    void EndDialogue()
    {

        Counter += 1;
        Debug.Log("End");
        animator.SetBool("IsOpen", false);
        if (Counter == 1)
        {
            yesButtonAnimator.SetBool("ChoicesOpen", true);
            noButtonAnimator.SetBool("ChoicesOpen", true);
            whoButtonAnimator.SetBool("ChoicesOpen", true);
            Debug.Log(Counter);
        } else
        {
            Debug.Log(Counter);
            yesButtonAnimator.SetBool("ChoicesOpen", false);
            noButtonAnimator.SetBool("ChoicesOpen", false);
            whoButtonAnimator.SetBool("ChoicesOpen", false);
        }

        Debug.Log(Counter);
    }

    public void ShowContinueButton()
    {
      _buttonContinue.SetActive(true);
    }
    public void LoadNextScene()
    {
      FindObjectOfType<SceneLoader>().LoadNextScene();
    }
}
