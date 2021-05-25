using Save;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;

namespace Core
{
    public static class GameData
    {
        private static SaveData data;
        public static SaveData Data
        {
            get
            {
                if (data == null)
                {
                    data = new SaveData
                    {
                        sceneLoadedAdditively = false,
                        sceneName = null,
                        yarnNode = null, // never touch it pls
                        yarnVariables = new Dictionary<string, Value>()
                    };
                }

                return data;
            }

            set
            {
                data = value;
            }
        }
    }
}