using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

// Responsible for displaying and manipulating character sprites on screen 
public class Character : MonoBehaviour
{
    public abstract class MoveType
    {
        public const string Right = "right";
        public const string Left = "left";
        public const string Center = "center";
    }
    
    // Each of these positions is used on Setting Position
    // "left" will move character to leftPosition and so on
    public Vector3 leftPosition;
    public Vector3 middlePosition;
    public Vector3 rightPosition;
    
    // Dash speed is the speed with which character moves smoothly between sides
    // Like from Left to Right, etc
    // TODO: move it to Game globals scriptable object?
    public float dashSpeed = 0.25f;
    
    private SpriteRenderer sprite;
    
    // Emotion value basically doesn't do some magic, it is used on loading whole character
    // sprite again with required emotion which is here
    private string emotion;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

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
        name = newName;
    }

    public void InitializeImage()
    {
        sprite.sprite = Resources.Load<Sprite>($"characters/{name}/{name}_{emotion}");
    }
    
    /// <summary>
    /// Reload whole sprite but with new emotion
    /// </summary>
    public void SetEmotion(string newEmotion)
    {
        Debug.Log($"Set {name}`s emotion to {newEmotion}");
        
        emotion = newEmotion;
        
        InitializeImage();
    }

    public void SetPosition(string position)
    {
        Debug.Log($"Set {name}`s position to {position}");
        
        switch (position.ToLower())
        {
            case MoveType.Left:
                LeanTween.moveX(gameObject, leftPosition.x, dashSpeed);
                break;
            
            case MoveType.Center:
                LeanTween.moveX(gameObject, middlePosition.x, dashSpeed);
                break;
            
            case MoveType.Right:
                LeanTween.moveX(gameObject, rightPosition.x, dashSpeed);
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

    /// <summary>
    /// "Kills" character i.e. destroys it
    /// </summary>
    public void Kill() // rofl...
    {
        Debug.Log($"Removing {name}");
        Destroy(gameObject);
    }
    
    //
    // Each of these methods below always check if event is called for them
    // by comparing name to name in argument
    //
    private void OnCharacterSetEmotionRequested(string[] args)
    {
        var argName = args[0];
        var argEmotion = args[1];
                
        if (name == argName)
            SetEmotion(argEmotion);
    }

    private void OnCharacterRemoveRequested(string argName)
    {
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