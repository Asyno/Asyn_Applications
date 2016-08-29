using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace CG_DatabaseReader
{
    partial class MainWindow
    {
        private async void ReadInvoiceList()
        {
            ObservableCollection<Invoice> list = new ObservableCollection<Invoice>();
            await Task.Delay(1);
            OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.DataSource = ExcelPath;
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=NO");

            using (OleDbConnection connection = new OleDbConnection(csbuilder.ConnectionString))
            {
                DataTable sheet1 = new DataTable();
                connection.Open();
                string sqlQuery = @"SELECT * FROM [Database$]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(sqlQuery, connection))
                {
                    try { adapter.Fill(sheet1); }
                    catch (Exception) { }
                    for (int i = 9; i < sheet1.Rows.Count; i++)
                    {
                        if(!string.IsNullOrWhiteSpace(sheet1.Rows[i].ItemArray[26] as string))
                        {
                            Invoice line = new Invoice();
                            line.InvMonth = "" + sheet1.Rows[i].ItemArray[0] as string;
                            line.InvDate = "" + sheet1.Rows[i].ItemArray[1] as string;
                            line.Customer = "" + sheet1.Rows[i].ItemArray[2] as string;
                            line.Debitor = "" + sheet1.Rows[i].ItemArray[3] as string;
                            line.AccNewInv = "" + sheet1.Rows[i].ItemArray[4] as string;
                            line.DI = "" + sheet1.Rows[i].ItemArray[9] as string;
                            line.Project = "" + sheet1.Rows[i].ItemArray[10] as string;
                            line.Comment = "" + sheet1.Rows[i].ItemArray[20] as string;
                            line.Commission = "" + sheet1.Rows[i].ItemArray[21] as string;
                            line.UstID = "" + sheet1.Rows[i].ItemArray[22] as string;
                            line.DueDate = "" + sheet1.Rows[i].ItemArray[23] as string;
                            line.Payment = "" + sheet1.Rows[i].ItemArray[24] as string;
                            line.Country = "" + sheet1.Rows[i].ItemArray[25] as string;
                            line.InvoiceNo = "" + sheet1.Rows[i].ItemArray[26] as string;
                            line.CSANCSA = "" + sheet1.Rows[i].ItemArray[27] as string;
                            list.Add(line);
                        }
                    }
                }
                connection.Close();
            }
            InvoiceList = list;
        }
    }
}
