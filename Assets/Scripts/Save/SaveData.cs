using System;
using System.Collections.Generic;
using Yarn;

namespace Save
{
    [Serializable]
    public class SaveData
    {
        public string sceneName;
        public Dictionary<string, Value> yarnVariables;
        public bool sceneLoadedAdditively;
        public string yarnNode;
    }
}