using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class ResourceLoader
    {
        private const string AliasFullnameDatabasePath = "misc/CharactersAliases";

        public static string ReadTextFromFile(string fullPath)
        {
            return Resources.Load<TextAsset>(fullPath).text;
        }
        
        public static Dictionary<string, string> LoadAliasFullnameDatabase()
        {
            // TODO: different languages
            var text = ReadTextFromFile(AliasFullnameDatabasePath + "_ru");
            var database = new Dictionary<string, string>();

            foreach (var line in text.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries))
            {
                var splittedLine = line.Split(':');
                var alias = splittedLine[0].Trim();
                var fullName = splittedLine[1].Trim();

                database.Add(alias, fullName);
            }

            return database;
        }
    }
}