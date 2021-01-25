using UnityEngine;

namespace Fisiomed.Chat
{
    using App;
    
    public class EndController : BubbleController
    {        
        public void Set(Video video, Character character, Sprite sprite)
        {
            base.Set(video.text, character, sprite);
        }        
    }
}