using System.Collections;
using UnityEngine;
using Loader;
using Fisiomed.Chat;
using Fisiomed.Quiz;

namespace Fisiomed.Case
{
    public class CaseController : Singleton<CaseController>
    {
        [Header("Data")]
        [SerializeField] string defaultUrl;
        [Header("Visuals")]
        [SerializeField] float loaderTime;
        CCase ccase;
        // Start is called before the first frame update
        void Start()
        {
            AppManager.Instance.ShowLoader(true);
            StartCoroutine(Downloader.Instance.LoadJSON(defaultUrl, OnMetadataLoaded));
            StartCoroutine(WaitLoader());
        }
        void OnMetadataLoaded(string json)
        {
            ccase = JsonUtility.FromJson<CCase>(json);            
            PresentationController.Instance.Set(ccase.info);
            ChatController.Instance.Set(ccase.chat);
            QuizController.Instance.Set(ccase.quiz);         
        }
        IEnumerator WaitLoader()
        {
            yield return new WaitForSeconds(loaderTime);
            AppManager.Instance.ShowLoader(false);
        }
    }
}
