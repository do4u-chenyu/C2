using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business
{
    class ModelDocumentDao
    {
        private List<ModelDocument> modelDocuments;
        private ModelDocument currentDocument;
        public void ProcessNewModelDocument(object sender)
        {
            AddDocument();
        }

        private void AddDocument()
        {
            ModelDocument modelDocument = new ModelDocument("need1", "need2");
            //modelDocuments.Add(modelDocument);
        }
    }
}
