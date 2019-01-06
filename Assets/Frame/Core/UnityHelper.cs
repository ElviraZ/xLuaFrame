using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;


/***
 * 
 * 热更新框架
 * 
 * 
 * 
 */
namespace ElviraFrame.HotUpdateFrame
{


public  static class UnityHelper 
{
        /// <summary>
        /// 根据 提供的需要进行校验的文件的路径，生成MD5编码
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileMD5(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            filePath = filePath.Trim();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                byte[] result = md5.ComputeHash(fs);
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("x2"));//x2 表示按照十六进制输出，且两位对齐
                }
            }
            return sb.ToString();
        }

    }

}