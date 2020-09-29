using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Citta_T1.Model.Documents;

namespace Citta_T1.Core
{
    abstract class DocumentImportEngine
    {
        public static IEnumerable<DocumentImportEngine> GetEngines()
        {
            return new DocumentImportEngine[]
            {
                new Citta_T1.Core.Imports.CsvEngine(),
                new Citta_T1.Core.Imports.FreeMindEngine()
            };
        }

        public abstract DocumentType DocumentType { get; }

        public abstract Document ImportFile(string filename);
    }
}
