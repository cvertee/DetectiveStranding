using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

// Spawns and initiates Character mono behaviours
// Kinda `factory at home:`
public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;

    private readonly List<Character> spawnedCharacters = new List<Character>();

    private void Awake()
    {
        GameEvents.Instance.onCharacterSpawnRequested.AddListener(
            args =>
            {
                var characterName = args[0];
                var emotion = args[1];
                var position = args[2];

                Spawn(characterName, emotion, position);
            });

        // On character remove request
        // Find and remove character in list of spawned characters
        GameEvents.Instance.onCharacterRemoveRequested.AddListener(
            characterName =>
            {
                var character = spawnedCharacters.FirstOrDefault(x => x.name == characterName);
                if (character != null)
                    spawnedCharacters.Remove(character);
            }
        );
    }

    private void Spawn(string characterName, string emotion, string position)
    {
        Debug.Log($"Trying to spawn {characterName}/{emotion}/{position}");
        
        Character character;

        // Check if character is already spawned
        if (spawnedCharacters.Any(x => x.name == characterName))
        {
            Debug.LogWarning($"Found already spawned character {characterName}");
            
            // If so, set character variable to already existing
            character = spawnedCharacters.First(x => x.name == characterName);
        }
        else
        {
            // Or create new one by instantiating prefab
            var characterGo = Instantiate(characterPrefab);
            character = characterGo.GetComponent<Character>();
            
            // Add new character to the spawned list
            spawnedCharacters.Add(character);
            
            // Character's prefab spawns somewhere else and it causes sliding bug...
            // This makes character spawn at right position and not weirdly slide to right or left side
            character.SetPositionInstantly(position);
        }
        
        // Change required properties
        character.SetName(characterName);
        character.SetEmotion(emotion);
        character.SetPosition(position); // Calling it after SetPositionInstantly doesn't bring much problems
        character.InitializeImage();

        GameEvents.Instance.onCharacterSpawned.Invoke(character);
    }
}