using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Citta_T1.Model.Documents;
using Citta_T1.Model.MindMaps;

namespace Citta_T1.Core.Imports
{
    class FreeMindEngine : DocumentImportEngine
    {
        public override DocumentType DocumentType
        {
            get { return DocumentType.FreeMind; }
        }

        public override Document ImportFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            return FreeMindFile.LoadFile(filename);
        }
    }
}
