using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BenkhoudM_LAB_07449_420_DA3_AS.Utils
{
    public static class DbUtilsSimple
    {
        //public static readonly string EXECUTION_DIRECTORY = Path.GetFullPath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        //public static readonly string DB_FILE_PATH = Path.GetFullPath(EXECUTION_DIRECTORY + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "Lab.mdf");

        public static SqlConnection GetDefaultConnection()
        {

            string DB_FILE_PATH = $"C:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQL2019EXPRESS\\MSSQL\\DATA\\Lab.mdf";//SQL2019EXPRESS
                //$"C:\\Users\\monta\\Documents\\session 3\\VisuelStudio\\BenkhoudMLabs\\BenkhoudM_LAB_07449_420-DA3-AS\\Lab.mdf";
           // string connString = $"Server=(LocalDB)\\MSSQLLocalDB; Integrated Security = True;AttachDbFilename={DB_FILE_PATH};";
            string connString = $"Data Source=PC_MONTA\\SQL2019EXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}
