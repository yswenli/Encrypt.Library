/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt
*文件名： SHAUtil
*版本号： V1.0.0.0
*唯一标识：d8238506-7be6-4a20-9e12-ad7136c73559
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 11:18:10
*描述：SHAUtil
*
*=================================================
*修改标记
*修改时间：2022/7/29 11:18:10
*修改人： yswen
*版本号： V1.0.0.0
*描述：SHAUtil
*
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using Encrypt.Library.Core;

namespace Encrypt.Library
{
    /// <summary>
    /// SHAUtil
    /// </summary>
    public static class SHAUtil
    {
        /// <summary>
        /// sha1
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSHA1(string data, string key = null)
        {
            return string.IsNullOrEmpty(key) ? EncryptProvider.Sha1(data) : EncryptProvider.HMACSHA1(data, key);
        }
        /// <summary>
        /// sha256
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSHA256(string data, string key = null)
        {
            return string.IsNullOrEmpty(key) ? EncryptProvider.Sha256(data) : EncryptProvider.HMACSHA256(data, key);
        }
        /// <summary>
        /// sha384
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSHA384(string data, string key = null)
        {
            return string.IsNullOrEmpty(key) ? EncryptProvider.Sha384(data) : EncryptProvider.HMACSHA384(data, key);
        }
        /// <summary>
        /// sha512
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSHA512(string data, string key = null)
        {
            return string.IsNullOrEmpty(key) ? EncryptProvider.Sha512(data) : EncryptProvider.HMACSHA512(data, key);
        }

        /// <summary>
        /// 获取企业微信消息签名
        /// </summary>
        /// <param name="token">票据</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="encrypt">密文</param>
        /// <returns>SHA1签名</returns>
        public static string GetSHA1ForWeChat(string token, string timestamp, string nonce, string encrypt)
        {
            return EncryptProvider.Sha1ForWeChat(token, timestamp, nonce, encrypt);
        }
    }
}
