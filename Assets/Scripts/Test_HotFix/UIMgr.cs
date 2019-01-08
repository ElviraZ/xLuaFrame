/***
 *
 *  Title: "热更新流程设计"课程项目
 *        
 *         开发一个“年会倒计时”UI界面控制
 *
 *  Description:
 *        功能：[本脚本的主要功能描述] 
 *
 *  Date: 2018
 * 
 *  Version: 1.0
 *
 *  Modify Recorder:
 *     
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using UnityEngine.UI;


namespace HotUpdateModel
{
    [Hotfix]
    public class UIMgr:MonoBehaviour
    {
        public Text TxtNumber;              //显示数字控件
        private int _CountDownNum = 0;      //倒计时数字


        private void Start()
        {
            _CountDownNum = 0;
        }

        private void Update()
        {
            if (Time.frameCount%60==0)
            {
                ++_CountDownNum;
                TxtNumber.text = _CountDownNum.ToString();
            }
        }


    }//Class_end
}


