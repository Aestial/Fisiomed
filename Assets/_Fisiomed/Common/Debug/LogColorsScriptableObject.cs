using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[System.Serializable]
public class DebugColor
{
    public Color color;
    public bool isActive;
}

/// <summary>
/// <c>LogColorsScriptableObject</c> defines the Asset file (ScriptableObject) for
/// saving the relationship between Classes names and Debug messages colors.
/// 
/// \Author: Jaime Hernandez
/// \Date: 2019-09-12
/// </summary>
[CreateAssetMenu(fileName = "New_LogColors", menuName = "Debug/Log Colors")]
public class LogColorsScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class StringTypeColors : SerializableDictionaryBase<string, DebugColor> { }
    public Color defaultColor = Color.black;
    public StringTypeColors classColors;  
}
