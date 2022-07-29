/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt
*文件名： MDUtil
*版本号： V1.0.0.0
*唯一标识：3a0301b3-d2fc-4c4d-a8ca-283c4e94db2e
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 11:08:53
*描述：MDUtil
*
*=================================================
*修改标记
*修改时间：2022/7/29 11:08:53
*修改人： yswen
*版本号： V1.0.0.0
*描述：MDUtil
*
*****************************************************************************/
using System;
using System.IO;
using System.Security.Cryptography;

using Encrypt.Library.Core;

namespace Encrypt.Library
{
    /// <summary>
    /// MDUtil
    /// </summary>
    public static class MD5Util
    {
        /// <summary>
        /// md5
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetMD5(this byte[] data)
        {
            return EncryptProvider.Md5(data);
        }

        /// <summary>
        /// md5
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetMD5Str(this string data)
        {
            return EncryptProvider.Md5(data);
        }

        /// <summary>
        /// md5
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetMD5Str(this byte[] data)
        {
            return EncryptProvider.GetMd5Str(data);
        }

        /// <summary>
        /// 高性能计算文件的md5
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetMD5ForFile(this string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException(string.Format("<{0}>, 不存在", filePath));
            int bufferSize = 1024 * 16;
            byte[] buffer = new byte[bufferSize];
            Stream inputStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
            int readLength = 0;
            var output = new byte[bufferSize];
            while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
            }
            hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
            string md5 = BitConverter.ToString(hashAlgorithm.Hash);
            hashAlgorithm.Clear();
            inputStream.Close();
            md5 = md5.Replace("-", "");
            return md5;
        }

        /// <summary>
        /// GetHMACMD5
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHMACMD5(this string data, string key)
        {
            return EncryptProvider.HMACMD5(data, key);
        }
        /// <summary>
        /// GetHMACMD5
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHMACMD5(this byte[] data, string key)
        {
            return EncryptProvider.HMACMD5(data, key);
        }
    }
}
