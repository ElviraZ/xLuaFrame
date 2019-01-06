using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;
using ElviraFrame.HotUpdateFrame;

/***
* 
* 热更新框架
* 功能：创建项目的校验文件
* 内容：
* 1、针对AB包、各种资源文件生成MD5校验文件
* 2、Unity编辑器运行，前提是先生成AB包以及资源文件
* 3、此脚本在Editor目录下
* 
*/
namespace Elvira.HotUpdateFrame
{
    /// <summary>
    /// 对指定文件夹生成校验文件 
    /// </summary>
    public class CreateVerifyFiles
    {
        /*定义局部变量*/
        //    string abOutPath = string.Empty;//待修改
        static string abOutPath = Application.streamingAssetsPath + "/Windows";
        [MenuItem("Elvira/Tools/生成校验文件")]
        public static void CreateFileMethod()
        {

            List<string> fileLists = new List<string>();                 // 存储所有的合法文件的相对路径信息
            /*定义校验文件的输出路径*/
            string verifyPath = abOutPath + "/ProjectVerifyFile.txt";

            /*如有检验文件则覆盖*/
            if (File.Exists(verifyPath))
            {
                File.Delete(verifyPath);
            }
            /*遍历(检验文件夹)中的所有文件，生成MD5*/
            ListFiles(new DirectoryInfo(abOutPath), ref fileLists);


            /* 检测需要进行校验的文件路径*/
            foreach (var item in fileLists)
            {
                Debug.Log("文件路径====" + item);
            }
            /*把文件的名字与对应的MD5写入校验文件*/
            WriteVerifyFile(verifyPath, fileLists);
        }
        /// <summary>
        /// 遍历(检验文件夹)中的所有文件，得到所有的合法文件
        /// </summary>
        /// <param name="fileSystemInfo">文件（夹）路径信息</param>
        /// <param name="fileList">得到所有的合法文件并写入集合</param>
        private static void ListFiles(FileSystemInfo fileSystemInfo, ref List<string> fileList)
        {
            //文件系统转为目录系统
            DirectoryInfo dirInfo = fileSystemInfo as DirectoryInfo;
            //获取文件下的所有文件信息（包括文件与文件夹）
            FileSystemInfo[] fleSystemInfos = dirInfo.GetFileSystemInfos();
            foreach (FileSystemInfo item in fleSystemInfos)
            {//假设是一个文件
                FileInfo fileInfo = item as FileInfo;
                if (fileInfo != null)
                {
                    //路径转换为Unity文件路径
                    string strFileFullName = fileInfo.FullName.Replace("\\", "/");
                    //过滤无效文件
                    string fileExt = Path.GetExtension(strFileFullName);
                    if (fileExt.EndsWith(".bat") || fileExt.EndsWith(".meta"))
                    {
                        continue;
                    }
                    else
                    {
                        fileList.Add(strFileFullName);
                    }
                }
                else
                {
                    //则是文件夹
                    ListFiles(item, ref fileList);
                }
            }

        }




        /// <summary>
        /// 把文件的名字与对应的MD5写入校验文件
        /// </summary>
        /// <param name="verifyFileOutPath">校验文件的输出路径</param>
        /// <param name="fileList">得到的需要进行校验的文件路径</param>
        private static void WriteVerifyFile(string verifyFileOutPath, List<string> fileList)
        {

            using (FileStream fs = new FileStream(verifyFileOutPath, FileMode.CreateNew))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {

                    for (int i = 0; i < fileList.Count; i++)
                    {
                        //获取文件的完整路径
                        string filePath = fileList[i];
                        //得到对应的MD5
                        string strFileMD5 = UnityHelper.GetFileMD5(filePath);

                        //得到文件的名字和后缀
                        string fileTrueName = filePath.Replace(abOutPath + "/", string.Empty);

                        //参数检查


                        //写入文件
                        sw.WriteLine(fileTrueName + "|" + strFileMD5);

                    }
                }

            }
            Debug.Log("检验文件写入完成===" + verifyFileOutPath);

        }

    }
}
