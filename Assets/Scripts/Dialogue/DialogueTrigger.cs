using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
            indicator.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.getInstance().EnterDialogueMode(inkJSON);
            }

        }
        else
        {
            indicator.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player is in range");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player is not in range");
        }
    }
}
