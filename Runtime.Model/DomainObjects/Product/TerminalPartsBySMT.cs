using System;
using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Process;

namespace IIMes.Services.Runtime.Model.Production
{
    public partial class TerminalPartsBySMT : TerminalParts
    {
        public string FeederType;

        public string Location;

        public int ItemCount;

        public int Qty;

        public List<TerminalPartsByBom> Values;
    }
}
