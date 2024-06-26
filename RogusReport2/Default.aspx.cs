using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;

namespace RogusReport2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                LocalReport localReport = ReportViewer1.LocalReport;

                DataTable dt = GetData();

                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt));

                localReport.ReportPath = "reports/RogusReport.rdl";

            }
        }

        private DataTable GetData() { 
            DataTable dt = new DataTable();
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Apellidos", typeof(string));
            dt.Columns.Add("Cedula", typeof(string));
            dt.Columns.Add("Nivel", typeof(string));
            dt.Columns.Add("Seccion", typeof(string));
            dt.Columns.Add("CodigoCedula", typeof(byte[]));

            dt.Rows.Add("Juan", "Perez", "123456789", "Primero", "A", null);
            dt.Rows.Add("Maria", "Garcia", "987654321", "Segundo", "B", null);
            dt.Rows.Add("Carlos", "Lopez", "456123789", "Tercero", "C", null);
            dt.Rows.Add("Ana", "Martinez", "321654987", "Primero", "A", null);
            dt.Rows.Add("Luis", "Hernandez", "654987321", "Segundo", "B", null);
            dt.Rows.Add("Laura", "Gomez", "789123456", "Tercero", "C", null);
            dt.Rows.Add("David", "Ramirez", "147258369", "Primero", "A", null);
            dt.Rows.Add("Sofia", "Torres", "258369147", "Segundo", "B", null);
            dt.Rows.Add("Jorge", "Ruiz", "369147258", "Tercero", "C", null);
            dt.Rows.Add("Elena", "Flores", "741852963", "Primero", "A", null);
            dt.Rows.Add("Miguel", "Vasquez", "852963741", "Segundo", "B", null);
            dt.Rows.Add("Natalia", "Morales", "963741852", "Tercero", "C", null);
            dt.Rows.Add("Carlos", "Ortiz", "951753852", "Primero", "A", null);
            dt.Rows.Add("Patricia", "Mendez", "159357258", "Segundo", "B", null);
            dt.Rows.Add("Roberto", "Castillo", "357159753", "Tercero", "C", null);
            dt.Rows.Add("Carmen", "Silva", "753159357", "Primero", "A", null);
            dt.Rows.Add("Andres", "Reyes", "456852159", "Segundo", "B", null);
            dt.Rows.Add("Beatriz", "Iglesias", "852159456", "Tercero", "C", null);
            dt.Rows.Add("Victor", "Delgado", "159456852", "Primero", "A", null);
            dt.Rows.Add("Sara", "Campos", "456852753", "Segundo", "B", null);


            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 100,
                    Width = 300
                }
            };

            foreach (DataRow row in dt.Rows)
            {
                string id = row["cedula"].ToString();
                var barcodeBitmap = writer.Write(id);

                using (var stream = new System.IO.MemoryStream())
                {
                    barcodeBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    row["CodigoCedula"] = stream.ToArray();
                }
            }



            return dt;
            
        }

    }

}