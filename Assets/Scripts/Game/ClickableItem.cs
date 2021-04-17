using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickableItem : MonoBehaviour
{
    public string yarnNodeToStart;
    public bool shouldDestroyOnUse = true;

    private bool locked = false;

    private void OnMouseDown()
    {
        if (locked)
            return;

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
}
