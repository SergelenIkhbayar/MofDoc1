using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MofDoc.Class
{
    public class IncomeDoc
    {
        public IncomeDoc() { }

        public IncomeDoc(string PkId, string RegNum, string RegDate, string DocNum, string DocDate, string ControlNum, string ControlDirection, string StaffId, string ToStaffId, string ToBrId, string DocNotePkId,
            string OrganizationTypePkId, string InFromWho, string IsReplyDoc, string ShortDesc, string PageNum, string CreatedDate, string ReturnDate, string ScannedFile)
        {
            this.PkId = decimal.Parse(PkId);
            this.RegNum = RegNum;
            this.RegDate = Convert.ToDateTime(RegDate);
            this.DocNum = DocNum;
            this.DocDate = Convert.ToDateTime(DocDate);
            this.ControlNum = ControlNum.ToString();
            this.ControlDirection = ControlDirection;
            this.StaffId = decimal.Parse(StaffId);
            this.ToStaffId = decimal.Parse(ToStaffId);
            this.ToBrId = decimal.Parse(ToBrId);
            this.DocNotePkId = decimal.Parse(DocNotePkId);
            this.OrganizationTypePkId = decimal.Parse(OrganizationTypePkId);
            this.InFromWho = InFromWho;
            this.IsReplyDoc = IsReplyDoc;
            this.ShortDesc = ShortDesc;
            this.PageNum = int.Parse(PageNum);
            this.CreatedDate = string.IsNullOrEmpty(CreatedDate) ? null : (DateTime?)Convert.ToDateTime(CreatedDate);
            this.ReturnDate = string.IsNullOrEmpty(ReturnDate) ? null : (DateTime?)Convert.ToDateTime(ReturnDate);
            this.ScannedFile = ScannedFile;
        }

        public IncomeDoc(string PkId, string RegNum, string RegDate, string DocNum, string DocDate, string ControlDirection, string StaffId, string ToStaffId, string ToBrId, string DocNotePkId,
            string OrganizationTypePkId, string InFromWho, string IsReplyDoc, string ShortDesc, string PageNum, string CreatedDate)
        {
            this.PkId = decimal.Parse(PkId);
            this.RegNum = RegNum;
            this.RegDate = Convert.ToDateTime(RegDate);
            this.DocNum = DocNum;
            this.DocDate = Convert.ToDateTime(DocDate);
            this.ControlDirection = ControlDirection;
            this.StaffId = decimal.Parse(StaffId);
            this.ToStaffId = decimal.Parse(ToStaffId);
            this.ToBrId = decimal.Parse(ToBrId);
            this.DocNotePkId = decimal.Parse(DocNotePkId);
            this.OrganizationTypePkId = decimal.Parse(OrganizationTypePkId);
            this.InFromWho = InFromWho;
            this.IsReplyDoc = IsReplyDoc;
            this.ShortDesc = ShortDesc;
            this.PageNum = int.Parse(PageNum);
            this.CreatedDate = Convert.ToDateTime(CreatedDate);
        }

        public IncomeDoc(string PkId, string ParentPkId, string RegNum, string RegDate, string DocNum, string DocDate, string ControlDirection, string StaffId, string ToStaffId, string ToBrId, string DocNotePkId,
            string OrganizationTypePkId, string InFromWho, string IsReplyDoc, string ShortDesc, string PageNum, string CreatedDate)
        {
            this.PkId = decimal.Parse(PkId);
            this.ParentPkId = decimal.Parse(ParentPkId);
            this.RegNum = RegNum;
            this.RegDate = Convert.ToDateTime(RegDate);
            this.DocNum = DocNum;
            this.DocDate = Convert.ToDateTime(DocDate);
            this.ControlDirection = ControlDirection;
            this.StaffId = decimal.Parse(StaffId);
            this.ToStaffId = decimal.Parse(ToStaffId);
            this.ToBrId = decimal.Parse(ToBrId);
            this.DocNotePkId = decimal.Parse(DocNotePkId);
            this.OrganizationTypePkId = decimal.Parse(OrganizationTypePkId);
            this.InFromWho = InFromWho;
            this.IsReplyDoc = IsReplyDoc;
            this.ShortDesc = ShortDesc;
            this.PageNum = int.Parse(PageNum);
            this.CreatedDate = Convert.ToDateTime(CreatedDate);
        }

        public decimal PkId { get; set; }
        public decimal? ParentPkId { get; set; }

        public string RegNum { get; set; }
        public DateTime RegDate { get; set; }

        public string DocNum { get; set; }
        public DateTime DocDate { get; set; }
        public string ControlNum { get; set; }
        public string ControlDirection { get; set; }

        public decimal StaffId { get; set; }
        public decimal? ToStaffId { get; set; }
        public decimal? ToBrId { get; set; }

        public decimal? DocNotePkId { get; set; }
        public decimal? OrganizationTypePkId { get; set; }
        public string InFromWho { get; set; }
        public string IsReplyDoc { get; set; }

        public string ShortDesc { get; set; }
        public string ScannedFile { get; set; }
        public int? PageNum { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}