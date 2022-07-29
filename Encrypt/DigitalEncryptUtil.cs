/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library
*文件名： DigitalEncryptUtil
*版本号： V1.0.0.0
*唯一标识：39bbf23f-1db1-48e1-a631-5d4a0521f8e4
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 13:50:49
*描述：
*
*=================================================
*修改标记
*修改时间：2022/7/29 13:50:49
*修改人： yswen
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypt.Library
{
    /// <summary>
    /// 数字加密工具类
    /// </summary>
    public static class DigitalEncryptUtil
    {
        static readonly byte[] IntDefault = new byte[4] { 230, 154, 0, 0 };

        static readonly byte[] LongDefault = new byte[8] { 230, 154, 0, 0, 0, 0, 0, 0 };

        static readonly decimal DecimalDefault = 39654;

        const string KEY = "RGV2ZWxvcGVkIGJ5IHlzd2VubGkyMDIy";

        /// <summary>
        /// 将整数转换成字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FromInt(int num)
        {
            var bytes = BitConverter.GetBytes(num);
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] ^= IntDefault[i];
            }
            return AESUtil.Encrypt(bytes, KEY).ToHexString();
        }
        /// <summary>
        /// 将整数转换成字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FromLong(long num)
        {
            var bytes = BitConverter.GetBytes(num);
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] ^= LongDefault[i];
            }
            return AESUtil.Encrypt(bytes, KEY).ToHexString();
        }
        /// <summary>
        /// 将decimal转换成字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FromDecimal(decimal num)
        {
            var bytes1 = decimal.GetBits(num);
            var bytes2 = decimal.GetBits(DecimalDefault);
            List<byte> result = new List<byte>();
            for (int i = 0; i < bytes1.Length; i++)
            {
                bytes1[i] ^= bytes2[i];
                result.AddRange(BitConverter.GetBytes(bytes1[i]));
            }
            return AESUtil.Encrypt(result.ToArray(), KEY).ToHexString();
        }

        /// <summary>
        /// 将密文转换成整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(string str)
        {
            var bytes = str.ToBytes();
            var source = AESUtil.Decrypt(bytes, KEY);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] ^= IntDefault[i];
            }
            return BitConverter.ToInt32(source);
        }
        /// <summary>
        /// 将密文转换成整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(string str)
        {
            var bytes = str.ToBytes();
            var source = AESUtil.Decrypt(bytes, KEY);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] ^= LongDefault[i];
            }
            return BitConverter.ToInt64(source);
        }

        /// <summary>
        /// 将密文转换成decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string str)
        {
            var bytes1 = str.ToBytes();            
            var source = AESUtil.Decrypt(bytes1, KEY);
            List<int> ints = new List<int>();
            for (int i = 0; i < source.Length/4; i++)
            {
                ints.Add(BitConverter.ToInt32(source.AsSpan().Slice(i * 4, 4)));
            }
            var bytes2 = decimal.GetBits(DecimalDefault);
            for (int i = 0; i < ints.Count; i++)
            {
                ints[i]^= bytes2[i];
            }
            return new decimal(ints.ToArray());
        }
    }
}
