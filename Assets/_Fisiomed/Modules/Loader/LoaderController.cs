using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderController : Singleton<LoaderController>
{
    public bool iAmFirst;

    void Awake()
    {
       DontDestroyOnLoad(Instance);

       LoaderController[] controllers = FindObjectsOfType(typeof(LoaderController)) as LoaderController[];

       if(controllers.Length > 1)
       {
           for(int i = 0; i < controllers.Length; i++)
           {
               if(!controllers[i].iAmFirst)
               {
                   DestroyImmediate(controllers[i].gameObject);
               }
           }
       }
       else
       {
           iAmFirst = true;
       }
    }
}
