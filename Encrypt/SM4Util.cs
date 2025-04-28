/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library
*文件名： SM4Util
*版本号： V1.0.0.0
*唯一标识：7c683cdf-6836-4756-a037-46f6bb7a69d6
*当前的用户域：WALLE
*创建人： wenli
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/8/12 13:50:20
*描述：Sm4算法  
*
*=================================================
*修改标记
*修改时间：2022/8/12 13:50:20
*修改人： yswen
*版本号： V1.0.0.0
*描述：Sm4算法  
*
*****************************************************************************/
using System;
using System.Text;

using Encrypt.Library.Core;
using Encrypt.Library.Core.Domestic;
using Encrypt.Library.Extensions;

namespace Encrypt.Library
{
    /// <summary>
    /// Sm4算法  
    /// 对标国际DES算法
    /// </summary>
    public static class SM4Util
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
        /// Sm4算法  
        /// 对标国际DES算法
        /// </summary>
        static SM4Util()
        {
            Key = EncryptProvider.CreateDesKey();
            IV = EncryptProvider.CreateDesIv();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="date"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] key, byte[] iv, byte[] date, Sm4Mode model)
        {
            key.ValideKey();

            Sm4Context ctx = new Sm4Context
            {
                IsPadding = true
            };
            SM4 sm4 = new SM4();
            sm4.SetKeyEnc(ctx, key);
            if (model == Sm4Mode.CBC)
            {
                return sm4.Sm4CryptCbc(ctx, iv, date);
            }
            else
            {
                return sm4.Sm4CryptEcb(ctx, date);
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="date"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] key, byte[] iv, byte[] date)
        {
            key.ValideKey();
            return Encrypt(key, iv, date, Sm4Mode.CBC);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] key, byte[] date)
        {
            key.ValideKey();
            return Encrypt(key, null, date, Sm4Mode.ECB);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="date"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] key, byte[] iv, byte[] date, Sm4Mode model)
        {
            key.ValideKey();
            Sm4Context ctx = new Sm4Context
            {
                IsPadding = true
            };
            SM4 sm4 = new SM4();
            sm4.Sm4SetKeyDec(ctx, key);
            if (model == Sm4Mode.CBC)
            {
                return sm4.Sm4CryptCbc(ctx, iv, date);
            }
            else
            {
                return sm4.Sm4CryptEcb(ctx, date);
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] key, byte[] iv, byte[] date)
        {
            key.ValideKey();
            return Decrypt(key, iv, date, Sm4Mode.CBC);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] key, byte[] date)
        {
            key.ValideKey();
            return Decrypt(key, null, date, Sm4Mode.ECB);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="date"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Encrypt(string key, string iv, string date, Sm4Mode model)
        {
            key.ValideKey();
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(iv),
                Encoding.UTF8.GetBytes(date), model));
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="base64"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Decrypt(string key, string iv, string base64, Sm4Mode model)
        {
            key.ValideKey();
            return Encoding.UTF8.GetString(Decrypt(Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(iv),
                Convert.FromBase64String(base64), model));
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string key, string text)
        {
            key.ValideKey();
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(key), null, Encoding.UTF8.GetBytes(text), Sm4Mode.ECB));
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="encryptText"></param>
        /// <returns></returns>
        public static string Decrypt(string key, string encryptText)
        {
            key.ValideKey();
            var data = Convert.FromBase64String(encryptText);
            return Encoding.UTF8.GetString(Decrypt(Encoding.UTF8.GetBytes(key), null, data, Sm4Mode.ECB));
        }
    }
}
