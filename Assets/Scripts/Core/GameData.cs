using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;

namespace Core
{
    public static class GameData
    {
        public static string sceneName;
        public static bool sceneLoadedAdditively = true;
        public static Dictionary<string, Value> YarnVariables = new Dictionary<string, Value>();
        public static string yarnNode;
    }
}