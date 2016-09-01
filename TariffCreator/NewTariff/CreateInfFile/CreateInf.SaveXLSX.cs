using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TariffCreator.NewTariff.CreateInfFile
{
    partial class CreateInf
    {
        private void SaveXLSX(string path)
        {
            // Create xlsx document
            SpreadsheetDocument document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);
            WorkbookPart workbookPart = document.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();
            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            SheetData sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            Sheets sheets = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
            Sheet sheet = new Sheet() { Id = document.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = txtName.Text };

            // Create the table
            // Create the Rows
            Row row = new Row { RowIndex = 1 };
            sheetData.AppendChild(row);

            // Create the Cells
            Cell cell = new Cell { DataType = CellValues.InlineString, CellReference = "A1" };
            cell.AppendChild(new InlineString { Text = new Text { Text = "Hello World" } });
            row.AppendChild(cell);

            // save and close the file
            sheets.Append(sheet);
            workbookPart.Workbook.Save();
            document.Close();
        }
    }
}
