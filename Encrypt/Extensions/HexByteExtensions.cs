/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt
*文件名： HexByteExtensions
*版本号： V1.0.0.0
*唯一标识：19a99a0e-466a-477f-9cfe-ed7b98f6b3dc
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 11:32:16
*描述：二进制、字节转换
*
*=================================================
*修改标记
*修改时间：2022/7/29 11:32:16
*修改人： yswen
*版本号： V1.0.0.0
*描述：二进制、字节转换
*
*****************************************************************************/
using System.Globalization;
using System.Text;

namespace Encrypt.Library.Extensions
{
    /// <summary>
    /// 二进制、字节转换
    /// </summary>
    public static class HexByteExtensions
    {
        /// <summary>
        /// byte to hex string extension
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// hex string to byte extension
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }
            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }
            byte[] result = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), NumberStyles.AllowHexSpecifier);
            }
            return result;
        }
    }
}
