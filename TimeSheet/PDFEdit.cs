using System.IO;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeSheet
{
    public class PDFEdit
    {
        string a = "1st - 15th";
        string b = "16th - 30th";
        string c = "16th - 31st";

        public void ReplacePdfForm()
        {
            string fileNameExisting = "time.pdf";
            string fileNameNew = @"C:\Users\Administrator\Desktop\ok.pdf";

            using (var existingFileStream = new FileStream(fileNameExisting, FileMode.Open))
            using (var newFileStream = new FileStream(fileNameNew, FileMode.Create))
            {
                // Open existing PDF
                var pdfReader = new PdfReader(existingFileStream);

                // PdfStamper, which will create
                var stamper = new PdfStamper(pdfReader, newFileStream);

                var form = stamper.AcroFields;
                var fieldKeys = form.Fields.Keys;

                foreach (string fieldKey in fieldKeys)
                {
                    if (fieldKey.Equals("First Name"))
                    {
                        form.SetField(fieldKey, "John");
                    }
                    else if (fieldKey.Equals("Last Name"))
                    {
                        form.SetField(fieldKey, "Singh");
                    }
                    else if (fieldKey.Equals("Pay Period From To"))
                    {
                        form.SetField(fieldKey, "shit");
                    }

                }

                // "Flatten" the form so it wont be editable/usable anymore
                stamper.FormFlattening = false;

                // You can also specify fields to be flattened, which
                // leaves the rest of the form still be editable/usable
                stamper.PartialFormFlattening("field1");

                stamper.Close();
                pdfReader.Close();
            }
        }
    }
}