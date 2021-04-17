using Core;
using Sound;
using System.Collections;
using Save;
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

        // cb = ChangeBackground
        // dialogueRunner.AddCommandHandler("cb", args =>
        // {
        //     Debug.Log($"got cb {args[0]}");
        //     GameEvents.Instance.onBackgroundChangeRequest.Invoke(args[0]);
        // });

        dialogueRunner.AddCommandHandler("switchScene", args =>
        {
            for (int n = 0; n < SceneManager.sceneCount; ++n)
            {
                Scene scene = SceneManager.GetSceneAt(n);
                if (scene.name != "hat1a")
                    SceneManager.UnloadSceneAsync(scene);
            }


            gameManager.LoadSceneAdditively(args[0]);
            lastLoadedScene = args[0];
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
