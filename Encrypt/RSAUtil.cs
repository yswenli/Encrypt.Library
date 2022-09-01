/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt
*文件名： RSAUtil
*版本号： V1.0.0.0
*唯一标识：2eba757f-2f68-401d-b7df-7df48db64470
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 10:24:55
*描述：RSAUtil
*
*=================================================
*修改标记
*修改时间：2022/7/29 10:24:55
*修改人： yswen
*版本号： V1.0.0.0
*描述：RSAUtil
*
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

using Encrypt.Library.Core;
using Encrypt.Library.Core.Internal;

namespace Encrypt.Library
{
    /// <summary>
    /// RSA
    /// </summary>
    public static class RSAUtil
    {
        /// <summary>
        /// Key
        /// </summary>
        public static RSAKey Key { get; private set; }

        /// <summary>
        /// RSA
        /// </summary>
        static RSAUtil()
        {
            Key = EncryptProvider.CreateRsaKey();
        }


        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Sign(string data)
        {
            return EncryptProvider.RSASign(data, Key.PrivateKey);
        }

        /// <summary>
        /// 较验
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signStr"></param>
        /// <returns></returns>
        public static bool Verify(string data, string signStr)
        {
            return EncryptProvider.RSAVerify(data, signStr, Key.PublicKey);
        }
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static byte[] Sign(byte[] data, string privateKey)
        {
            return EncryptProvider.RSASign(data, privateKey);
        }
        /// <summary>
        /// 较验
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signStr"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static bool Verify(byte[] data, string signStr, string publicKey)
        {
            return EncryptProvider.RSAVerify(Encoding.UTF8.GetString(data), signStr, publicKey);
        }
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Sign(string data, string privateKey)
        {
            return EncryptProvider.RSASign(data, privateKey);
        }
        /// <summary>
        /// 较验
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signStr"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static bool Verify(string data, string signStr, string publicKey)
        {
            return EncryptProvider.RSAVerify(data, signStr, publicKey);
        }

        /// <summary>
        /// 获取pkcs
        /// </summary>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static (string publicPkcs1, string privatePkcs1) GetPKCS(bool pkcs1 = true)
        {
            return pkcs1 ? EncryptProvider.RsaToPkcs1() : EncryptProvider.RsaToPkcs8();
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string publicKey, bool pkcs1 = false)
        {
            return pkcs1 ? EncryptProvider.RSAEncrypt(publicKey, data, RSAEncryptionPadding.Pkcs1)
                : EncryptProvider.RSAEncrypt(publicKey, data);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] data, string publicKey, bool pkcs1 = false)
        {
            return pkcs1 ? EncryptProvider.RSADecrypt(publicKey, data, RSAEncryptionPadding.Pkcs1)
                : EncryptProvider.RSADecrypt(publicKey, data);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static string Encrypt(string data, string publicKey, bool pkcs1 = false)
        {
            return pkcs1 ? EncryptProvider.RSAEncrypt(publicKey, data, RSAEncryptionPadding.Pkcs1)
                : EncryptProvider.RSAEncrypt(publicKey, data);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privateKey"></param>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static string Decrypt(string data, string privateKey, bool pkcs1 = false)
        {
            return pkcs1 ? EncryptProvider.RSADecrypt(privateKey, data, RSAEncryptionPadding.Pkcs1)
                : EncryptProvider.RSADecrypt(privateKey, data);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, bool pkcs1 = false)
        {
            return Encrypt(data, Key.PublicKey, pkcs1);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] data, bool pkcs1 = false)
        {
            return Decrypt(data, Key.PrivateKey, pkcs1);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static string Encrypt(string data, bool pkcs1 = false)
        {
            return Encrypt(data, Key.PublicKey, pkcs1);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static string Decrypt(string data, bool pkcs1 = false)
        {
            return Decrypt(data, Key.PrivateKey, pkcs1);
        }

        /// <summary>
        /// GetPem,
        /// PKCS1 pem or PKCS8 pem
        /// </summary>
        /// <param name="pkcs1"></param>
        /// <returns></returns>
        public static (string publicPem, string privatePem) GetPem(bool pkcs1 = true)
        {
            return pkcs1 ? EncryptProvider.RSAToPem(false) : EncryptProvider.RSAToPem(true);
        }
        /// <summary>
        /// EncryptWithPem
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pemPublicKey"></param>
        /// <returns></returns>
        public static byte[] EncryptWithPem(byte[] data, string pemPublicKey)
        {
            return EncryptProvider.RSAEncryptWithPem(pemPublicKey, data);
        }
        /// <summary>
        /// DecryptWithPem
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pemPublicKey"></param>
        /// <returns></returns>
        public static byte[] DecryptWithPem(byte[] data, string pemPrivateKey)
        {
            return EncryptProvider.RSADecryptWithPem(pemPrivateKey, data);
        }
        /// <summary>
        /// EncryptWithPem
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pemPublicKey"></param>
        /// <returns></returns>
        public static string EncryptWithPem(string data, string pemPublicKey)
        {
            return EncryptProvider.RSAEncryptWithPem(pemPublicKey, data);
        }
        /// <summary>
        /// DecryptWithPem
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pemPublicKey"></param>
        /// <returns></returns>
        public static string DecryptWithPem(string data, string pemPrivateKey)
        {
            return EncryptProvider.RSADecryptWithPem(pemPrivateKey, data);
        }

    }
}
