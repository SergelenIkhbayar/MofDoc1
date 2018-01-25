using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MofDoc.Class
{
    public class Permission
    {
        public Permission() { }
        public Permission(decimal Id, bool Add, bool Edit, bool Delete, bool Transfer, bool View)
        {
            this.Id = Id;
            this.Add = Add;
            this.Edit = Edit;
            this.Delete = Delete;
            this.Transfer = Transfer;
            this.View = View;
        }

        public decimal Id;
        public bool Add;
        public bool Edit;
        public bool Delete;
        public bool Transfer;
        public bool View;
    }
}