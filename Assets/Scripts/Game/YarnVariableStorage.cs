using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class YarnVariableStorage : VariableStorageBehaviour
{
    public override Value GetValue(string variableName)
    {
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
    }

    public override void ResetToDefaults()
    {
        //GameData.YarnVariables.Clear();
    }
}
