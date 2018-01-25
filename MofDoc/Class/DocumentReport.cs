using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MofDoc.Class
{
    public class DocumentReport
    {
        public DocumentReport() { }

        public DocumentReport(string Name, bool IsBranch, decimal Card, decimal Direct, decimal Income, decimal CardDecision,
            decimal ExpiredDecision, decimal DirectDecision, decimal Decision, decimal ProgressTime, decimal ProgressNonTime)
        {
            this.Name = Name;
            this.IsBranch = IsBranch;
            this.Card = Card;
            this.Direct = Direct;
            this.Income = Income;
            this.CardDecision = CardDecision;
            this.ExpiredDecision = ExpiredDecision;
            this.DirectDecision = DirectDecision;
            this.Decision = Decision;
            this.ProgressTime = ProgressTime;
            this.ProgressNonTime = ProgressNonTime;
        }

        public string Name { get; set; }
        public bool? IsBranch { get; set; }
        public decimal Card { get; set; }
        public decimal Direct { get; set; }
        public decimal Income { get; set; }

        public decimal CardDecision { get; set; }
        public decimal ExpiredDecision { get; set; }
        public decimal DirectDecision { get; set; }
        public decimal Decision { get; set; }

        public decimal ProgressTime { get; set; }
        public decimal ProgressNonTime { get; set; }
    }
}