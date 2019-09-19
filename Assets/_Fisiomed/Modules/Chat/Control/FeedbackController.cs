using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fisiomed.Chat
{
    public class FeedbackController : MonoBehaviour
    {
        [SerializeField] Canvas canvas;
        [SerializeField] TMP_Text text;
        [SerializeField] Canvas rightCanvas;
        [SerializeField] Canvas wrongCanvas;
        #region Public Methods 
        public void Show(string message, bool isCorrect)
        {
            SelectTitleCanvas(isCorrect);
            text.text = message;
            canvas.enabled = true;
        }
        public void Close()
        {
            canvas.enabled = false;
        }
        #endregion
        void SelectTitleCanvas(bool isCorrect)
        {
            rightCanvas.enabled = isCorrect;
            wrongCanvas.enabled = !isCorrect;
        }
        // Start is called before the first frame update
        void Start()
        {
            canvas.enabled = false;
        }    
    }
}
