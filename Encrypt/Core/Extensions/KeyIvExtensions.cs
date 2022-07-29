/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library.Core.Extensions
*文件名： KeyIvExtensions
*版本号： V1.0.0.0
*唯一标识：d7dc7a60-fb9d-41dc-8dcb-195dac70b234
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 14:53:24
*描述：
*
*=================================================
*修改标记
*修改时间：2022/7/29 14:53:24
*修改人： yswen
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypt.Library.Core.Extensions
{
    /// <summary>
    /// key iv扩展
    /// </summary>
    public static class KeyIvExtensions
    {
        //32
        public const string DEFAULTKEY = "RGV2ZWxvcGVkIGJ5IHlzd2VubGkyMDIy";
        //16
        public const string DEFAULTIV = "eXN3ZW5saTIwMjI=";

        /// <summary>
        /// GetKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKey(this string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return DEFAULTKEY;
            }
            if (key.Length < DEFAULTKEY.Length)
            {
                return $"{key}{DEFAULTKEY.Substring(0, DEFAULTKEY.Length - key.Length)}";
            }
            else if (key.Length > DEFAULTKEY.Length)
            {
                return key.Substring(0, DEFAULTKEY.Length);
            }
            else
            {
                return key;
            }
        }

        /// <summary>
        /// get iv
        /// </summary>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string GetIV(this string iv)
        {
            if (string.IsNullOrEmpty(iv))
            {
                return DEFAULTIV;
            }
            if (iv.Length < DEFAULTIV.Length)
            {
                return $"{iv}{DEFAULTIV.Substring(0, DEFAULTIV.Length - iv.Length)}";
            }
            else if (iv.Length > DEFAULTIV.Length)
            {
                return iv.Substring(0, DEFAULTIV.Length);
            }
            else
            {
                return iv;
            }
        }
    }
}
