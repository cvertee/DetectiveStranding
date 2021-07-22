using System;
using System.Collections.Generic;
using Yarn;

namespace Save
{
    [Serializable]
    public class SceneData
    {
        public string name;
        public bool loadedAdditively;
    }

    // It contains info about recently used/shown clickables
    [Serializable]
    public class ClickableData
    {
        public string name;
        public bool isShown;
    }
    
    [Serializable]
    public class DialogueData
    {
        // Recently started Yarn node
        public string lastNode;
        
        // Yarn variables database
        // 
        // TODO: Due to temporary unfixed serialization bug
        // Whole yarn variables storage only checks if variable
        // Is saved, if so returns true to yarn or false otherwise.
        // So for now it is not using values as they are saved
        public Dictionary<string, Value> variableValue = new Dictionary<string, Value>();
        
        public List<StoryResult> storyResults = new List<StoryResult>();

        public bool HasVariable(string variable) => variableValue.ContainsKey(variable);

        public void AddOrUpdateVariable(string variable, Value value)
        {
            switch (value.AsBool)
            {
                case true:
                    if (!HasVariable(variable))
                        variableValue.Add(variable, value);
                    break;
                
                case false:
                    variableValue.Remove(variable);
                    break;
            }

        }
    }
    
    [Serializable]
    public class SaveData
    {
        public ClickableData clickableData = new ClickableData();
        public SceneData sceneData = new SceneData();
        public DialogueData dialogueData = new DialogueData();
    }
}