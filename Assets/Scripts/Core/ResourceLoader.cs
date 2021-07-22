using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class ResourceLoader
    {
        public static string ReadTextFromFile(string fullPath)
        {
            return Resources.Load<TextAsset>(fullPath).text;
        }

        public static UnityEngine.Object LoadClickable(string clickableName) => Resources.Load($"Prefabs/Clickables/{clickableName}");

        public static Sprite LoadShownImage(string id) => Resources.Load<Sprite>($"misc/si/{id}");
    }
}