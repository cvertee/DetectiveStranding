using System;
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
        if (!isEnabled)
            return;

        ShowButton("Reload", () => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        
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

        ShowButton("Try to LOAD from initial JSON", () =>
        {
            var json = Resources.Load<TextAsset>("EmptySave").text;
            Debug.Log($"Loaded {json}");

            var saveData = JsonConvert.DeserializeObject<SaveData>(json);

            SaveSystem.LoadFrom(saveData);
        });

        ShowButton("Try to LOAD from pre-created JSON", () =>
        {
            var json = Resources.Load<TextAsset>("TestSave").text;
            Debug.Log($"Loaded {json}");

            var saveData = JsonConvert.DeserializeObject<SaveData>(json);

            SaveSystem.LoadFrom(saveData);
        });

        yarnNodeTextField = GUILayout.TextField(yarnNodeTextField);
        ShowButton("^ Start node", () => FindObjectOfType<GameManager>().StartYarnNode(yarnNodeTextField));
    }

    private void ShowButton(string text, Action action)
    {
        if (GUILayout.Button(text))
            action();
    }
}