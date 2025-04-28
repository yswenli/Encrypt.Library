/****************************************************************************
*Copyright (c) YSWenli All Rights Reserved.
*CLR版本： .net8.0
*机器名称：WALLE
*Author：yswenli
*命名空间：Encrypt.Library.Extensions
*文件名： KeyLengthExtentions
*版本号： V1.0.0.0
*唯一标识：81b0fa75-948e-4fcc-b763-2280bd2df0b0
*当前的用户域：WALLE
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2025/4/28 10:39:00
*描述：
*
*=================================================
*修改标记
*修改时间：2025/4/28 10:39:00
*修改人： yswenli
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
using System;
using System.Linq;
using System.Text;

using Encrypt.Library.Core.Models;

namespace Encrypt.Library.Extensions
{
    /// <summary>
    /// 密码长度扩展类
    /// </summary>
    internal static class KeyExtentions
    {
        static readonly int[] _keyLength;
        static readonly int[] _keyBitLength;

        /// <summary>
        /// 密码长度扩展类
        /// </summary>
        static KeyExtentions()
        {
            _keyLength = new int[] { 16, 24, 32 };
            _keyBitLength = new int[] { 128, 192, 256 };
        }

        /// <summary>
        /// 验证密码长度
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ValideKey(this string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            var keyLength = key.Length;
            foreach (var len in _keyLength)
            {
                if (keyLength == len) return;
            }
            throw new ArgumentOutOfRangeException("The password length must be 16, 24, or 32.");
        }

        /// <summary>
        /// 验证密码长度
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ValideKey(this byte[] key)
        {
            if (key == null || key.Length == 0)
            {
                throw new ArgumentNullException(nameof(key));
            }
            var keyLength = key.Length;
            foreach (var len in _keyBitLength)
            {
                if (keyLength * 8 == len) return;
            }
            throw new ArgumentOutOfRangeException("The password length must be 16, 24, or 32.");
        }

        /// <summary>
        /// 获取随机密码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomKey(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%_";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        /// <summary>
        /// 生成密码
        /// </summary>
        /// <param name="enumKeyConst"></param>
        /// <returns></returns>
        public static void GenerateKey(EnumKeyConst enumKeyConst, out string passwords, out byte[] pwdBytes)
        {
            passwords = null;
            pwdBytes = null;
            //超过128的密码长度为bit长度
            if (enumKeyConst <= EnumKeyConst.Len64)
            {
                // 生成指定长度的字符串密码
                var length = (int)enumKeyConst;
                passwords = GenerateRandomKey(length);
                pwdBytes = Encoding.UTF8.GetBytes(passwords);
            }
            else
            {
                // 生成指定位数的byte[]密码
                var bitLength = (int)enumKeyConst;
                var byteLength = bitLength / 8;
                var key = new byte[byteLength];
                using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
                {
                    rng.GetBytes(key);
                }
                pwdBytes = key;
            }
        }

        /// <summary>
        /// 生成密码
        /// </summary>
        /// <param name="length"></param>
        /// <param name="passwords"></param>
        /// <param name="pwdBytes"></param>
        public static void GenerateKey(int length, out string passwords, out byte[] pwdBytes)
        {
            passwords = null;
            pwdBytes = null;

            // 获取所有EnumKeyConst的值并排序
            var enumValues = Enum.GetValues(typeof(EnumKeyConst)).Cast<int>().OrderBy(v => v).ToArray();

            // 找到大于等于length的最小值
            var closestValue = enumValues.FirstOrDefault(v => v >= length);

            if (length < (int)EnumKeyConst.Len16)
            {
                closestValue = (int)EnumKeyConst.Len16;
            }
            if (length >= (int)EnumKeyConst.Bit2048)
            {
                closestValue = (int)EnumKeyConst.Bit2048;
            }
            // 将找到的值转换为EnumKeyConst
            var enumKeyConst = (EnumKeyConst)closestValue;

            // 调用已有的GenerateKey方法
            GenerateKey(enumKeyConst, out passwords, out pwdBytes);
        }

    }

}
