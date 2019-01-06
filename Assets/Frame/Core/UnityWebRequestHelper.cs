using UnityEngine;
using System.Collections;
namespace ElviraFrame.Helper
{
    public class UnityWebRequestHelper : MonoBehaviour
    {
        static UnityWebRequestHelper instance = null;
        static GameObject go;
        public static UnityWebRequestHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    go = new GameObject();
                    go.name = "ScreenshotManager";
                    instance = go.AddComponent<UnityWebRequestHelper>();
                }

                return instance;
            }
        }






    }
}

