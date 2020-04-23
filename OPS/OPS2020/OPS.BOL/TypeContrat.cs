using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class TypeContrat
    {
        public TypeContrat()
        {
            Contrat = new HashSet<Contrat>();
        }

        public int IdTypeContrat { get; set; }
        public string DesignationTypeContrat { get; set; }

        public virtual ICollection<Contrat> Contrat { get; set; }
    }
}
