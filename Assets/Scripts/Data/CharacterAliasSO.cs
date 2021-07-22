using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class AliasFullname
    {
        public string alias;
        public string fullname;
    }
    
    [CreateAssetMenu(fileName = "CharacterAlias", menuName = "Game Settings/Character aliases", order = 0)]
    public class CharacterAliasSO : ScriptableObject
    {
        public AliasFullname[] aliasFullnameList;
    }
}