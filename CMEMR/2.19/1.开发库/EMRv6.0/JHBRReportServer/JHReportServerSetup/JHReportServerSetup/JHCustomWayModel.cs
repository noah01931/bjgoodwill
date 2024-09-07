using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//////////////////////////////////////////////////////////////////////////
//存储用户安装文件的自定义路径 李海威2013-05-10
//////////////////////////////////////////////////////////////////////////

namespace JHEMR.JHReportServerSetup
{
   class JHCustomWayModel
    {
        private static string strPath;
        /// <summary>
        /// 自定义安装路径
        /// </summary>
        public static string StrPath
        {
            get { return strPath; }
            set { strPath = value; }
        }

        private static int proMax=300;
        /// <summary>
        /// 滚动条最大值
        /// </summary>
        public static int ProMax
        {
            get { return proMax; }
            set { proMax = 300; }
        }

        private static int proCurr;
        /// <summary>
        /// 滚动条当前值
        /// </summary>
        public static int ProCurr
        {
            get { return proCurr; }
            set { proCurr = value; }
        }
    }
}
