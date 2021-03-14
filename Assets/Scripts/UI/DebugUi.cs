using System;
using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DebugUi : MonoBehaviour
{
    private bool isEnabled;

    private AudioManager audioManager;

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
    }

    private void ShowButton(string text, Action action)
    {
        if (GUILayout.Button(text))
            action();
    }
}