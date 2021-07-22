using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class DebugGameManager : MonoBehaviour
{
    private Character currentCharacter;

    // Start is called before the first frame update
    void Awake()
    {
        GameEvents.Instance.onCharacterSpawned.AddListener((c) => currentCharacter = c);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            currentCharacter?.SetPosition(Character.MoveType.Left);
        else if (Input.GetKeyDown(KeyCode.S))
            currentCharacter?.SetPosition(Character.MoveType.Center);
        else if (Input.GetKeyDown(KeyCode.D))
            currentCharacter?.SetPosition(Character.MoveType.Right);
    }
}
