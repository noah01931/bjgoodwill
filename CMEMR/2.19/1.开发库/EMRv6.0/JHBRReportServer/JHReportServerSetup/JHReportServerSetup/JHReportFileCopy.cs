using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JHEMR.JHReportServerSetup
{
    class JHReportFileCopy
    {
        public void cest()
        {
            //JHCustomWayModel dd = new JHCustomWayModel();
            //dd.ProMax=3333;
            JHCustomWayModel.ProMax = 20000;
        }
        /// <summary>
        /// 将整个文件夹复制到目标文件夹中。
        /// 李海威 2013-04-01
        /// </summary>
        /// <param name="srcPath">源文件夹</param>
        /// <param name="aimPath">目标文件夹</param>
        /// <returns></returns>
        public static bool  CopyDir(string srcPath, string aimPath)
        {
            try
            {
               
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;

                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                {
                    Directory.CreateDirectory(aimPath);
                }
                else
                {
                    return true;
                }
               
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                foreach (string file in fileList)
                {
                  
                    if (Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    }
                    else
                    {
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
