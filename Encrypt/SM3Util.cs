/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library
*文件名： SM3Util
*版本号： V1.0.0.0
*唯一标识：1e65668d-4c11-40e6-b4d5-a359d5fd6098
*当前的用户域：WALLE
*创建人： wenli
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/8/12 13:33:16
*描述：Sm3算法
*
*=================================================
*修改标记
*修改时间：2022/8/12 13:33:16
*修改人： yswen
*版本号： V1.0.0.0
*描述：Sm3算法
*
*****************************************************************************/
using System;
using System.Text;
using System.Text.RegularExpressions;

using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;

namespace Encrypt.Library
{
    /// <summary>
    /// Sm3算法(10进制的ASCII)  
    /// 在SHA-256基础上改进实现的一种算法  
    /// 对标国际MD5算法和SHA算法
    /// </summary>
    public class SM3Util
    {
        /// <summary>
        /// sm3加密(使用自定义密钥)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToSM3byte(string data, string key)
        {
            byte[] msg1 = Encoding.Default.GetBytes(data);
            byte[] key1 = Encoding.Default.GetBytes(key);

            KeyParameter keyParameter = new KeyParameter(key1);
            SM3Digest sm3 = new SM3Digest();

            HMac mac = new HMac(sm3);//带密钥的杂凑算法
            mac.Init(keyParameter);
            mac.BlockUpdate(msg1, 0, msg1.Length);
            byte[] result = new byte[mac.GetMacSize()];

            mac.DoFinal(result, 0);
            return Hex.Encode(result);
        }

        /// <summary>
        /// sm3加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns>二进制数组</returns>
        public static byte[] ToSM3byte(string data)
        {
            var msg = Encoding.Default.GetBytes(data);//把字符串转成16进制的ASCII码 
            SM3Digest sm3 = new SM3Digest();
            sm3.BlockUpdate(msg, 0, msg.Length);
            byte[] md = new byte[sm3.GetDigestSize()];//SM3算法产生的哈希值大小
            sm3.DoFinal(md, 0);
            return Hex.Encode(md);
        }

        /// <summary>
        /// sm3加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns>16进制字符串</returns>
        public static string ToSM3HexStr(string data)
        {
            var msg = Encoding.Default.GetBytes(data);//把字符串转成16进制的ASCII码 
            SM3Digest sm3 = new SM3Digest();
            sm3.BlockUpdate(msg, 0, msg.Length);
            byte[] md = new byte[sm3.GetDigestSize()];//SM3算法产生的哈希值大小
            sm3.DoFinal(md, 0);
            return new UTF8Encoding().GetString(Hex.Encode(md));
        }

        /// <summary>
        /// sm3加密(使用自定义Hex密钥)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToSM3HexStr(string data, string key)
        {
            byte[] msg1 = Encoding.Default.GetBytes(data);
            byte[] key1 = HexStringToBytes(key);

            KeyParameter keyParameter = new KeyParameter(key1);
            SM3Digest sm3 = new SM3Digest();

            HMac mac = new HMac(sm3);//带密钥的杂凑算法
            mac.Init(keyParameter);
            mac.BlockUpdate(msg1, 0, msg1.Length);
            byte[] result = new byte[mac.GetMacSize()];

            mac.DoFinal(result, 0);
            return new UTF8Encoding().GetString(Hex.Encode(result));
        }

        /// <summary>
        /// 16进制格式字符串转字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hexString)
        {
            hexString = Regex.Replace(hexString, @".{2}", "$0 ");
            //以 ' ' 分割字符串，并去掉空字符
            string[] chars = hexString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 16);
            }
            return returnBytes;
        }
    }
}
