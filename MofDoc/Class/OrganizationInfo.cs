using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MofDoc.Class
{
    public class OrganizationInfo
    {
        public OrganizationInfo() { }

        public OrganizationInfo(decimal PkId, decimal ParentPkId, string Name)
        {
            this.PkId = PkId;
            this.ParentPkId = ParentPkId;
            this.Name = Name;
        }

        public decimal PkId { get; set; }
        public decimal ParentPkId { get; set; }
        public string Name { get; set; }
    }
}