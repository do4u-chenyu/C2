using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using C2.Model.Documents;
using C2.Model.MindMaps;

namespace C2.Core.Imports
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
