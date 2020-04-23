using System;
using System.Collections.Generic;
using System.Text;

namespace OPS.BOL
{
    public enum EnqueteDestinataireResult
    {
        Initialise = 0,
        MailEnqueteTransmis = 1,
        AdresseMailInexistante = 2,
        AdresseMailNonConforme = 3,
        ReponsePartielle = 4,
        ReponseComplete = 5,
        ErreurTransmissionMail = 7
    }
}
