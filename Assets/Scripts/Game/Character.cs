using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private Image image;
    private RectTransform rectTransform;
    private string emotion;

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        GameEvents.Instance.onCharacterSetEmotionRequested.AddListener( 
            OnCharacterSetEmotionRequested
        );
        
        GameEvents.Instance.onCharacterRemoveRequested.AddListener(
            OnCharacterRemoveRequested
        );
        
        GameEvents.Instance.onCharacterMoveRequested.AddListener( 
            OnCharacterMoveRequested
        );
    }

    public void SetName(string newName)
    {
        this.name = newName;
    }

    public void InitializeImage()
    {
        image.sprite = Resources.Load<Sprite>($"characters/{name}/{name}_{emotion}");
    }
    
    public void SetEmotion(string newEmotion)
    {
        Debug.Log($"Set {name}`s emotion to {newEmotion}");
        this.emotion = newEmotion;
        InitializeImage();
    }

    public void SetPosition(string position)
    {
        Debug.Log($"Set {name}`s position to {position}");
        const int offsetX = 400;
        const int halfOffsetX = offsetX / 2;
        
        switch (position.ToLower())
        {
            case "left":
                rectTransform.anchoredPosition = new Vector2(-halfOffsetX, 0);
                break;
            
            case "center":
                rectTransform.anchoredPosition = new Vector2(0, 0);
                break;
            
            case "right":
                rectTransform.anchoredPosition = new Vector2(halfOffsetX, 0);
                break;
            
            default:
                rectTransform.offsetMax = new Vector2(-offsetX, 0); // put in middle if some shit breaks itself
                break;
        }
    }

    public void Kill() // rofl...
    {
        Debug.Log($"Removing {name}");
        Destroy(gameObject);
    }
    
    private void OnCharacterSetEmotionRequested(string[] args)
    {
        var argName = args[0] as string;
        var argEmotion = args[1] as string;
                
        if (name == argName)
            SetEmotion(argEmotion);
    }

    private void OnCharacterRemoveRequested(string args)
    {
        var argName = args;
        if (name == argName)
            Kill();
    }

    private void OnCharacterMoveRequested(string[] args)
    {
        var argName = args[0];
        var argPos = args[1];
                
        if (name == argName)
            SetPosition(argPos);
    }
}