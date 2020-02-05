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
        [SerializeField] bool useDefault;
        [SerializeField] string defaultUrl;
        [SerializeField] string closeSceneName;
        [Header("Visuals")]
        [SerializeField] float loaderTime;
        CCase ccase;
        public void Close()
        {
            AppManager.Instance.ChangeScene(closeSceneName);
        }
        void Start()
        {
            AppManager.Instance.ShowLoader(true);
            string url = GetUrl();
            StartCoroutine(Downloader.Instance.LoadJSON(url, OnMetadataLoaded));
        }
        string GetUrl()
        {
            if (useDefault)
                return defaultUrl;
            string url = AppManager.Instance.GetComponent<AppData>().GetStringValue("url");            
            if(string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
                return defaultUrl;
            return url;
        }
        void OnMetadataLoaded(string json)
        {
            ccase = JsonUtility.FromJson<CCase>(json);            
            PresentationController.Instance.Set(ccase.info);
            ChatController.Instance.Set(ccase.chat);            
            QuizController.Instance.Set(ccase.quiz);
            StartCoroutine(WaitLoader());
        }
        IEnumerator WaitLoader()
        {
            yield return new WaitForSeconds(loaderTime);
            AppManager.Instance.ShowLoader(false);
        }
    }
}
