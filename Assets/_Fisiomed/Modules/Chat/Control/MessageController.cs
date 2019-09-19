using UnityEngine;
using UnityEngine.UI;
using Loader;

namespace Fisiomed.Chat
{
    public enum BubbleSide
    {
        Left,
        Right
    }

    public class MessageController : MonoBehaviour
    {
        [Header("Customize Components")]
        [SerializeField] Image characterImage;
        [SerializeField] Image characterBubble;
        [SerializeField] Image textBubble;
        [SerializeField] Image textBubbleShadow;
        [SerializeField] Text text;
        #region Public Methods
        public void Set(Message message, Character character, Sprite sprite)
        {           
            SetColors(character.textColor, character.textBColor, character.faceBColor);
            SetContent(message.text);
            SetSide(character.side);                                    
            SetSprite(sprite);            
        }
        #endregion
        void SetColors(string text, string textB, string faceB)
        {
            Color textColor = new Color();
			Color textBColor = new Color();
			Color faceBColor = new Color();
			ColorUtility.TryParseHtmlString (text, out textColor);
			ColorUtility.TryParseHtmlString (textB, out textBColor);
			ColorUtility.TryParseHtmlString (faceB, out faceBColor);
            SetColors(textColor, textBColor, faceBColor);
        }
        void SetColors(Color textColor, Color textBColor, Color charBColor)
        {            
            text.color = textColor;
            textBubble.color = textBColor;
            characterBubble.color = charBColor;            
        }
        void SetContent(string content)
        {
            text.text = content;
        }
        void SetSide(string input)
        {
            BubbleSide side = input == "left" ? BubbleSide.Left:BubbleSide.Right;
            float radius = 60f;
            SetSide(side, radius);
        }
        void SetSide(BubbleSide side, float radius)
        {
            Vector4 radiusRect = new Vector4(radius, radius, radius, radius);
            switch(side)
            {
                case BubbleSide.Right:
                    radiusRect.y = 0.0f;
                    break;
                default:
                    radiusRect.x = 0.0f;
                    break;                
            }
            textBubbleShadow.GetComponent<FreeModifier>().Radius = radiusRect;
            textBubble.GetComponent<FreeModifier>().Radius = radiusRect;            
            if (side == BubbleSide.Right)
                transform.GetChild(0).SetAsLastSibling();
        }
        void SetSprite(Sprite sprite)
        {
            characterImage.sprite = sprite;
        }
        #region Not Recommended
        void SetSprite(string url)
        {
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
                StartCoroutine(Downloader.Instance.LoadTexture(url, OnTextureLoaded));    
        }        
        void OnTextureLoaded(Texture texture)
		{
			Texture2D tex = (Texture2D) texture;
			Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
			SetSprite(sprite);
		}
        #endregion        
    }    
}
