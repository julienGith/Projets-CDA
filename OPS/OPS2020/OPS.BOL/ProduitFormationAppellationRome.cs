using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class ProduitFormationAppellationRome
    {
        public int CodeProduitFormation { get; set; }
        public string CodeRome { get; set; }

        public virtual ProduitFormation CodeProduitFormationNavigation { get; set; }
        public virtual Rome CodeRomeNavigation { get; set; }
    }
}
