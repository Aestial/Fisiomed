using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.Video
{
    /// <summary>
    /// <c>FullscreenUIController</c> shows and hides VideoPlayer's Fullscreen
    /// canvases with a public method. Sets the skin colors for some elements.
    /// TODO: Make independent and complete SkinManager for Fullscreen UI.
    /// 
    /// \Author: Jaime Hernandez
    /// \Date: May 2019
    /// </summary>
    public class FullscreenUIController : MonoBehaviour
    {
        [Header("Canvas elements for show and hide")]
        [SerializeField] Canvas videoCanvas;
        [SerializeField] Canvas controlCanvas;

        [Header("Skin object and customizable UI elements")]
        [SerializeField] FullscreenUISkin skin;
        [SerializeField] Image backgroundImage;
        [SerializeField] Image scrubHandleImage;
        [SerializeField] Image[] panelsImages;
        [SerializeField] Image[] buttonsImages;
        [SerializeField] Image[] buttonsBackImages;

        public void Show(bool enabled)
        {
            videoCanvas.enabled = enabled;
            controlCanvas.enabled = enabled;
        }

        void Start()
        {
            //SetColors();
        }

        private void SetColors()
        {
            //skin.Print();
            backgroundImage.color = skin.backgroundColor;
            scrubHandleImage.color = skin.scrubHandleColor;
            SetImageArrayColors(panelsImages, skin.panelsColor, false);
            SetImageArrayColors(buttonsImages, skin.buttonsColor, true);
            SetImageArrayColors(buttonsBackImages, skin.buttonsBackColor, false);
        }

        private void SetImageArrayColors(Image[] images, Color color, bool preserveAlpha)
        {
            for (int i = 0; i < images.Length; i++)
            {
                float alpha = preserveAlpha ? images[i].color.a : color.a;
                Color c = new Color(color.r, color.g, color.b, alpha);
                images[i].color = c;
            }
        }
    }
}
