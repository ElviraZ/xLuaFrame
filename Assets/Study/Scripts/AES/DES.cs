using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;






namespace Elvira.Helper
{

    /// <summary>
    /// DES加密解密
    /// </summary>
    public class DES
    {

        //密钥
        private static readonly string EncryptKey = "12345678";


        #region 加密


        #region 加密字符串
        /// <summary>
        /// DES加密
        /// 对字符串进行加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string AESEncrypt(string encryptString)
        {

            DESCryptoServiceProvider des            = new DESCryptoServiceProvider();
            byte[]                   inputByteArray = Encoding.Default.GetBytes(encryptString);
            des.Key = ASCIIEncoding.ASCII.GetBytes(EncryptKey); // 密匙
            des.IV  = ASCIIEncoding.ASCII.GetBytes(EncryptKey); // 初始化向量
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var retB = Convert.ToBase64String(ms.ToArray());
            return retB;
       
        }

        #endregion

        #region 加密字节数组
        public static byte[] AESEncrypt(byte[] EncryptByte)
        {
            if (EncryptByte.Length==0)
            {
                Debug.LogError("明文不得为空");
                return null;
            }
      

            byte[] m_strEncrypt;
            byte[]   m_btIV        = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            byte[]   m_salt        = Convert.FromBase64String("gsf4jvkyhye5/d7k8OrLgM==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                MemoryStream        m_stream   = new MemoryStream();
                PasswordDeriveBytes pdb        = new PasswordDeriveBytes(EncryptKey, m_salt);
                ICryptoTransform    transform  = m_AESProvider.CreateEncryptor(pdb.GetBytes(32), m_btIV);
                CryptoStream        m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                m_csstream.Write(EncryptByte, 0, EncryptByte.Length);
                m_csstream.FlushFinalBlock();
                m_strEncrypt = m_stream.ToArray();
                m_stream.Close(); m_stream.Dispose();
                m_csstream.Close(); m_csstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_AESProvider.Clear(); }
            return m_strEncrypt;
        }


        #endregion

        #endregion



        #region 解密
        #region 解密字符串
        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        public static string AESDecrypt(string DecryptString)
        {
            DESCryptoServiceProvider des            = new DESCryptoServiceProvider();
            byte[]                   inputByteArray = Convert.FromBase64String(DecryptString);
            des.Key = ASCIIEncoding.ASCII.GetBytes(EncryptKey);
            des.IV  = ASCIIEncoding.ASCII.GetBytes(EncryptKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            // 如果两次密匙不一样，这一步可能会引发异常
            cs.FlushFinalBlock();
        
            System.Text.Encoding.UTF8.GetBytes(    ms.ToString());
          
            return System.Text.Encoding.Default.GetString(ms.ToArray());
 
        }


        public static byte[] AESDecryptBytes(string DecryptString)
        {
            DESCryptoServiceProvider des            = new DESCryptoServiceProvider();
            byte[]                   inputByteArray = Convert.FromBase64String(DecryptString);
            des.Key = ASCIIEncoding.ASCII.GetBytes(EncryptKey);
            des.IV  = ASCIIEncoding.ASCII.GetBytes(EncryptKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            // 如果两次密匙不一样，这一步可能会引发异常
            cs.FlushFinalBlock();



            return System.Text.Encoding.UTF8.GetBytes(ms.ToString());

        }
        #endregion


        #endregion

    }
}
