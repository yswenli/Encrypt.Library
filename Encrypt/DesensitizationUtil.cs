/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library
*文件名： DesensitizationUtil
*版本号： V1.0.0.0
*唯一标识：039b1929-1372-4e6c-b05f-09a0465f768c
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/7/29 13:40:18
*描述：脱敏工具类
*
*=================================================
*修改标记
*修改时间：2022/7/29 13:40:18
*修改人： yswen
*版本号： V1.0.0.0
*描述：脱敏工具类
*
*****************************************************************************/
using System;
using System.Text;

namespace Encrypt.Library
{
    /// <summary>
    /// 脱敏工具类
    /// </summary>
    public static class DesensitizationUtil
    {
        private static readonly int SIZE = 6;
        private static readonly String SYMBOL = "*";

        /// <summary>
        /// 通用脱敏方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String CommonDisplay(String value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            int len = value.Length;
            int pamaone = len / 2;
            int pamatwo = pamaone - 1;
            int pamathree = len % 2;
            StringBuilder stringBuilder = new StringBuilder();
            if (len <= 2)
            {
                if (pamathree == 1)
                {
                    return SYMBOL;
                }
                stringBuilder.Append(SYMBOL);
                stringBuilder.Append(value.ToCharArray()[len - 1]);
            }
            else
            {
                if (pamatwo <= 0)
                {
                    stringBuilder.Append(value.Substring(0, 1));
                    stringBuilder.Append(SYMBOL);
                    stringBuilder.Append(value.Substring(len - 1, len));

                }
                else if (pamatwo >= SIZE / 2 && SIZE + 1 != len)
                {
                    int pamafive = (len - SIZE) / 2;
                    stringBuilder.Append(value.Substring(0, pamafive));
                    for (int i = 0; i < SIZE; i++)
                    {
                        stringBuilder.Append(SYMBOL);
                    }
                    if ((pamathree == 0 && SIZE / 2 == 0) || (pamathree != 0 && SIZE % 2 != 0))
                    {
                        stringBuilder.Append(value.Substring(len - pamafive, len));
                    }
                    else
                    {
                        stringBuilder.Append(value.Substring(len - (pamafive + 1), len));
                    }
                }
                else
                {
                    int pamafour = len - 2;
                    stringBuilder.Append(value.Substring(0, 1));
                    for (int i = 0; i < pamafour; i++)
                    {
                        stringBuilder.Append(SYMBOL);
                    }
                    stringBuilder.Append(value.Substring(len - 1, len));
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 姓名敏感处理
        /// </summary>
        /// <param name="fullName">姓名</param>
        /// <returns>脱敏后的姓名</returns>
        public static string GetSensitiveName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return string.Empty;

            string familyName = fullName.Substring(0, 1);
            string end = fullName.Substring(fullName.Length - 1, 1);
            string name = string.Empty;
            //长度为2
            if (fullName.Length <= 2) name = familyName + "*";
            //长度大于2
            else if (fullName.Length >= 3)
            {
                name = familyName.PadRight(fullName.Length - 1, '*') + end;
            }
            return name;
        }

        /// <summary>
        /// 身份证脱敏
        /// </summary>
        /// <param name="idCardNo">身份证号</param>
        /// <returns>脱敏后的身份证号</returns>
        private static string GetSensitiveIdCardNo(string idCardNo)
        {
            if (string.IsNullOrEmpty(idCardNo)
                || (idCardNo.Length != 15 && idCardNo.Length != 18)) return idCardNo;

            string begin = idCardNo.Substring(0, 6);
            string middle = idCardNo.Substring(6, 8);
            string end = idCardNo.Substring(14, idCardNo.Length - 14);

            string card = string.Empty;
            card = begin + "********" + end;
            return card;
        }

        /// <summary>
        /// 手机号脱敏
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static String GetSensitivePhoneNumber(String phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                phoneNumber = phoneNumber.Replace("(\\w{3})\\w*(\\w{4})", "$1****$2");
            }
            return phoneNumber;
        }
    }
}
