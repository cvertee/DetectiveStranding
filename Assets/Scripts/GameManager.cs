using System.Collections;
using System.Collections.Generic;
using Core;
using Save;
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
        var dialogueData = GameData.GetDialogueData();

        if (FindObjectOfType<AudioManager>() == null)
        {
            var soundPrefab = Resources.Load("Prefabs/[SOUND]");
            var soundGo = Instantiate(soundPrefab);
        }
        
        if (!string.IsNullOrEmpty(dialogueData.lastNode))
        {
            dialogueRunner.startNode = dialogueData.lastNode;
        }
        
        dialogueRunner.onNodeStart.AddListener((node) =>
        {
            Debug.Log($"OnNodeStart:{node}");
            dialogueData.lastNode = node;
            
            GameData.AddVisitedNode(node);
        });
    }

    private void Start()
    {
        var clickableData = GameData.GetClickableData();
        
        if (clickableData.isShown && clickableData.name != null)
        {
            ObjectSpawner.Instance.SpawnResource($"Prefabs/Clickables/{clickableData.name}");
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
        var sceneData = GameData.GetSceneData();
        sceneData.name = sceneName;
        sceneData.loadedAdditively = false;
        
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAdditively(string sceneName)
    {
        var sceneData = GameData.GetSceneData();
        sceneData.name = sceneName;
        sceneData.loadedAdditively = true;
        
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void SpeedupAutoSkip()
    {
        GameData.autoSkipWaitTime = 0.0f;
    }

    public void RestoreAutoSkipSpeed()
    {
        GameData.autoSkipWaitTime = 0.2f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
