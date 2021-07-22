using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class ClickableItemLoader : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.Instance.onSceneSwitchRequested.AddListener(LoadClickable);
    }

    private void LoadClickable(string clickableName)
    {
        var clickableData = GameData.GetClickableData();
        var clickableGo = ResourceLoader.LoadClickable(clickableName);
        if (clickableGo == null)
        {
            Debug.LogWarning($"There is no clickable items for {clickableName}");
            clickableData.isShown = false;
            return;
        }

        Debug.Log($"{nameof(ClickableItemLoader)}: Spawning {clickableName}");
        Instantiate(clickableGo);

        clickableData.name = clickableName;
        clickableData.isShown = true;
    }
}
