using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ElviraFrame.ActionCenter;
/***
 * 
 * 热更新框架
 * 
 * 功能：开始游戏
 * 
 */
namespace ElviraFrame.HotUpdateFrame
{


public class StartGame : MonoBehaviour, IStartGame
    {


        private void Awake()
        {
            ActionCenter.ActionCenter.AddListener(ActionType.GameStart, StartRunning);
        }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void StartRunning()
        {
            Debug.Log("热更新模块执行完毕，开始游戏主逻辑");

        }
    }

}