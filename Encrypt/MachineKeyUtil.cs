/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt
*文件名： MachineKeyUtil
*版本号： V1.0.0.0
*唯一标识：2d2cc457-cff0-4edd-84c9-26753f4909e9
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 11:26:16
*描述：MachineKeyUtil
*
*=================================================
*修改标记
*修改时间：2022/7/29 11:26:16
*修改人： yswen
*版本号： V1.0.0.0
*描述：MachineKeyUtil
*
*****************************************************************************/
using Encrypt.Library.Core;

namespace Encrypt.Library
{
    /// <summary>
    /// MachineKeyUtil
    /// </summary>
    public static class MachineKeyUtil
    {
        /// <summary>
        /// CreateDecryptionKey
        /// </summary>
        /// <param name="len">decryption key length range is 16 -48</param>
        /// <returns></returns>
        public static string CreateDecryptionKey(int len)
        {
            return EncryptProvider.CreateDecryptionKey(len);
        }

        /// <summary>
        /// CreateValidationKey
        /// </summary>
        /// <param name="len">decryption key length range is 48 -128</param>
        /// <returns></returns>
        public static string CreateValidationKey(int len)
        {
            return EncryptProvider.CreateValidationKey(len);
        }
    }
}
