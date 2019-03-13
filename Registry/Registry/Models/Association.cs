using System.Collections.Generic;

namespace Registry
{
    public class Association
    {
        public long AssociationId { get; set; }
        public string AssociationName { get; set; }
        public string TaxNumber { get; set; }
        public int Goodwill { get; set; }

        public List<AssociationAddress> AssociationAddress { get; set; }
    }
}
