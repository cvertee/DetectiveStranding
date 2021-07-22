using System;
using Save;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;

namespace Core
{
    public static class GameData
    {
        // TODO: move to other class
        public static float autoSkipWaitTime = 0.2f;
        
        // This list is mostly used for debugging, for now visited nodes aren't saved
        // Will still have duplicates for comfy node tracing
        // To not fuck up someone's ram only editor will have luxury to add to this list
        // TODO: maybe replace with debug builds?
        private static readonly List<string> visitedNodes = new List<string>();
        
        private static SaveData _saveData = new SaveData();

        public static SaveData GetSaveData()
        {
            Debug.LogWarning($"GameData: Gaining access to whole save data object");

            return _saveData;
        }
        
        public static void UpdateSaveData(SaveData newSaveData)
        {
            Debug.LogWarning($"GameData: Updating save data!");

            _saveData = newSaveData;
        }

        public static ClickableData GetClickableData()
        {
                Debug.Log($"GameData: Accessing Clickable Data");
            return _saveData.clickableData;
        }

        public static SceneData GetSceneData()
        {
            Debug.Log($"GameData: Accessing Scene Data");
            return _saveData.sceneData;
        }

        public static DialogueData GetDialogueData()
        {
            Debug.Log($"GameData: Accessing Dialogue Data");
            return _saveData.dialogueData;
        }

        // TODO: Works as a temporary solution
        public static List<StoryResult> GetStoryResults() => new List<StoryResult>();

        public static void AddVisitedNode(string node)
        {
            #if UNITY_EDITOR
            visitedNodes.Add(node);
            #endif
        }
        public static List<string> GetVisitedNodes()
        {
            return visitedNodes;
        }
    }
}