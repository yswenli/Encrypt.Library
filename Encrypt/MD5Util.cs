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
using System.IO;

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
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetMD5(this byte[] data, MD5Length length = MD5Length.L32)
        {
            return EncryptProvider.Md5(data, length);
        }

        /// <summary>
        /// md5
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetMD5Str(this string data, MD5Length length = MD5Length.L32)
        {
            return EncryptProvider.Md5(data);
        }
        /// <summary>
        /// md5
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetMD5Str(this byte[] data, MD5Length length = MD5Length.L32)
        {
            return EncryptProvider.GetMd5Str(data, length);
        }

        /// <summary>
        /// md5
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetMD5Str(this Stream stream, MD5Length length = MD5Length.L32)
        {
            return EncryptProvider.GetMd5ForStream(stream, length);
        }

        /// <summary>
        /// md5
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetMD5StrForFile(this string filePath, MD5Length length = MD5Length.L32)
        {
            return EncryptProvider.GetMd5ForFile(filePath, length);
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
