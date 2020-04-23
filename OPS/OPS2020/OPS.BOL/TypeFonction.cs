using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class TypeFonction
    {
        public TypeFonction()
        {
            CollaborateurAfpa = new HashSet<CollaborateurAfpa>();
        }

        public int IdFonction { get; set; }
        public string LibelleFonction { get; set; }

        public virtual ICollection<CollaborateurAfpa> CollaborateurAfpa { get; set; }
    }
}
