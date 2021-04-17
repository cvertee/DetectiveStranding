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
    
    private void Start()
    {
        //GameEvents.Instance.onBackgroundChangeRequest.AddListener(ChangeTo);
        
        
    }

    private void ChangeTo(string bg)
    {
        //StartCoroutine(BackgroundChangerThread(bg));
        //image.sprite = Resources.Load<Sprite>($"bg/{bg}");
    }

    private IEnumerator BackgroundChangerThread(string bg, Action onComplete)
    {
        Debug.Log($"Changing background to {bg}");
        spriteRenderer.sprite = Resources.Load<Sprite>($"bg/{bg}");
        
        //yield return new WaitForSeconds(0.001f);
        onComplete();

        yield return null;
    }
    
    
}