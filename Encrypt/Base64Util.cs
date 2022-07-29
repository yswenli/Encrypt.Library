/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt
*文件名： Base64Util
*版本号： V1.0.0.0
*唯一标识：f9221ea6-a9c7-4289-829a-6b2bff190a6f
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 11:30:29
*描述：
*
*=================================================
*修改标记
*修改时间：2022/7/29 11:30:29
*修改人： yswen
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
using System;
using System.Text;

namespace Encrypt.Library
{
    /// <summary>
    /// base64快捷工具类
    /// </summary>
    public static class Base64Util
    {
        /// <summary>
        /// 将一般文本转换成base64 文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertToBase64Str(this string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// 将 base64 文本转换成一般文本
        /// </summary>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public static string ConvertToUTF8Str(this string base64Str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64Str));
        }
    }
}
