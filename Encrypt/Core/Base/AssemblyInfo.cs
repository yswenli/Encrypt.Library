/****************************************************************************
*Copyright (c) 2022 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：Encrypt.Library.Core.Base
*文件名： AssemblyInfo
*版本号： V1.0.0.0
*唯一标识：a3c2f3d4-19aa-4ef8-a9ed-ecf8a0547dd8
*当前的用户域：WALLE
*创建人： wenli
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/8/12 11:56:06
*描述：
*
*=================================================
*修改标记
*修改时间：2022/8/12 11:56:06
*修改人： yswen
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Encrypt.Library.Core.Base
{

    internal class AssemblyInfo
    {
        private static string version = null;

        public static string Version
        {
            get
            {
                if (version == null)
                {
#if PORTABLE
#if NEW_REFLECTION
                var a = typeof(AssemblyInfo).GetTypeInfo().Assembly;
                var c = a.GetCustomAttributes(typeof(AssemblyVersionAttribute));
#else
                var a = typeof(AssemblyInfo).Assembly;
                var c = a.GetCustomAttributes(typeof(AssemblyVersionAttribute), false);
#endif
                var v = (AssemblyVersionAttribute)c.FirstOrDefault();
                if (v != null)
                {
                    version = v.Version;
                }
#else
                    version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
#endif

                    // if we're still here, then don't try again
                    if (version == null)
                    {
                        version = string.Empty;
                    }
                }

                return version;
            }
        }
    }
}
