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

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        GameEvents.Instance.onYarnLineStart.AddListener(
            (args) =>
            {
                var characterAlias = args.Split(':')[0];

                text.text = CharacterAliasResolver.Resolve(characterAlias);
            }
        );
    }
}