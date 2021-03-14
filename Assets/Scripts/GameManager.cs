using System.Collections;
using System.Collections.Generic;
using Core;
using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    private DialogueUI dialogueUi;
    private DialogueRunner dialogueRunner;
    private AudioManager audioManager;

    void Awake()
    {
        dialogueUi = FindObjectOfType<DialogueUI>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        audioManager = FindObjectOfType<AudioManager>();
        
        #if UNITY_EDITOR
        dialogueUi.textSpeed = 0.0f;
        #endif
        
        dialogueUi.onLineStart.AddListener((arg) => GameEvents.Instance.onYarnLineStart.Invoke(arg));
        dialogueUi.onDialogueEnd.AddListener(() => GameEvents.Instance.onYarnDialogueEnd.Invoke());
        
        // sc = SpawnCharacter
        dialogueRunner.AddCommandHandler("sc", args =>
        {
            GameEvents.Instance.onCharacterSpawnRequested.Invoke(args);
        });
        
        // se = SetEmotion
        dialogueRunner.AddCommandHandler("se", args =>
        {
            GameEvents.Instance.onCharacterSetEmotionRequested.Invoke(args);
        });
        
        // kc = KillCharacter (rofl)
        dialogueRunner.AddCommandHandler("kc", args =>
        {
            GameEvents.Instance.onCharacterRemoveRequested.Invoke(args[0]);
        });
        
        // mc = MoveCharacter
        dialogueRunner.AddCommandHandler("mc", args =>
        {
            GameEvents.Instance.onCharacterMoveRequested.Invoke(args);
        });
        
        // cb = ChangeBackground
        // dialogueRunner.AddCommandHandler("cb", args =>
        // {
        //     Debug.Log($"got cb {args[0]}");
        //     GameEvents.Instance.onBackgroundChangeRequest.Invoke(args[0]);
        // });
        
        dialogueRunner.AddCommandHandler("switchScene", args =>
        {
            SceneManager.LoadScene(args[0]);
        });
        
        dialogueRunner.AddCommandHandler("showDialogue", args =>
        {
            GameEvents.Instance.onDialogueContainerShowRequested.Invoke();
        });
        
        dialogueRunner.AddCommandHandler("hideDialogue", args =>
        {
            GameEvents.Instance.onDialogueContainerHideRequested.Invoke();
        });
        
        dialogueRunner.AddCommandHandler("playSound", args =>
        {
            var soundName = args[0];

            AudioManager.Instance.PlaySound(soundName);
        });
        
        dialogueRunner.AddCommandHandler("playMusic", args =>
        {
            var soundName = args[0];

            AudioManager.Instance.PlayMusic(soundName);
        });
        
        dialogueRunner.AddCommandHandler("stopAllMusic", args =>
        {
            //var soundName = args[0];

            AudioManager.Instance.StopMusic();  
        });
        
        dialogueRunner.AddCommandHandler("playAmbient", args =>
        {
            var soundName = args[0];

            AudioManager.Instance.PlayAmbient(soundName);
        });
        
        dialogueRunner.AddCommandHandler("stopAllAmbient", args =>
        {
            //var soundName = args[0];

            AudioManager.Instance.StopAmbient();
        });

        GameEvents.Instance.onDialogueNextPhraseRequested.AddListener(() => dialogueUi.MarkLineComplete());
        
        StartCoroutine(DialogueSkipper());
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueUi.MarkLineComplete();
        }
    }

    private IEnumerator DialogueSkipper()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.LeftControl))
                dialogueUi.MarkLineComplete();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void StartYarnNode(string node)
    {
        dialogueRunner.StartDialogue(node);
    }
}
