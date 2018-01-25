using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MofDoc.Class
{
    public class DateType
    {
        public DateType() { }
        public DateType(decimal Id, string Name, int Day)
        {
            this.Id = Id;
            this.Name = Name;
            this.Day = Day;
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public int Day { get; set; }
    }
}