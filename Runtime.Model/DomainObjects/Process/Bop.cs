using System;
using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Product;

namespace IIMes.Services.Runtime.Model.Process
{
    public class Bop : DomainObject, IAggregateRoot
    {
        public Bop(string wo)
        {
            Wo = wo;
        }

        public virtual string Wo { get; set; }

        public IList<BopProcessBom> BopBom { get; set; }
        public IList<WoBom> WoBom { get; set; }
        public WoFs Fs { get; set; }
        public Workflow Workflow { get; set; }
    }
}
