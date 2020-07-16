using System.Collections;
using UnityEngine;

namespace Fisiomed.Popup
{
    public class PopupManager : Singleton<PopupManager>
    {
        [SerializeField] Canvas canvas = default;
        [SerializeField] GameObject cardPrefab = default;
        [SerializeField] Transform cardContainer = default;        

        public void PrintMessage(string message)
        {
            GameObject newCard = Instantiate(cardPrefab, cardContainer);
            PopupCardController controller = newCard.GetComponent<PopupCardController>();
            controller.Print(message);
            canvas.enabled = true;
            StopAllCoroutines(); 
            StartCoroutine(CheckForDeactivation());
        }

        public void CleanMessages()
        {
            for (int i = 0; i < cardContainer.childCount; i++)
            {
                Destroy(cardContainer.GetChild(i).gameObject);
            }
            canvas.enabled = false;
        }

        private IEnumerator CheckForDeactivation()
        {            
            while (cardContainer.childCount > 0)
            {
                yield return null;
            }
            canvas.enabled = false;
        }

        public bool iAmFirst;

        void Awake()
        {            
            DontDestroyOnLoad(Instance);
            PopupManager[] managers = FindObjectsOfType(typeof(PopupManager)) as PopupManager[];

            if (managers.Length > 1)
            {
                for (int i = 0; i < managers.Length; i++)
                {
                    if (!managers[i].iAmFirst)
                    {
                        DestroyImmediate(managers[i].gameObject);
                    }
                }
            }
            else
            {
                iAmFirst = true;
            }
        }
        void Start()
        {
            CleanMessages();
        }

        void OnDestroy()
        {
            CleanMessages();
        }
    }
}
