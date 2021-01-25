using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.Chat
{
    public enum BubbleSide
    {
        Left,
        Right
    }
    public abstract class BubbleController : MonoBehaviour
    {
        [Header("Customize Components")]
        [SerializeField] Image characterImage = default;
        [SerializeField] Image characterBubble = default;
        [SerializeField] Image textBubble = default;
        [SerializeField] Image textBubbleShadow = default;
        [SerializeField] TMPro.TMP_Text text = default;
        #region Public Methods
        public virtual void Set(string message, Character character, Sprite sprite)
        {
            SetContent(message);
            SetColors(character.textColor, character.textBColor, character.faceBColor);
            SetSprite(sprite);
            SetSide(character.side);            
        }
        #endregion
        #region View Config
        void SetContent(string content)
        {
            text.text = TextProcess.Process(content);
        }
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
        void SetSprite(Sprite sprite)
        {
            characterImage.sprite = sprite;
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
        #endregion       
    }
} 