using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Vector3 leftPosition;
    public Vector3 middlePosition;
    public Vector3 rightPosition;
    public float dashSpeed = 0.25f;
    
    private SpriteRenderer image;
    private RectTransform rectTransform;
    private string emotion;

    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
        rectTransform = GetComponent<RectTransform>();
        
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

    private void Start()
    {
        
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
        
        switch (position.ToLower())
        {
            case "left":
                LeanTween.moveX(gameObject, leftPosition.x, dashSpeed);
                //transform.position = leftPosition;
                break;
            
            case "center":
                LeanTween.moveX(gameObject, middlePosition.x, dashSpeed);
                //transform.position = middlePosition;
                break;
            
            case "right":
                LeanTween.moveX(gameObject, rightPosition.x, dashSpeed);
                //transform.position = rightPosition;
                break;
            
            default:
                transform.position = middlePosition; // put in middle if some shit breaks itself
                break;
        }
    }

    /// <summary>
    /// Sets position instantly without animations
    /// </summary>
    /// <param name="position"></param>
    public void SetPositionInstantly(string position)
    {
        var originalDashSpeed = dashSpeed;
        // Instant
        dashSpeed = 0.0f;
        
        SetPosition(position);
        
        // Return original speed
        dashSpeed = originalDashSpeed;
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