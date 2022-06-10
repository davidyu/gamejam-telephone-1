using System.Collections;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
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
