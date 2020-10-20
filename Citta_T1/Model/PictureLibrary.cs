using System;
using System.Collections;
using System.Collections.Generic;

namespace C2.Model
{
    public class PictureLibrary : Dictionary<string, Picture>
    {
        public PictureLibrary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
