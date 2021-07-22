using System;
using System.Text;
using Core;
using Newtonsoft.Json;
using Save;
using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DebugUi : MonoBehaviour
{
    private bool isEnabled;

    private AudioManager audioManager;

    private string yarnNodeTextField = "";

    private void Start()
    {
        isEnabled = false;

        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isEnabled = !isEnabled;
        }
    }

    private void OnGUI()
    {
        #if UNITY_EDITOR
        if (!isEnabled)
            return;
        
        ShowButton("Play music", () => audioManager.PlayMusic("Time"));
        ShowButton("Stop music", () => audioManager.StopMusic());
        
        ShowButton("Play sound", () => audioManager.PlaySound("lol"));
        
        ShowButton("Play ambient", () => audioManager.PlayAmbient("Office"));
        ShowButton("Stop ambient", () => audioManager.StopAmbient());
        
        ShowButton("Try to save", () =>
        {
            SaveSystem.Save();
        });
        
        ShowButton("Try to load", () =>
        {
            SaveSystem.Load();
        });

        yarnNodeTextField = GUILayout.TextField(yarnNodeTextField);
        ShowButton("^ Start node", () =>
        {
            GameEvents.Instance.onSceneSwitchRequested.Invoke(yarnNodeTextField);
            FindObjectOfType<GameManager>().StartYarnNode(yarnNodeTextField);
        });
        
        ShowButton("Print story results", () =>
        {
            Debug.Log("==== STORY RESULTS ====");
            foreach (var storyResult in GameData.GetStoryResults())
            {
                Debug.Log($"{storyResult.name} - {storyResult.displayName}");
            }
            Debug.Log("==== END ====");
        });

        ShowButton("Print yarn node trace", () =>
        {
            var sb = new StringBuilder();
            
            // Go through each visited node and add it to whole string to print it once
            foreach (var visitedNode in GameData.GetVisitedNodes())
            {
                sb.AppendLine(visitedNode);
            }

            Debug.Log($"Node trace\n{sb}");
        });

        ShowButton("Spawn an", () =>
        {
            GameEvents.Instance.onCharacterSpawnRequested.Invoke(new string[] {"an", "default", Character.MoveType.Center});
        });
        ShowButton("Spawn clickable act1", () =>
        {
            GameEvents.Instance.onSceneSwitchRequested.Invoke("act1");
        });
        ShowButton("Show test image", () =>
        {
            GameEvents.Instance.onImageShowRequested.Invoke("TestImage");
        });
        ShowButton("Hide test image", () =>
        {
            GameEvents.Instance.onImageHideRequested.Invoke();
        });
        ShowButton("-", () => Debug.Log("This button does nothing"));
        ShowButton("Superfast enable", () =>
        {
            FindObjectOfType<GameManager>().SpeedupAutoSkip();
        });
        ShowButton("Superfast disable", () =>
        {
            FindObjectOfType<GameManager>().RestoreAutoSkipSpeed();
        });
        #endif
    }

    private void ShowButton(string text, Action action)
    {
        if (GUILayout.Button(text))
            action();
    }
}