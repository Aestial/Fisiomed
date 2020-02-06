using UnityEngine;

namespace Fisiomed.Video
{
    /// <summary>
    /// <c>FullscreenUISkin</c> defines the Asset file (ScriptableObject) for
    /// saving the values of the VideoPlayer's Fullscreen mode UI skin.
    /// 
    /// \Author: Jaime Hernandez
    /// \Date: May 2019
    /// </summary>
    [CreateAssetMenu(fileName = "New Fullscreen HUD Skin", menuName = "Fullscreen Skin")]
    public class FullscreenUISkin : ScriptableObject
    {
        public new string name;
        public string description;
        public Color backgroundColor;
        public Color buttonsColor;
        public Color buttonsBackColor;
        public Color panelsColor;
        public Color scrubHandleColor;

        // TODO: Maybe we can customize buttons too??
        //public Sprite playButton;
        //public Sprite pauseButton;
        //public Sprite loopButton;

        public void Print()
        {
            Debug.Log("Fullscreen Video - SKIN Configuration");
            Debug.Log("Fullscreen Video - Skin name: " + this.name);
            Debug.Log("Fullscreen Video - Skin description: " + this.description);
        }
    }
}