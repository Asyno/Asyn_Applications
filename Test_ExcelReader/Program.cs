using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Data;

namespace Test_ExcelReader
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Application app = new Application();
            Workbook book = null;
            Worksheet sheet = null;
            String path = @"C:\Users\Jan\Desktop\Database\2016-08_Database_current-version_01-08-2016.xls";

            app.Visible = false;
            app.ScreenUpdating = false;
            app.DisplayAlerts = false;

            book = app.Workbooks.Open(path);

            sheet = (Worksheet)book.Worksheets[2];
            
            Console.WriteLine("Hier Test: " + sheet.Cells[10, "A"].Value.ToString());
            Console.ReadLine();
            
            Marshal.FinalReleaseComObject(sheet);
            //app.DisplayAlerts = false;
            sheet = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            book.Close();
            Marshal.FinalReleaseComObject(book);
            book = null;
            app.Quit();
            Marshal.FinalReleaseComObject(app);
            app = null;*/

            OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.DataSource = @"C:\Users\Jan\Desktop\Database\2016-08_Database_current-version_01-08-2016.xls";
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES");

            System.Data.DataTable dt = null;

            using (OleDbConnection connection = new OleDbConnection(csbuilder.ConnectionString))
            {
                connection.Open();
                dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                connection.Close();
            }

            String[] excelSheets = new String[dt.Rows.Count];

            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                Console.WriteLine(row["TABLE_NAME"].ToString());
                i++;
            }
            // OLEDB
            csbuilder = new OleDbConnectionStringBuilder();
            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.DataSource = @"C:\Users\Jan\Desktop\Database\2016-08_Database_current-version_01-08-2016.xls";
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=NO");

            using(OleDbConnection connection = new OleDbConnection(csbuilder.ConnectionString))
            {
                DataTable sheet1 = new DataTable();
                connection.Open();
                string sqlQuery = @"SELECT * FROM [Database$]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(sqlQuery, connection))
                {
                    adapter.Fill(sheet1);
                    for (int n = 9; n < sheet1.Rows.Count; n++)
                        Console.WriteLine(n+" - "+sheet1.Rows[n].ItemArray[2]);
                    Console.WriteLine(" Rows: " + sheet1.Rows.Count + " - Columns: " + sheet1.Columns.Count);
                }
                connection.Close();
            }
            Console.ReadLine();
        }
    }
}
