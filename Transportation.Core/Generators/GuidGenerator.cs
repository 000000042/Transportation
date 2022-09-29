using System;
using System.Collections.Generic;
using System.Text;

namespace Transportation.Core.Generators
{
    public class GuidGenerator
    {
        public static string GuidGenerate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
