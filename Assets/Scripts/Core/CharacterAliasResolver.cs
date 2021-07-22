using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

public class CharacterAliasResolver : MonoBehaviour
{
    private static CharacterAliasResolver _instance;
    
    [SerializeField] private CharacterAliasSO aliasData;
    
    private void Awake()
    {
        _instance = this;
    }

    public static string Resolve(string alias)
    {
        var aliasFullnameList = _instance.aliasData.aliasFullnameList;
        var result = aliasFullnameList.FirstOrDefault(x => x.alias == alias)?.fullname;

        return result ?? alias;
    }
}
