/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt
*文件名： DESUtil
*版本号： V1.0.0.0
*唯一标识：bbd5793e-f173-48a2-a9e6-7a5fce4af47d
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 10:11:37
*描述：DESUtil
*
*=================================================
*修改标记
*修改时间：2022/7/29 10:11:37
*修改人： yswen
*版本号： V1.0.0.0
*描述：DESUtil
*
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using Encrypt.Library.Core;

namespace Encrypt.Library
{
    /// <summary>
    /// DESUtil
    /// </summary>
    public static class DESUtil
    {
        /// <summary>
        /// Key,des key length is 24 bit
        /// </summary>
        public static string Key { get; private set; }
        /// <summary>
        /// iv,des iv length is 8 bit
        /// </summary>
        public static string IV { get; private set; }

        /// <summary>
        /// DES
        /// </summary>
        static DESUtil()
        {
            Key = EncryptProvider.CreateDesKey();
            IV = EncryptProvider.CreateDesIv();
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string key, string vector)
        {
            return EncryptProvider.DESEncrypt(data, key, vector);
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] data, string key, string vector)
        {
            return EncryptProvider.DESDecrypt(data, key, vector);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, string vector)
        {
            return Encoding.UTF8.GetString(Encrypt(Encoding.UTF8.GetBytes(data), key, vector));
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static string Decrypt(string data, string key, string vector)
        {
            return Encoding.UTF8.GetString(Decrypt(Encoding.UTF8.GetBytes(data), key, vector));
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string key)
        {
            return EncryptProvider.DESEncrypt(data, key);
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] data, string key)
        {
            return EncryptProvider.DESDecrypt(data, key);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string data, string key)
        {
            return Encoding.UTF8.GetString(Encrypt(Encoding.UTF8.GetBytes(data), key));
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string data, string key)
        {
            return Encoding.UTF8.GetString(Decrypt(Encoding.UTF8.GetBytes(data), key));
        }
    }
}
