using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class YarnVariableStorage : VariableStorageBehaviour
{
    private void Start()
    {
        #if UNITY_EDITOR
        ParseYarnVariables();
        #endif
    }

    private void ParseYarnVariables()
    {
        var variablesText = ResourceLoader.ReadTextFromFile("misc/DevYarnVars");

        foreach (var line in variablesText.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries))
        {
            var variable = line[0] == '$' ? line : $"${line}";
            SetValue(variable, true);
        }
    }
    
    public override Value GetValue(string variableName)
    {
        Debug.Log($"Yarn: Trying to get yarn variable {variableName}");
        if (!GameData.YarnVariables.TryGetValue(variableName, out var variable))
            return new Value(false);
        else
            return variable;
    }

    public override void SetValue(string variableName, Value value)
    {
        if (GameData.YarnVariables.ContainsKey(variableName))
            GameData.YarnVariables[variableName] = value;
        else
            GameData.YarnVariables.Add(variableName, value);
        
        Debug.Log($"Set {variableName} to some value");
    }

    public override void ResetToDefaults()
    {
        //GameData.YarnVariables.Clear();
    }
}
