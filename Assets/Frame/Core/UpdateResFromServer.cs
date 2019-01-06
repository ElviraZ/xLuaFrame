using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


/***
 * 
 * 热更新框架
 * 
 * 功能：从服务器下载最新的资源（AB包，Lua文件，配置文件等）
 * 
 * 
 * 内容：
 * 1、下载校验文件，
 * 2、在客户端进行MD5比较
 * 3、如果客户端存在服务器上增加的，直接下载
 * 4、如果存在相同的，但MD5不同，亦下载
 * 
 * 
 * 
 * 
 */
namespace ElviraFrame.HotUpdateFrame
{


    public class UpdateResFromServer : MonoBehaviour
    {





        #region    核心变量定义

        ///是否启用本脚本(是否联网下载服务器资源)
        public bool enableSelf = true;

        ///PC平台的ab下载路径
        private string downLoadPath = string.Empty;
        ///Http服务器路径
        private string ServerURL = "http://127.0.01:8080/UpdateAssets";

        #endregion

        private void Awake()
        {
            if (enableSelf)
            {//PC平台本地的保存路径
                downLoadPath = Application.streamingAssetsPath;


                StartCoroutine(DownloadAndCheck());
            }
            else
            {
                //不启用更新
                //通知其他游戏主逻辑运行



                ActionCenter.ActionCenter.Broadcast(ActionCenter.ActionType.GameStart);
            }
        }
        IEnumerator DownloadAndCheck()
        {



            #region 1、下载校验文件
            if (string.IsNullOrEmpty(ServerURL))
            {
                Debug.LogError(GetType() + "/服务器地址不正确==ServerURL===" + ServerURL);
                yield break;
            }
            string fileURL = ServerURL + "/ProjectVerifyFile.txt";
            UnityWebRequest www = new UnityWebRequest(fileURL);
            www.SendWebRequest();
            DownloadHandler downloadHandler = (DownloadHandler)www.downloadHandler;
            yield return www;
            if (www.error == null || www.isDone == false)
            {
                Debug.LogError(GetType() + "/下载错误");
                yield break;
            }


            if (string.IsNullOrEmpty(downLoadPath))
            {
                Debug.LogError(GetType() + "/下载本地保存路径错误");
                yield break;
            }


            if (!Directory.Exists(downLoadPath))
            {
                Directory.CreateDirectory(downLoadPath);
            }


            File.WriteAllBytes(downLoadPath + "/ProjectVerifyFile.txt", www.downloadHandler.data);
            #endregion

            #region   2、读取内容在客户端进行MD5比较
            string serverFileTxt = www.downloadHandler.text;
            string[] lines = serverFileTxt.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {//略过空行
                    continue;
                }
                //得到每行的文件名与MD5编码
                string[] fileinfo = lines[i].Split('|');
                string serverfileName = fileinfo[0].Trim();
                string serverfileMD5 = fileinfo[1].Trim();
                //得到本地对应名字的文件
                string localFile = downLoadPath + "/" + serverfileName;
                #region   3、如果客户端没有服务器上的，直接下载
                if (File.Exists(localFile))
                {
                    Debug.Log(GetType() + "/localFile不存在，直接下载===" + localFile);
                    string dir = Path.GetDirectoryName(localFile);
                    if (Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    yield return StartCoroutine(DownLoad(ServerURL + "/" + serverfileName, localFile));

                }


                #endregion

                #region   4、如果存在相同的，但MD5不同，亦下载

                else
                {

                    //根据客户端本地文件名字，得到文件的MD5                
                    //服务器的MD5与本地生成的MD5进行比较
                    //如果不一致删除本地文件并下载
                    string localMD5 = UnityHelper.GetFileMD5(localFile);
                    if (!localMD5.Equals(serverfileMD5))
                    {
                        File.Delete(localFile);
                        Debug.Log(GetType() + "/localFile不一致，直接下载===" + localFile);
                        yield return StartCoroutine(DownLoad(ServerURL + "/" + serverfileName, localFile));
                    }
                }
                #endregion

            }


            #endregion

            yield return new WaitForEndOfFrame();


            Debug.Log("DownloadAndCheck()更新完成");


            //通知其他游戏主逻辑运行
            ActionCenter.ActionCenter.Broadcast(ActionCenter.ActionType.GameStart);


        }



        /// <summary>
        /// 下载更新的资源
        /// </summary>
        /// <param name="wwwPath"></param>
        /// <param name="localSavePath"></param>
        /// <returns></returns>

        IEnumerator DownLoad(string wwwPath, string localSavePath)
        {
            UnityWebRequest www = new UnityWebRequest(wwwPath);
            www.SendWebRequest();
            DownloadHandler downloadHandler = (DownloadHandler)www.downloadHandler;
            yield return www;
            if (www.isDone && www.error == null)
            {
                File.WriteAllBytes(localSavePath, www.downloadHandler.data);
            }
            else
            {
                Debug.LogError("网络下载更新资源失败====" + wwwPath + "||" + www.error);
            }

        }
    }

}