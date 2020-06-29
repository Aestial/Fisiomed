using System.Collections;
using UnityEngine;
using TMPro;

namespace Fisiomed.Popup
{
    public class PopupCardController : MonoBehaviour
    {
        [SerializeField] TMP_Text text;
        [SerializeField] float lifeTime = 8.0f;

        void Start()
        {
            StartCoroutine(DestroyCoroutine(lifeTime));
        }

        IEnumerator DestroyCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }

        public void Print(string msg)
        {
            text.text = msg;
        }
    }
}
