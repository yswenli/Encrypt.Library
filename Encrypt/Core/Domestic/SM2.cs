/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library.Core.Domestic
*文件名： SM2
*版本号： V1.0.0.0
*唯一标识：c9205eb8-db08-4f6e-91e2-e1270dce4f0c
*当前的用户域：WALLE
*创建人： wenli
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/8/12 11:58:13
*描述：基于ECC算法非对称加密
*
*=================================================
*修改标记
*修改时间：2022/8/12 11:58:13
*修改人： yswen
*版本号： V1.0.0.0
*描述：基于ECC算法非对称加密
*
*****************************************************************************/
using System;
using System.Text.RegularExpressions;

using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;

namespace Encrypt.Library.Core.Domestic
{
    /// <summary>
    /// 基于ECC算法非对称加密
    /// </summary>
    public class SM2
    {
        /// <summary>
        /// 基于ECC算法非对称加密
        /// </summary>
        public SM2(byte[] pubkey, byte[] privkey, Mode mode)
        {
            this.pubkey = pubkey;
            this.privkey = privkey;
            this.mode = mode;
        }
        /// <summary>
        /// 基于ECC算法非对称加密
        /// </summary>
        /// <param name="pubkey"></param>
        /// <param name="privkey"></param>
        /// <param name="mode"></param>
        /// <param name="isPkcs8"></param>
        public SM2(string pubkey, string privkey, Mode mode = Mode.C1C2C3, bool isPkcs8 = false)
        {
            if (!isPkcs8)
            {
                if (pubkey != null) this.pubkey = Decode(pubkey);
                if (privkey != null) this.privkey = Decode(privkey);
            }
            else
            {
                if (pubkey != null) this.pubkey = ((ECPublicKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(pubkey))).Q.GetEncoded();
                if (privkey != null) this.privkey = ((ECPrivateKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privkey))).D.ToByteArray();
            }
            this.mode = mode;
        }
        byte[] pubkey;
        byte[] privkey;
        Mode mode;
        ICipherParameters _privateKeyParameters;
        ICipherParameters PrivateKeyParameters
        {
            get
            {
                var r = _privateKeyParameters;
                if (r == null) r = _privateKeyParameters = new ECPrivateKeyParameters(new BigInteger(1, privkey), new ECDomainParameters(GMNamedCurves.GetByName("SM2P256V1")));
                return r;
            }
        }
        ICipherParameters _publicKeyParameters;
        ICipherParameters PublicKeyParameters
        {
            get
            {
                var r = _publicKeyParameters;
                if (r == null)
                {
                    var x9ec = GMNamedCurves.GetByName("SM2P256V1");
                    r = _publicKeyParameters = new ECPublicKeyParameters(x9ec.Curve.DecodePoint(pubkey), new ECDomainParameters(x9ec));
                }
                return r;
            }
        }
        /// <summary>
        /// GenerateKeyHex
        /// </summary>
        /// <param name="pubkey"></param>
        /// <param name="privkey"></param>
        public static void GenerateKeyHex(out string pubkey, out string privkey)
        {
            GenerateKey(out var a, out var b);
            pubkey = Hex.ToHexString(a);
            privkey = Hex.ToHexString(b);
        }
        /// <summary>
        /// GenerateKey
        /// </summary>
        /// <param name="pubkey"></param>
        /// <param name="privkey"></param>
        public static void GenerateKey(out byte[] pubkey, out byte[] privkey)
        {
            var g = new ECKeyPairGenerator();
            g.Init(new ECKeyGenerationParameters(new ECDomainParameters(GMNamedCurves.GetByName("SM2P256V1")), new SecureRandom()));
            var k = g.GenerateKeyPair();
            pubkey = ((ECPublicKeyParameters)k.Public).Q.GetEncoded(false);
            privkey = ((ECPrivateKeyParameters)k.Private).D.ToByteArray();
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] data)
        {
            if (mode == Mode.C1C3C2) data = C132ToC123(data);
            var sm2 = new SM2Engine(new SM3Digest());
            sm2.Init(false, this.PrivateKeyParameters);
            return sm2.ProcessBlock(data, 0, data.Length);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] data)
        {
            var sm2 = new SM2Engine(new SM3Digest());
            sm2.Init(true, new ParametersWithRandom(PublicKeyParameters));
            data = sm2.ProcessBlock(data, 0, data.Length);
            if (mode == Mode.C1C3C2) data = C123ToC132(data);
            return data;
        }
        /// <summary>
        /// Sign
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public byte[] Sign(byte[] msg, byte[] id = null)
        {
            var sm2 = new SM2Signer(new SM3Digest());
            ICipherParameters cp;
            if (id != null) cp = new ParametersWithID(new ParametersWithRandom(PrivateKeyParameters), id);
            else cp = new ParametersWithRandom(PrivateKeyParameters);
            sm2.Init(true, cp);
            sm2.BlockUpdate(msg, 0, msg.Length);
            return sm2.GenerateSignature();
        }
        /// <summary>
        /// VerifySign
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="signature"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool VerifySign(byte[] msg, byte[] signature, byte[] id = null)
        {
            var sm2 = new SM2Signer(new SM3Digest());
            ICipherParameters cp;
            if (id != null) cp = new ParametersWithID(PublicKeyParameters, id);
            else cp = PublicKeyParameters;
            sm2.Init(false, cp);
            sm2.BlockUpdate(msg, 0, msg.Length);
            return sm2.VerifySignature(signature);
        }
        static byte[] C123ToC132(byte[] c1c2c3)
        {
            var gn = GMNamedCurves.GetByName("SM2P256V1");
            int c1Len = (gn.Curve.FieldSize + 7) / 8 * 2 + 1;
            int c3Len = 32;
            byte[] result = new byte[c1c2c3.Length];
            Array.Copy(c1c2c3, 0, result, 0, c1Len); //c1
            Array.Copy(c1c2c3, c1c2c3.Length - c3Len, result, c1Len, c3Len); //c3
            Array.Copy(c1c2c3, c1Len, result, c1Len + c3Len, c1c2c3.Length - c1Len - c3Len); //c2
            return result;
        }
        static byte[] C132ToC123(byte[] c1c3c2)
        {
            var gn = GMNamedCurves.GetByName("SM2P256V1");
            int c1Len = (gn.Curve.FieldSize + 7) / 8 * 2 + 1;
            int c3Len = 32;
            byte[] result = new byte[c1c3c2.Length];
            Array.Copy(c1c3c2, 0, result, 0, c1Len); //c1: 0->65
            Array.Copy(c1c3c2, c1Len + c3Len, result, c1Len, c1c3c2.Length - c1Len - c3Len); //c2
            Array.Copy(c1c3c2, c1Len, result, c1c3c2.Length - c3Len, c3Len); //c3
            return result;
        }
        static byte[] Decode(string key)
        {
            return Regex.IsMatch(key, "^[0-9a-f]+$", RegexOptions.IgnoreCase) ? Hex.Decode(key) : Convert.FromBase64String(key);
        }

    }

    public enum Mode
    {
        C1C2C3, C1C3C2
    }
}
