using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase.Auth;

namespace Fisiomed.User
{
    using FirebaseServices;
    public class AuthSceneController : MonoBehaviour
    {
        public UnityEvent OnFirebaseInitialized = new UnityEvent();
        public FirebaseUserEvent OnFirebaseUserSignedIn = new FirebaseUserEvent();
        public UnityEvent OnFirebaseUserSignedOut = new UnityEvent();
        public UnityEvent OnFirebaseUserNull = new UnityEvent();

        void Start()
        {            
            try
            {
                FirebaseManager.Instance.OnFirebaseInitialized.AddListener(OnFirebaseInitListener);
                FirebaseManager.Instance.OnFirebaseUserSignedIn.AddListener(OnFirebaseUserSignedInListener);
                FirebaseManager.Instance.OnFirebaseUserSignedOut.AddListener(OnFirebaseUserSignedOutListener);
                FirebaseManager.Instance.OnFirebaseUserNull.AddListener(OnFirebaseUserNullListener);

                FirebaseManager.Instance.AuthStateChanged(this, null);
            }
            catch
            {

            }
        }
        void OnDestroy()
        {
            try
            {
                FirebaseManager.Instance.OnFirebaseInitialized.RemoveListener(OnFirebaseInitListener);
                FirebaseManager.Instance.OnFirebaseUserSignedIn.RemoveListener(OnFirebaseUserSignedInListener);
                FirebaseManager.Instance.OnFirebaseUserSignedOut.RemoveListener(OnFirebaseUserSignedOutListener);
                FirebaseManager.Instance.OnFirebaseUserNull.RemoveListener(OnFirebaseUserNullListener);
            }
            catch
            {

            }            
        }
        private void OnFirebaseInitListener()
        {
            OnFirebaseInitialized.Invoke();
        }
        private void OnFirebaseUserSignedInListener(FirebaseUser user)
        {
            OnFirebaseUserSignedIn.Invoke(user);
        }
        private void OnFirebaseUserSignedOutListener()
        {
            OnFirebaseUserSignedOut.Invoke();
        }
        private void OnFirebaseUserNullListener()
        {
            OnFirebaseUserNull.Invoke();
        }
    }
}
