using System.Collections;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
  [SerializeField]
  private Collider _triggerBox;

    void Start()
    {
      _triggerBox = this.GetComponent<Collider>();
      _triggerBox.isTrigger = false;
    }

    public void ActivateExitTriggerBox()
    {
      _triggerBox.isTrigger = true;
    }
    public void DeactivateExitTriggerBox()
    {
      _triggerBox.isTrigger = false;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameObject UI = GameObject.FindWithTag("UI");
            if (UI)
            {
                UI.SendMessage("GameWon");
               // yield return new WaitForSeconds(5);
            }
            FindObjectOfType<SceneLoader>().LoadNextScene();
        }
    }
}
