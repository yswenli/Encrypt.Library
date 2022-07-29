/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt
*文件名： AESUtil
*版本号： V1.0.0.0
*唯一标识：35d3e803-e499-496a-81b4-c3db27360322
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 10:03:27
*描述：AESUtil
*
*=================================================
*修改标记
*修改时间：2022/7/29 10:03:27
*修改人： yswen
*版本号： V1.0.0.0
*描述：AESUtil
*
*****************************************************************************/
using Encrypt.Library.Core;
using Encrypt.Library.Core.Extensions;
using Encrypt.Library.Core.Internal;

namespace Encrypt.Library
{
    /// <summary>
    /// aes，
    /// AES: 16-bit key=128 bits, 24-bit key=192 bits, 32-bit key=256 bits, IV is 16 bits
    /// </summary>
    public static class AESUtil
    {
        /// <summary>
        /// aes key，
        /// AES: 16-bit key=128 bits, 24-bit key=192 bits, 32-bit key=256 bits, IV is 16 bits
        /// </summary>
        public static AESKey Key
        {
            get; private set;
        }

        /// <summary>
        /// aes，
        /// AES: 16-bit key=128 bits, 24-bit key=192 bits, 32-bit key=256 bits, IV is 16 bits
        /// </summary>
        static AESUtil()
        {
            Key = EncryptProvider.CreateAesKey();
        }

        /// <summary>
        /// AES encrypt without iv (ECB mode)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypt(string data)
        {
            return EncryptProvider.AESEncrypt(data, Key.Key);
        }
        /// <summary>
        /// AES decrypt without iv (ECB mode)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Decrypt(string data)
        {
            return EncryptProvider.AESDecrypt(data, Key.Key);
        }
        /// <summary>
        /// AES encrypt without iv (ECB mode)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string key)
        {
            return EncryptProvider.AESEncrypt(data, key.GetKey());
        }

        /// <summary>
        /// AES decrypt without iv (ECB mode)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] data, string key)
        {
            return EncryptProvider.AESDecrypt(data, key.GetKey());
        }
        /// <summary>
        /// AES encrypt without iv (ECB mode)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string data, string key)
        {
            return EncryptProvider.AESEncrypt(data, key.GetKey());
        }

        /// <summary>
        /// AES decrypt without iv (ECB mode)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string data, string key)
        {
            return EncryptProvider.AESDecrypt(data, key.GetKey());
        }

        /// <summary>
        /// AES encrypt bytes with iv (CBC mode)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string key, string vector)
        {
            return EncryptProvider.AESEncrypt(data, key.GetKey(), vector.GetIV());
        }
        /// <summary>
        /// AES decrypt bytes with iv (CBC mode)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] data, string key, string vector)
        {
            return EncryptProvider.AESDecrypt(data, key.GetKey(), vector.GetIV());
        }

        /// <summary>
        /// AES encrypt bytes with iv (CBC mode)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, string vector)
        {
            return EncryptProvider.AESEncrypt(data, key.GetKey(), vector.GetIV());
        }
        /// <summary>
        /// AES decrypt bytes with iv (CBC mode)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static string Decrypt(string data, string key, string vector)
        {
            return EncryptProvider.AESDecrypt(data, key.GetKey(), vector.GetIV());
        }
    }
}
