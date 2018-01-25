using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MofDoc.Class
{
    public class Branch
    {
        public Branch() { }

        public Branch(decimal BrId, string Name, decimal ParentId)
        {
            this.BrId = BrId;
            this.Name = Name;
            this.ParentId = ParentId;
        }

        public decimal BrId { get; set; }
        public string Name { get; set; }
        public decimal ParentId { get; set; }
    }
}