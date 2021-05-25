using System.Linq;
using Core;
using UnityEngine;
public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject charactersContainer;

    private void Awake()
    {
        GameEvents.Instance.onCharacterSpawnRequested.AddListener(
            args => Spawn(args[0] as string, args[1] as string, args[2] as string)
        );
    }

    private void Spawn(string characterName, string emotion, string position)
    {
        Debug.Log($"Trying to spawn {characterName}/{emotion}/{position}");
        Character character;
        
        var spawnedCharacters = FindObjectsOfType<Character>();
        // check if character is already displayed/spawned
        if (spawnedCharacters.Any(x => x.name == characterName))
        {
            Debug.LogWarning($"Found already spawned character {characterName}");
            // if so, set character to already existing
            character = spawnedCharacters.First(x => x.name == characterName);
        }
        else
        {
            // or create new one
            var characterGo = Instantiate(characterPrefab);
            character = characterGo.GetComponent<Character>();
            // Character's prefab spawns somewhere else and this can cause sliding bug
            // This makes character spawn at right position and not slide to right or left side
            character.SetPositionInstantly(position);
        }
        
        // and always change all these properties
        character.SetName(characterName);
        character.SetEmotion(emotion);
        character.SetPosition(position); // Calling it after SetPositionInstantly doesn't bring much problems
        character.InitializeImage();
    }
}