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

        var dialogueData = GameData.GetDialogueData();
        var hasVariable = dialogueData.HasVariable(variableName);

        return new Value(hasVariable);
    }

    public override void SetValue(string variableName, Value value)
    {
        Debug.Log($"Yarn: Set value {variableName}");
        
        var dialogueData = GameData.GetDialogueData();
        
        dialogueData.AddOrUpdateVariable(variableName, value);
    }

    public override void ResetToDefaults()
    {
        //GameData.YarnVariables.Clear();
    }
}
