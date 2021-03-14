using System.Linq;
using Core;
using UnityEngine;
public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject charactersContainer;

    private void Start()
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
            var characterGo = Instantiate(characterPrefab, charactersContainer.transform);
            character = characterGo.GetComponent<Character>();
        }
        
        // and always change all these properties
        character.SetName(characterName);
        character.SetEmotion(emotion);
        character.SetPosition(position);
        character.InitializeImage();
    }
}