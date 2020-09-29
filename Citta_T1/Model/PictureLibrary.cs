using System;
using System.Collections;
using System.Collections.Generic;

namespace Citta_T1.Model
{
    public class PictureLibrary : Dictionary<string, Picture>
    {
        public PictureLibrary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
