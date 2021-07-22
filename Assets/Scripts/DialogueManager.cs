using Core;
using Sound;
using System.Collections;
using Save;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    private DialogueUI dialogueUi;
    private DialogueRunner dialogueRunner;
    private GameManager gameManager;

    private string lastLoadedScene;

    private void Awake()
    {
        dialogueUi = FindObjectOfType<DialogueUI>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        gameManager = FindObjectOfType<GameManager>();

#if UNITY_EDITOR
        dialogueUi.textSpeed = 0.0f;
#endif

        dialogueUi.onLineStart.AddListener((arg) => GameEvents.Instance.onYarnLineStart.Invoke(arg));
        dialogueRunner.onDialogueComplete.AddListener(() => GameEvents.Instance.onYarnDialogueEnd.Invoke());

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
        
        // si = ShowImage
        dialogueRunner.AddCommandHandler("si", args =>
        {
            var imageId = args[0];
            
            GameEvents.Instance.onImageShowRequested.Invoke(imageId);
        });

        // ci = CloseImage
        dialogueRunner.AddCommandHandler("ci", args =>
        {
            GameEvents.Instance.onImageHideRequested.Invoke();
        });

        dialogueRunner.AddCommandHandler("switchScene", args =>
        {
            var clickableName = args[0];
            
            GameEvents.Instance.onSceneSwitchRequested.Invoke(clickableName);
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
            AudioManager.Instance.StopMusic();
        });

        dialogueRunner.AddCommandHandler("playAmbient", args =>
        {
            var soundName = args[0];

            AudioManager.Instance.PlayAmbient(soundName);
        });

        dialogueRunner.AddCommandHandler("stopAllAmbient", args =>
        {
            AudioManager.Instance.StopAmbient();
        });
        
        dialogueRunner.AddCommandHandler("save", (parameters, complete) =>
        {
            SaveSystem.Save();
            complete();
        });
        
        dialogueRunner.AddCommandHandler("load", args =>
        {
            SaveSystem.Load();
        });
        
        dialogueRunner.AddCommandHandler("addStoryResult", args =>
        {
            return; // NOTE: deprecated for now
            var storyResult = Resources.Load<StoryResult>($"storyboard/{args[0]}");
            if (storyResult == null)
            {
                Debug.LogWarning($"Something went wrong on loading story result ({args[0]})");
                return;
            }
            
            GameData.GetStoryResults().Add(storyResult);
            
            GameEvents.Instance.onStoryResultAdded.Invoke();
        });

        GameEvents.Instance.onDialogueNextPhraseRequested.AddListener(() => dialogueUi.MarkLineComplete());

        StartCoroutine(DialogueSkipper());
    }

    private IEnumerator DialogueSkipper()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.LeftControl))
                dialogueUi.MarkLineComplete();
            yield return new WaitForSeconds(GameData.autoSkipWaitTime);
        }
    }
}
