using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using C2.Model.Documents;

namespace C2.Core
{
    abstract class DocumentImportEngine
    {
        public static IEnumerable<DocumentImportEngine> GetEngines()
        {
            return new DocumentImportEngine[]
            {
                new C2.Core.Imports.CsvEngine(),
                new C2.Core.Imports.FreeMindEngine()
            };
        }

        public abstract DocumentType DocumentType { get; }

        public abstract Document ImportFile(string filename);
    }
}
