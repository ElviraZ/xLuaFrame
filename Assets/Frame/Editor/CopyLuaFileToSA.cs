using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/***
 * 
 * 热更新框架
 * 拷贝所有Lua编辑器区的文件到发布区
 * 
 * 
 */
namespace Elvira.HotUpdateFrame
{


public class CopyLuaFileToSA
    {

        /*定义lua编辑器区的目录*/

        private  static string luaDirPath = Application.dataPath + "/LuaScripts/src";
        /*定义目标目录(Lua发布区)*/
        private static string targetDir = Application.streamingAssetsPath + "/LuaScripts";

        [MenuItem("Elvira/Tools/拷贝所有Lua编辑器区的文件到发布区")]
        public static void CopyLuaFile()
        {
            /*参数检查*/
            if (string.IsNullOrEmpty(luaDirPath)||string.IsNullOrEmpty(targetDir))
            {
                return;

            }
            /*定义文件与目录结构*/
            DirectoryInfo dirInfo = new DirectoryInfo(luaDirPath);
            FileInfo[] files = dirInfo.GetFiles();
            /*创建拷贝的目录，无则新建*/
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
            /*循环拷贝*/
            foreach (var item in files)
            {
                File.Copy(item.FullName, targetDir+"/"+item.Name,true);
            }
            /*编辑器刷新*/

#if Unity_Editor
                     AssetDatabase.Refresh();
#endif

        }
    }

}