using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StoryboardItem", menuName = "Story/Story result", order = 0)]
public class StoryResult : ScriptableObject
{
    public Sprite displayImage;
    public string displayName;
    [TextArea]
    public string description;
}
