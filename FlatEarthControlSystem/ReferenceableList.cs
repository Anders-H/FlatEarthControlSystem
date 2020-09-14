using System.Collections.Generic;

namespace FlatEarthControlSystem
{
    public class ReferenceableList<T> : List<Referenceable>
    {
        public string EnumerationText
        {
            get { return ""; }
        }
    }
}