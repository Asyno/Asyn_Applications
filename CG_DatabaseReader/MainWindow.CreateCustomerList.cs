using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CG_DatabaseReader
{
    partial class MainWindow
    {
        private void ReadCustomerList()
        {
            ObservableCollection<Customer> list = new ObservableCollection<Customer>();
            OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.DataSource = ExcelPath;
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=NO");

            using (OleDbConnection connection = new OleDbConnection(csbuilder.ConnectionString))
            {
                DataTable sheet1 = new DataTable();
                connection.Open();
                string sqlQuery = @"SELECT * FROM ['invoice schedule new$']";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(sqlQuery, connection))
                {
                    adapter.Fill(sheet1);
                    for (int i = 3; i < sheet1.Rows.Count; i++)
                    {
                        if(!string.IsNullOrWhiteSpace(sheet1.Rows[i].ItemArray[1] as string))
                        {
                            Customer line = new Customer();
                            line.Debitor = "" + sheet1.Rows[i].ItemArray[0] as string;
                            line.Projects = "" + sheet1.Rows[i].ItemArray[1] as string;
                            line.Period = "" + sheet1.Rows[i].ItemArray[2] as string;
                            line.Product = "" + sheet1.Rows[i].ItemArray[4] as string;
                            line.Name = "" + sheet1.Rows[i].ItemArray[5] as string;
                            line.Zusatz = "" + sheet1.Rows[i].ItemArray[6] as string;
                            line.Strasse = "" + sheet1.Rows[i].ItemArray[7] as string;
                            line.Stadt = "" + sheet1.Rows[i].ItemArray[8] as string;
                            line.Country = "" + sheet1.Rows[i].ItemArray[9] as string;
                            line.Comment = "" + sheet1.Rows[i].ItemArray[10] as string;
                            line.TigerComms = "" + sheet1.Rows[i].ItemArray[11] as string;
                            line.SupportLevel = "" + sheet1.Rows[i].ItemArray[12] as string;
                            line.Contract = "" + sheet1.Rows[i].ItemArray[13] as string;
                            line.Warranty = "" + sheet1.Rows[i].ItemArray[14] as string;
                            line.Volume = "" + sheet1.Rows[i].ItemArray[15] as string;
                            list.Add(line);
                            Debug.WriteLine(i + " - " + line.Projects + " - " + line.Debitor);
                        }
                    }
                }
                connection.Close();
                txtStatus.Text = "";
            }
            CustomerList = list;
        }
    }
}
