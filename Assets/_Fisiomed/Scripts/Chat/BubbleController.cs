using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.Chat
{
    public enum BubbleSide
    {
        Left,
        Right
    }

    public class BubbleController : MonoBehaviour
    {
        
        [Header("Current Values")]
        public string content;
        public Sprite characterSprite;
        public BubbleSide side;
        public float radius;
        public Color charBColor;
        public Color textBColor;
        public Color textColor;
        [Header("Customize Components")]
        [SerializeField] Image characterImage;
        [SerializeField] Image characterBubble;
        [SerializeField] Image textBubble;
        [SerializeField] Image textBubbleShadow;
        [SerializeField] Text text;

        public void SetAll()
        {
            SetCharacter(characterSprite);
            SetColors(charBColor, textBColor, textColor);
            SetSide(side, radius);
            SetContent(content);

        }

        void SetContent(string content)
        {
            string description = "Dianita me nombró <b>" + characterImage.sprite.name 
                                    + "</b> y tengo el color: <b>" + charBColor.ToString() 
                                    + "</b>";
            text.text = content;
        }


        void SetCharacter(Sprite sprite)
        {
            characterImage.sprite = sprite;
        }

        void SetColors(Color charBColor, Color textBColor, Color textColor)
        {
            characterBubble.color = charBColor;
            textBubble.color = textBColor;
            text.color = textColor;
        }

        void SetSide(BubbleSide side, float radius)
        {
            Vector4 radiusRect = new Vector4(radius, radius, radius, radius);
            switch(side)
            {
                case BubbleSide.Left:
                    radiusRect.x = 0;
                    break;
                case BubbleSide.Right:
                    transform.GetChild(0).SetAsLastSibling();
                    radiusRect.y = 0;
                    break;
            }
            textBubble.GetComponent<FreeModifier>().Radius = radiusRect;
            textBubbleShadow.GetComponent<FreeModifier>().Radius = radiusRect;
        }
    }    
}
