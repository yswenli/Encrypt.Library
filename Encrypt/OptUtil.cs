/****************************************************************************
*Copyright (c) 2023 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library
*文件名： OptUtil
*版本号： V1.0.0.0
*唯一标识：66127935-3a06-4125-bcc1-4e678a739ae0
*当前的用户域：WALLE
*创建人： yswenli
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2023/7/26 17:51:40
*描述：
*
*=================================================
*修改标记
*修改时间：2023/7/26 17:51:40
*修改人： yswenli
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
using System.Text;

using Encrypt.Library.Core.Opt;

namespace Encrypt.Library
{
    public static class OptUtil
    {
        /// <summary>
        /// 获取totp计算值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="size"></param>
        /// <param name="otpHashMode"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetTotp(string key, int size = 6, OtpHashMode otpHashMode = OtpHashMode.Sha1, int seconds = 30)
        {
            var otp = new Totp(Encoding.UTF8.GetBytes(key), seconds, otpHashMode, size);
            return otp.ComputeTotp();
        }

        /// <summary>
        /// 获取hotp计算值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="counter"></param>
        /// <param name="otpHashMode"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetHotp(string key, long counter, OtpHashMode otpHashMode = OtpHashMode.Sha1, int seconds = 30)
        {
            var otp = new Hotp(Encoding.UTF8.GetBytes(key), otpHashMode, seconds);
            return otp.ComputeHOTP(counter);
        }


        /// <summary>
        /// 获取optUri
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="otpType"></param>
        /// <param name="user"></param>
        /// <param name="issuer"></param>
        /// <param name="hash"></param>
        /// <param name="digits"></param>
        /// <param name="period"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public static string GetOtpUri(string secret, OtpType otpType, string user, string issuer,
        OtpHashMode hash, int digits, int period, int counter)
        {
            return new OtpUri(otpType, secret, user, issuer, hash, digits, period, counter).ToString();
        }
    }
}
