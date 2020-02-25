using UnityEngine;

/// <summary>
/// <c>Log</c> is an improved static class for printing Debug.Log with Class information
/// including an associated color to Class name and Namespace (detailed scope).
/// 
/// \Author: Jaime Hernandez
/// \Date: 2019-09-12
/// </summary>
public static class Log
{
    /// <summary>
    /// Color: Debug.Log replace method with Class information and color.
    /// </summary>
    public static void Color<T>(string message, T param)
    {
        string key = param.GetType().ToString();
        var logColors = Resources.Load<LogColorsScriptableObject>("Debug/LogColors");

        if (logColors.classColors.TryGetValue(key, out DebugColor debugColor))
        {
            if (!debugColor.isActive) return;
            string colorString = ColorUtility.ToHtmlStringRGB(debugColor.color);
            Debug.Log("<color=#" + colorString + ">" + param.GetType() + " - " + message + "</color>");
        }
        else
        {
            Color color = logColors.defaultColor;            
            string colorString = ColorUtility.ToHtmlStringRGB(color);
            Debug.LogWarning("<color=#" + colorString + ">" + param.GetType() + " - USING DEFAULT COLOR</color>");
            Debug.Log("<color=#" + colorString + ">" + param.GetType() + " - " + message + "</color>");
        }
    }
    /// <summary>
    /// ColorError: Debug.LogError replace method with Class information and color.
    /// </summary>
    public static void ColorError<T>(string message, T param)
    {
        string key = param.GetType().ToString();
        var logColors = Resources.Load<LogColorsScriptableObject>("Debug/LogColors");

        if (logColors.classColors.TryGetValue(key, out DebugColor debugColor))
        {
            if (!debugColor.isActive) return;
            string colorString = ColorUtility.ToHtmlStringRGB(debugColor.color);            
            Debug.LogError("<color=#" + colorString + ">" + param.GetType() + " - " + message + "</color>");
        }
        else
        {
            debugColor.color = logColors.defaultColor;
            string colorString = ColorUtility.ToHtmlStringRGB(debugColor.color);
            Debug.LogWarning("<color=#" + colorString + ">" + param.GetType() + " - USING DEFAULT COLOR</color>");
            Debug.LogError("<color=#" + colorString + ">" + param.GetType() + " - " + message + "</color>");
        }
    }
}