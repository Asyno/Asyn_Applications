using System;
using System.Text;
using System.Xml;
using System.Data;
using TariffCreator.Config;

namespace TariffCreator.NewTariff.CreateInfFile
{
    partial class CreateInf
    {
        private void SaveXML(string path)
        {
            DataTable table = new DataTable();

            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());

            table.TableName = txtName.Text;
            table.Rows.Add(new Object[] { "Tariff Info", "", "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "Tariff Name:", txtName.Text, "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "Tariff ID:", txtIdent.Text, "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "Meter Price:", txtMeter.Text, "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "Default ChargeBand:", ((ChargeBand)comboDefault.SelectedItem).CBName, "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "", "", "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "", "", "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "Price Overview", "", "", "", "", "", "", "" });
            foreach (ChargeBand cb in CbListe)
                table.Rows.Add(new Object[] { cb.CBName, "ID:" + cb.CBShortName,
                    "Price per min:", cb.PriceMin, "Price per Call:", cb.PriceCall,
                    "Price per " + cb.PricePer + " sec for every " + cb.PriceFor + "sec", "" });
            table.Rows.Add(new Object[] { "", "", "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "", "", "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "Country Overview", "", "", "", "", "", "", "" });
            table.Rows.Add(new Object[] { "Prefix", "Description", "Price per min",
                "Price per Call", "charge Band", "", "", "" });
            foreach(ChargeBand cb in CbListe)
            {
                foreach (Country count in cb.Countrys)
                    table.Rows.Add(new Object[] { count.Prefix, count.Description, cb.PriceMin, cb.PriceCall, cb.CBName, "", "", "" });
            }

            ExportDataTableToWorksheet(table, path);
        }
        /// <summary>
        /// Erzeugt aus einer DataTable ein Excel-XML-Dokument mit SpreadsheetML.
        /// </summary>        
        /// <param name="dataSource">Datenquelle, die in Excel exportiert werden soll</param>
        /// <param name="fileName">Dateiname der Ausgabe-XML-Datei</param>
        public static void ExportDataTableToWorksheet(DataTable dataSource, string fileName)
        {
            try
            {
                // XML-Schreiber erzeugen
                XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8);

                // Ausgabedatei für bessere Lesbarkeit formatieren (einrücken etc.)
                writer.Formatting = Formatting.Indented;

                // <?xml version="1.0"?>
                writer.WriteStartDocument();

                // <?mso-application progid="Excel.Sheet"?>
                writer.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");

                // <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet >"
                writer.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");

                // Definition der Namensräume schreiben 
                writer.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
                writer.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
                writer.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
                writer.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");

                // <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
                writer.WriteStartElement("DocumentProperties", "urn:schemas-microsoft-com:office:office");

                // Dokumenteingeschaften schreiben
                writer.WriteElementString("Author", Environment.UserName);
                writer.WriteElementString("LastAuthor", Environment.UserName);
                writer.WriteElementString("Created", DateTime.Now.ToString("u") + "Z");
                writer.WriteElementString("Company", "Unknown");
                writer.WriteElementString("Version", "11.8122");

                // </DocumentProperties>
                writer.WriteEndElement();

                // <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
                writer.WriteStartElement("ExcelWorkbook", "urn:schemas-microsoft-com:office:excel");

                // Arbeitsmappen-Einstellungen schreiben
                writer.WriteElementString("WindowHeight", "13170");
                writer.WriteElementString("WindowWidth", "17580");
                writer.WriteElementString("WindowTopX", "120");
                writer.WriteElementString("WindowTopY", "60");
                writer.WriteElementString("ProtectStructure", "False");
                writer.WriteElementString("ProtectWindows", "False");

                // </ExcelWorkbook>
                writer.WriteEndElement();

                // <Styles>
                writer.WriteStartElement("Styles");

                // <Style ss:ID="Default" ss:Name="Normal">
                writer.WriteStartElement("Style");
                writer.WriteAttributeString("ss", "ID", null, "Default");
                writer.WriteAttributeString("ss", "Name", null, "Normal");

                // <Alignment ss:Vertical="Bottom"/>
                writer.WriteStartElement("Alignment");
                writer.WriteAttributeString("ss", "Vertical", null, "Bottom");
                writer.WriteEndElement();

                // Verbleibende Sytle-Eigenschaften leer schreiben
                writer.WriteElementString("Borders", null);
                writer.WriteElementString("Font", null);
                writer.WriteElementString("Interior", null);
                writer.WriteElementString("NumberFormat", null);
                writer.WriteElementString("Protection", null);

                // </Style>
                writer.WriteEndElement();

                // </Styles>
                writer.WriteEndElement();

                // <Worksheet ss:Name="xxx">
                writer.WriteStartElement("Worksheet");
                writer.WriteAttributeString("ss", "Name", null, dataSource.TableName);

                // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                writer.WriteStartElement("Table");
                writer.WriteAttributeString("ss", "ExpandedColumnCount", null, dataSource.Columns.Count.ToString());
                writer.WriteAttributeString("ss", "ExpandedRowCount", null, dataSource.Rows.Count.ToString());
                writer.WriteAttributeString("x", "FullColumns", null, "1");
                writer.WriteAttributeString("x", "FullRows", null, "1");
                writer.WriteAttributeString("ss", "DefaultColumnWidth", null, "100");

                // Alle Zeilen der Datenquelle durchlaufen
                foreach (DataRow row in dataSource.Rows)
                {
                    // <Row>
                    writer.WriteStartElement("Row");

                    // Alle Zellen der aktuellen Zeile durchlaufen
                    foreach (object cellValue in row.ItemArray)
                    {
                        // <Cell>
                        writer.WriteStartElement("Cell");

                        // <Data ss:Type="String">xxx</Data>
                        writer.WriteStartElement("Data");
                        writer.WriteAttributeString("ss", "Type", null, "String");

                        // Zelleninhakt schreiben
                        writer.WriteValue(cellValue);

                        // </Data>
                        writer.WriteEndElement();

                        // </Cell>
                        writer.WriteEndElement();
                    }
                    // </Row>
                    writer.WriteEndElement();
                }
                // </Table>
                writer.WriteEndElement();

                // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                writer.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                // Seiteneinstellungen schreiben
                writer.WriteStartElement("PageSetup");
                writer.WriteStartElement("Header");
                writer.WriteAttributeString("x", "Margin", null, "0.4921259845");
                writer.WriteEndElement();
                writer.WriteStartElement("Footer");
                writer.WriteAttributeString("x", "Margin", null, "0.4921259845");
                writer.WriteEndElement();
                writer.WriteStartElement("PageMargins");
                writer.WriteAttributeString("x", "Bottom", null, "0.984251969");
                writer.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                writer.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                writer.WriteAttributeString("x", "Top", null, "0.984251969");
                writer.WriteEndElement();
                writer.WriteEndElement();

                // <Selected/>
                writer.WriteElementString("Selected", null);

                // <Panes>
                writer.WriteStartElement("Panes");

                // <Pane>
                writer.WriteStartElement("Pane");

                // Bereichseigenschaften schreiben
                writer.WriteElementString("Number", "1");
                writer.WriteElementString("ActiveRow", "1");
                writer.WriteElementString("ActiveCol", "1");

                // </Pane>
                writer.WriteEndElement();

                // </Panes>
                writer.WriteEndElement();

                // <ProtectObjects>False</ProtectObjects>
                writer.WriteElementString("ProtectObjects", "False");

                // <ProtectScenarios>False</ProtectScenarios>
                writer.WriteElementString("ProtectScenarios", "False");

                // </WorksheetOptions>
                writer.WriteEndElement();

                // </Worksheet>
                writer.WriteEndElement();

                // </Workbook>
                writer.WriteEndElement();

                // Datei auf Festplatte schreiben
                writer.Flush();
                writer.Close();
            }

            catch(Exception e) { System.Windows.MessageBox.Show(e.Message); }
        }
    }
}
