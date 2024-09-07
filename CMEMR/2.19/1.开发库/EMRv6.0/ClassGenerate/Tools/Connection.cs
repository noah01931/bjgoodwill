using System;
using System.Collections.Generic;
using System.Text;

namespace ClassGenerate
{
    public class Connection
    {
        private string server = ".";

        public string Server
        {
            get { return server; }
            set { server = value; }
        }
        private string database = "master";

        public string Database
        {
            get { return database; }
            set { database = value; }
        }
        private string uid = "sa";

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
        private string pwd = "";

        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

        private string windows = "Integrated Security=True;";
        bool isWindows = true;
        public bool IsWindows
        {
            get { return isWindows; }
            set 
            { 
                isWindows = value;
                if (isWindows)
                {
                    windows = "Integrated Security=True;";
                }
                else
                {
                    windows = "";
                }
            }
        }

        public string ConnectionString
        {
            get
            {
                string connectionString = string.Format("server={0};database={1};",server,database);
                if (IsWindows)
                {
                    connectionString += windows;
                 
                }
                else
                {
                    connectionString += string.Format("uid={0};pwd={1};",uid,pwd);
                }
                return connectionString;
            }
        }
    }

}
