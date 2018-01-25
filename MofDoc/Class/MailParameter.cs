using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MofDoc.Class
{
    public class MailParameter
    {
        public MailParameter() {}

        public MailParameter(string DomainId, string RegNum, string ControlNum, string DocDate, string DocNum,
            string FromWhere, string FromWho, string FilePath, string ReturnDate, string ShortDesc, string ExpiredDays)
        {
            this.DomainId = DomainId;
            this.RegNum = RegNum;
            this.ControlNum = ControlNum;
            this.DocDate = DocDate;
            this.DocNum = DocNum;
            this.FromWhere = FromWhere;
            this.FromWho = FromWho;
            this.FilePath = FilePath;
            this.ReturnDate = ReturnDate;
            this.ShortDesc = ShortDesc;
            this.ExpiredDays = ExpiredDays;
        }

        public MailParameter(string DomainId, string RegNum, string ControlNum, string DocDate, string DocNum, 
            string FromWhere, string FromWho, string FilePath, string ReturnDate, string ShortDesc)
        {
            this.DomainId = DomainId;
            this.RegNum = RegNum;
            this.ControlNum = ControlNum;
            this.DocDate = DocDate;
            this.DocNum = DocNum;
            this.FromWhere = FromWhere;
            this.FromWho = FromWho;
            this.FilePath = FilePath;
            this.ReturnDate = ReturnDate;
            this.ShortDesc = ShortDesc;
        }

        public MailParameter(string DomainId, string RegNum, string ControlNum, string DocDate, string DocNum,
            string FromWhere, string FromWho, string FilePath, string ReturnDate, string ShortDesc, string toWhoFirst, string toWhoSecond)
        {
            this.DomainId = DomainId;
            this.RegNum = RegNum;
            this.ControlNum = ControlNum;
            this.DocDate = DocDate;
            this.DocNum = DocNum;
            this.FromWhere = FromWhere;
            this.FromWho = FromWho;
            this.FilePath = FilePath;
            this.ReturnDate = ReturnDate;
            this.ShortDesc = ShortDesc;
            this.toWhoFirst = toWhoFirst;
            this.toWhoSecond = toWhoSecond;
        }

        public string DomainId { get; set; }
        public string RegNum { get; set; }
        public string ControlNum { get; set; }
        public string DocDate { get; set; }
        public string DocNum { get; set; }
        public string FromWhere { get; set; }
        public string FromWho { get; set; }
        public string FilePath { get; set; }
        public string ReturnDate { get; set; }
        public string ShortDesc { get; set; }
        public string ExpiredDays { get; set; }
        public string toWhoFirst { get; set; }
        public string toWhoSecond { get; set; }
    }
}