/****************************************************************************
*Copyright (c) YSWenli All Rights Reserved.
*CLR版本： .net8.0
*机器名称：WALLE
*Author：yswenli
*命名空间：Encrypt.Library.Core.Models
*文件名： EnumKeyConst
*版本号： V1.0.0.0
*唯一标识：488d6393-c8a7-40de-a0e6-98f0a4d2e4f8
*当前的用户域：WALLE
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2025/4/28 10:52:36
*描述：
*
*=================================================
*修改标记
*修改时间：2025/4/28 10:52:36
*修改人： yswenli
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
namespace Encrypt.Library.Core.Models
{
    /// <summary>
    /// 定义密钥相关的常量类。
    /// </summary>
    public enum EnumKeyConst
    {
        /// <summary>
        /// 密钥长度16字节。
        /// </summary>        
        Len16 = 16,

        /// <summary>
        /// 密钥长度24字节。
        /// </summary>
        Len24 = 24,

        /// <summary>
        /// 密钥长度32字节。
        /// </summary>
        Len32 = 32,

        /// <summary>
        /// 密钥长度64字节。
        /// </summary>
        Len64 = 64,

        /// <summary>
        /// 密钥位数128位。
        /// </summary>
        Bit128 = 128,

        /// <summary>
        /// 密钥位数192位。
        /// </summary>
        Bit192 = 192,

        /// <summary>
        /// 密钥位数256位。
        /// </summary>
        Bit256 = 256,

        /// <summary>
        /// 密钥位数512位。
        /// </summary>
        Bit512 = 512,

        /// <summary>
        /// 密钥位数1024位。
        /// </summary>
        Bit1024 = 1024,

        /// <summary>
        /// 密钥位数2048位。
        /// </summary>
        Bit2048 = 2048,
    }
}
