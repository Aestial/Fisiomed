using UnityEngine;
using TMPro;

namespace Fisiomed.Feedback
{
    public class FeedbackController : Singleton<FeedbackController>
    {
        [SerializeField] Canvas canvas = default;
        [SerializeField] TMP_Text text = default;
        [SerializeField] Canvas rightCanvas = default;
        [SerializeField] Canvas wrongCanvas = default;
        string caller;
        Notifier notifier = new Notifier();
        public const string ON_MODAL_CLOSED = "OnModalClosed";
        #region Public Methods 
        public void Show(string caller, string message, bool isCorrect)
        {
            this.caller = caller;
            SelectTitleCanvas(isCorrect);
            text.text = message;
            canvas.enabled = true;
        }
        public void Close()
        {
            canvas.enabled = false;
            notifier.Notify(ON_MODAL_CLOSED, caller);
        }
        #endregion
        void SelectTitleCanvas(bool isCorrect)
        {
            rightCanvas.enabled = isCorrect;
            wrongCanvas.enabled = !isCorrect;
        }
        void Start()
        {
            canvas.enabled = false;
        }    
    }
}
