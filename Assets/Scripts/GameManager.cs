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

    void Awake()
    {
        dialogueUi = FindObjectOfType<DialogueUI>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        if (FindObjectOfType<AudioManager>() == null)
        {
            var soundPrefab = Resources.Load("Prefabs/[SOUND]");
            var soundGo = Instantiate(soundPrefab);
        }
        
        if (!string.IsNullOrEmpty(GameData.Data.yarnNode))
        {
            dialogueRunner.startNode = GameData.Data.yarnNode;
        }
        
        dialogueRunner.onNodeStart.AddListener((node) =>
        {
            Debug.Log($"OnNodeStart:{node}");
            GameData.Data.yarnNode = node;
        });
    }

    private void Start()
    {
        if (GameData.Data.sceneName != null)
        {
            ObjectSpawner.Instance.SpawnResource($"Prefabs/Clickables/{GameData.Data.sceneName}");
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueUi.MarkLineComplete();
        }
    }
    
    public void StartYarnNode(string node)
    {
        dialogueRunner.StartDialogue(node);
    }

    public void LoadScene(string sceneName)
    {
        GameData.Data.sceneName = sceneName;
        GameData.Data.sceneLoadedAdditively = false;
        //GameEvents.Instance.onSceneSwitchRequested.Invoke();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAdditively(string sceneName)
    {
        GameData.Data.sceneName = sceneName;
        GameData.Data.sceneLoadedAdditively = true;
        //GameEvents.Instance.onSceneSwitchRequested.Invoke();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
