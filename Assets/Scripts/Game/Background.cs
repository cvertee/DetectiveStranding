using System;
using System.Collections;
using System.IO;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Background : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private DialogueRunner dialogueRunner;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler("cb", (parameters, complete) =>
        {
            StartCoroutine(BackgroundChangerThread(parameters[0], complete));
        });
    }

    private IEnumerator BackgroundChangerThread(string bg, Action onComplete)
    {
        Debug.Log($"Changing background to {bg}");
        spriteRenderer.sprite = Resources.Load<Sprite>($"bg/{bg}");
        
        onComplete();

        yield return null;
    }
    
    
}