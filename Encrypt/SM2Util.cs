/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library
*文件名： SM2Util
*版本号： V1.0.0.0
*唯一标识：7db51d2e-03dd-441c-913e-65b886691506
*当前的用户域：WALLE
*创建人： wenli
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/8/12 12:03:32
*描述：
*
*=================================================
*修改标记
*修改时间：2022/8/12 12:03:32
*修改人： yswen
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
using Encrypt.Library.Core.Domestic;

namespace Encrypt.Library
{
    public static class SM2Util
    {

        /// <summary>
        /// GenerateKey
        /// </summary>
        /// <param name="pubkey"></param>
        /// <param name="privkey"></param>
        public static void GenerateKey(out byte[] pubkey, out byte[] privkey)
        {
            SM2.GenerateKey(out pubkey, out privkey);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pubkey"></param>
        /// <param name="privkey"></param>
        /// <param name="mode"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] pubkey, byte[] privkey, Mode mode, byte[] data)
        {
            var sm2 = new SM2(pubkey, privkey, mode);
            return sm2.Encrypt(data);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pubkey"></param>
        /// <param name="privkey"></param>
        /// <param name="mode"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] pubkey, byte[] privkey, Mode mode, byte[] data)
        {
            var sm2 = new SM2(pubkey, privkey, mode);
            return sm2.Decrypt(data);
        }
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="pubkey"></param>
        /// <param name="privkey"></param>
        /// <param name="mode"></param>
        /// <param name="msg"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static byte[] Sign(byte[] pubkey, byte[] privkey, Mode mode, byte[] msg, byte[] id = null)
        {
            var sm2 = new SM2(pubkey, privkey, mode);
            return sm2.Sign(msg, id);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="pubkey"></param>
        /// <param name="privkey"></param>
        /// <param name="mode"></param>
        /// <param name="msg"></param>
        /// <param name="signature"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool VerifySign(byte[] pubkey, byte[] privkey, Mode mode, byte[] msg, byte[] signature, byte[] id = null)
        {
            var sm2 = new SM2(pubkey, privkey, mode);
            return sm2.VerifySign(msg, signature, id);
        }
    }
}
