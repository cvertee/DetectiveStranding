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
        
        dialogueRunner.AddCommandHandler("si", args =>
        {
            var imageId = args[0];
            ImageDisplayer.Show(imageId);
        });
        
        dialogueRunner.AddCommandHandler("ci", args =>
        {
            ImageDisplayer.Hide();
        });

        // cb = ChangeBackground
        // dialogueRunner.AddCommandHandler("cb", args =>
        // {
        //     Debug.Log($"got cb {args[0]}");
        //     GameEvents.Instance.onBackgroundChangeRequest.Invoke(args[0]);
        // });

        dialogueRunner.AddCommandHandler("switchScene", args =>
        {
            var clickableName = args[0];
            
            GameEvents.Instance.onSceneSwitchRequested.Invoke();

            var clickableGo = Resources.Load($"Prefabs/Clickables/{clickableName}");
            if (clickableGo == null)
            {
                Debug.LogWarning($"There is no clickable items for {clickableName}");
                GameData.Data.sceneName = null;
                return;
            }
            
            Debug.Log($"switchScene: Spawning {clickableName}");
            Instantiate(clickableGo);

            GameData.Data.sceneName = clickableName;
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
            var storyResult = Resources.Load<StoryResult>($"storyboard/{args[0]}");
            if (storyResult == null)
            {
                Debug.LogWarning($"Something went wrong on loading story result ({args[0]})");
                return;
            }
            
            GameData.Data.storyResults.Add(storyResult);
            
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
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
