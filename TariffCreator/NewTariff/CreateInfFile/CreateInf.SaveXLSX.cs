using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Windows;
using TariffCreator.Config;

namespace TariffCreator.NewTariff.CreateInfFile
{
    partial class CreateInf
    {
        private void SaveXLSX(string path)
        {
            try
            {
                // Create xlsx document
                SpreadsheetDocument document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet();
                Sheets sheets = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                Sheet sheet = new Sheet() { Id = document.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = txtName.Text };
                UInt32 rowIndex = 0;

                // Create the table
                worksheetPart.Worksheet.Append(sheetData);

                // Tariff Info
                sheetData.AppendChild(CreateCell("A", ++rowIndex, "Tariff Info"));
                sheetData.AppendChild(CreateCell("A", ++rowIndex, "Tariff Name"));
                sheetData.AppendChild(CreateCell("B", rowIndex, txtName.Text));
                sheetData.AppendChild(CreateCell("A", ++rowIndex, "Tariff ID"));
                sheetData.AppendChild(CreateCell("B", rowIndex, txtIdent.Text));
                sheetData.AppendChild(CreateCell("A", ++rowIndex, "Default ChargeBand"));
                sheetData.AppendChild(CreateCell("B", rowIndex, ((ChargeBand)comboDefault.SelectedItem).CBName));
                sheetData.AppendChild(CreateCell("A", ++rowIndex, "Meter Unit Price"));
                sheetData.AppendChild(CreateCell("B", rowIndex, txtMeter.Text));
                rowIndex++;
                rowIndex++;

                // Price Overview
                sheetData.AppendChild(CreateCell("A", ++rowIndex, "Price Overview"));
                foreach(ChargeBand cb in CbListe)
                {
                    sheetData.AppendChild(CreateCell("A", ++rowIndex, cb.CBName));
                    sheetData.AppendChild(CreateCell("B", rowIndex, "ID: " + cb.CBShortName));
                    sheetData.AppendChild(CreateCell("C", rowIndex, "Price per min"));
                    sheetData.AppendChild(CreateCell("D", rowIndex, "" + cb.PriceMin));
                    sheetData.AppendChild(CreateCell("E", rowIndex, "Price per call"));
                    sheetData.AppendChild(CreateCell("F", rowIndex, "" + cb.PriceCall));
                    sheetData.AppendChild(CreateCell("G", rowIndex, "Price per " + cb.PricePer + "sec for every " + cb.PriceFor + "sec"));
                }
                rowIndex++;
                rowIndex++;

                // Country Overview
                sheetData.AppendChild(CreateCell("A", ++rowIndex, "Country Overview"));
                sheetData.AppendChild(CreateCell("A", ++rowIndex, "Prefix"));
                sheetData.AppendChild(CreateCell("B", rowIndex, "Description"));
                sheetData.AppendChild(CreateCell("C", rowIndex, "Price per min"));
                sheetData.AppendChild(CreateCell("D", rowIndex, "Price per Call"));
                sheetData.AppendChild(CreateCell("E", rowIndex, "Charge Band"));
                foreach(ChargeBand cb in CbListe)
                {
                    foreach(Country count in cb.Countrys)
                    {
                        sheetData.AppendChild(CreateCell("A", ++rowIndex, count.Prefix));
                        sheetData.AppendChild(CreateCell("B", rowIndex, count.Description));
                        sheetData.AppendChild(CreateCell("C", rowIndex, "" + cb.PriceMin));
                        sheetData.AppendChild(CreateCell("D", rowIndex, "" + cb.PriceCall));
                        sheetData.AppendChild(CreateCell("E", rowIndex, cb.CBName));
                    }
                }

                // save and close the file
                sheets.Append(sheet);
                workbookPart.Workbook.Save();
                document.Close();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        /// <summary>
        /// Create a new Cell opject
        /// </summary>
        /// <param name="rowIndex">Row Index</param>
        /// <param name="columnIndex">Column Index</param>
        /// <param name="text">Text Value for the Cell</param>
        /// <returns></returns>
        private Row CreateCell (string columnIndex, UInt32 rowIndex, string text)
        {
            Row row = new Row { RowIndex = rowIndex };
            Cell cell = new Cell { DataType = CellValues.InlineString, CellReference = columnIndex + rowIndex };
            cell.AppendChild(new InlineString { Text = new Text { Text = text } });
            row.AppendChild(cell);
            return row;
        }
    }
}
