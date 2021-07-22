using Assets.Scripts.Core;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickableItem : MonoBehaviour
{
    // Yarn node that will be started ON CLICK
    public string yarnNodeToStart;
    // Destroy item on click if needs to do so
    public bool shouldDestroyOnUse = true;

    // Locked means it cannot be clicked.
    private bool locked = false;

    private void Awake() 
    {
        // Clickable items mostly used when some dialogue is finished and 
        // game lets player to click on stuff to see dialogues
        // So when dialogue ends - all of the clickable items are unlocked
        // 
        // And reversed - if ANY dialogue starts/continues than all clickable items
        // are locked to prevent to click on items while "inspecting" some other.
        GameEvents.Instance.onYarnLineStart.AddListener((arg) => Lock());
        GameEvents.Instance.onYarnDialogueEnd.AddListener(Unlock);
    }

    private void OnMouseDown()
    {
        if (locked)
            return;

        StartDialogue();
        
        if (shouldDestroyOnUse)
            Destroy(this.gameObject);
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }

    public void StartDialogue()
    {
        // Some of the items require to do something with the game
        // Like invoke events
        // TODO: events probably should be moved right into this class
        // TODO: only if it doesn't break some SOLID stuff...
        var clickableEvents = GetComponent<ClickableEvents>();
        if (clickableEvents != null)
        {
            clickableEvents.Invoke();
        }
        else
        {
            Debug.Log($"ClickableEvents null at {name}");
        }

        FindObjectOfType<GameManager>().StartYarnNode(yarnNodeToStart);
    }
}
