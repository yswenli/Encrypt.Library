/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library
*文件名： GZipUtil
*版本号： V1.0.0.0
*唯一标识：b9141d79-afe2-4196-99de-e8dabaa83946
*当前的用户域：WALLE
*创建人： wenli
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/8/12 11:16:18
*描述：
*
*=================================================
*修改标记
*修改时间：2022/8/12 11:16:18
*修改人： yswen
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Encrypt.Library
{
    /// <summary>
    /// gzip工具类
    /// </summary>
    public static class GZipUtil
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="zippedData"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Compress(string str)
        {
            var data = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(Compress(data));
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="zippedStr"></param>
        /// <returns></returns>
        public static string Decompress(string zippedStr)
        {
            var zippedData = Convert.FromBase64String(zippedStr);
            return Encoding.UTF8.GetString(Decompress(zippedData));
        }


    }
}
