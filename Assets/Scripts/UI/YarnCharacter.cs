using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

// Where character's name is displayed in dialogue
public class YarnCharacter : MonoBehaviour
{
    private TextMeshProUGUI text;

    private Dictionary<string, string> aliasFullnameDatabase;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        aliasFullnameDatabase = ResourceLoader.LoadAliasFullnameDatabase();
        
        Assert.IsTrue(aliasFullnameDatabase != null && aliasFullnameDatabase.Count > 0);
        
        GameEvents.Instance.onYarnLineStart.AddListener(
            (args) =>
            {
                var characterAlias = args.Split(':')[0];
                
                if (aliasFullnameDatabase.TryGetValue(characterAlias ?? string.Empty,out var fullName))
                {
                    text.text = fullName;
                }
                else
                {
                    text.text = characterAlias;
                }
            }
        );
    }
}