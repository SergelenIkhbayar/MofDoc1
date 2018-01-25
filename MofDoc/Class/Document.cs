using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MofDoc.Class
{
    public class Document
    {
        public Document() { }

        public Document(string PkId, string ParentPkId, string RegNum, string RegDate, string DocNum, string DocDate, string ControlNum, string ControlDirection,
            string StaffId, string Name, string ToStaffId, string ToName, string ToBrId, string Branch, string LocationPkId, string Location, string OrganizationTypePkId, string OrganizationName,
            string InFromWho, string DocNotePkId, string DocNoteType, string ShortDesc, string Status, string IsReplyDoc, string ScannedFile, string PageNum, string CreatedDate,
            string ReturnDate, string ClosedDate, string ExpiredDays)
        {
            this.PkId = decimal.Parse(PkId);
            this.ParentPkId = string.IsNullOrEmpty(ParentPkId) ? null : (decimal?)decimal.Parse(ParentPkId);

            this.RegNum = RegNum;
            this.RegDate = Convert.ToDateTime(RegDate);

            this.DocNum = DocNum;
            if (!string.IsNullOrEmpty(DocDate))
                this.DocDate = Convert.ToDateTime(DocDate);

            this.ControlNum = string.IsNullOrEmpty(ControlNum) ? null : (decimal?)decimal.Parse(ControlNum);
            this.ControlDirection = ControlDirection;

            this.StaffId = decimal.Parse(StaffId);
            this.Name = Name;

            this.ToStaffId = string.IsNullOrEmpty(ToStaffId) ? null : (decimal?)decimal.Parse(ToStaffId);
            this.ToName = ToName;
            this.ToBrId = decimal.Parse(ToBrId);
            this.Branch = Branch;

            this.LocationPkId = string.IsNullOrEmpty(LocationPkId) ? null : (decimal?)decimal.Parse(LocationPkId);
            this.Location = Location;
            this.OrganizationTypePkId = string.IsNullOrEmpty(OrganizationTypePkId) ? null : (decimal?)decimal.Parse(OrganizationTypePkId);
            this.OrganizationName = OrganizationName;
            this.InFromWho = InFromWho;

            this.DocNotePkId = string.IsNullOrEmpty(DocNotePkId) ? null : (decimal?)decimal.Parse(DocNotePkId);
            this.DocNoteType = DocNoteType;

            this.ShortDesc = ShortDesc;
            this.Status = Status.Equals("Y") ? true : false; ;
            this.IsReplyDoc = IsReplyDoc.Equals("Y") ? true : false; ;

            this.ScannedFile = ScannedFile;
            this.PageNum = string.IsNullOrEmpty(PageNum) ? null : (int?)int.Parse(PageNum);

            this.CreatedDate = string.IsNullOrEmpty(CreatedDate) ? null : (DateTime?)Convert.ToDateTime(CreatedDate);
            this.ReturnDate = string.IsNullOrEmpty(ReturnDate) ? null : (DateTime?)Convert.ToDateTime(ReturnDate);
            this.ClosedDate = string.IsNullOrEmpty(ClosedDate) ? null : (DateTime?)Convert.ToDateTime(ClosedDate);
            this.ExpiredDays = ExpiredDays;
        }

        public Document(string PkId, string ParentPkId, string RegNum, string RegDate, string DocNum, string DocDate, string ControlNum, string ControlDirection,
            string StaffId, string Name, string ToStaffId, string ToName, string ToBrId, string Branch, string LocationPkId, string Location, string OrganizationTypePkId, string OrganizationName,
            string InFromWho, string DocNotePkId, string DocNoteType, string ShortDesc, string Status, string IsReplyDoc, string ScannedFile, string PageNum, string CreatedDate,
            string ReturnDate, string ClosedDate)
        {
            this.PkId = decimal.Parse(PkId);
            this.ParentPkId = string.IsNullOrEmpty(ParentPkId) ? null : (decimal?)decimal.Parse(ParentPkId);

            this.RegNum = RegNum;
            this.RegDate = Convert.ToDateTime(RegDate);

            this.DocNum = DocNum;
            if (!string.IsNullOrEmpty(DocDate))
                this.DocDate = Convert.ToDateTime(DocDate);

            this.ControlNum = string.IsNullOrEmpty(ControlNum) ? null : (decimal?)decimal.Parse(ControlNum);
            this.ControlDirection = ControlDirection;

            this.StaffId = decimal.Parse(StaffId);
            this.Name = Name;

            this.ToStaffId = string.IsNullOrEmpty(ToStaffId) ? null : (decimal?)decimal.Parse(ToStaffId);
            this.ToName = ToName;
            this.ToBrId = decimal.Parse(ToBrId);
            this.Branch = Branch;

            this.LocationPkId = string.IsNullOrEmpty(LocationPkId) ? null : (decimal?)decimal.Parse(LocationPkId);
            this.Location = Location;
            this.OrganizationTypePkId = string.IsNullOrEmpty(OrganizationTypePkId) ? null : (decimal?)decimal.Parse(OrganizationTypePkId);
            this.OrganizationName = OrganizationName;
            this.InFromWho = InFromWho;

            this.DocNotePkId = string.IsNullOrEmpty(DocNotePkId) ? null : (decimal?)decimal.Parse(DocNotePkId);
            this.DocNoteType = DocNoteType;

            this.ShortDesc = ShortDesc;
            this.Status = Status.Equals("Y") ? true : false; ;
            this.IsReplyDoc = IsReplyDoc.Equals("Y") ? true : false; ;

            this.ScannedFile = ScannedFile;
            this.PageNum = string.IsNullOrEmpty(PageNum) ? null : (int?)int.Parse(PageNum);

            this.CreatedDate = string.IsNullOrEmpty(CreatedDate) ? null : (DateTime?)Convert.ToDateTime(CreatedDate);
            this.ReturnDate = string.IsNullOrEmpty(ReturnDate) ? null : (DateTime?)Convert.ToDateTime(ReturnDate);
            this.ClosedDate = string.IsNullOrEmpty(ClosedDate) ? null : (DateTime?)Convert.ToDateTime(ClosedDate);
        }

        public Document(decimal PkId, decimal? ParentPkId, string RegNum, DateTime RegDate, string DocNum, DateTime DocDate, decimal? ControlNum, string ControlDirection,
            decimal StaffId, string Name, decimal? ToStaffId, string ToName, string Branch, decimal? OrganizationTypePkId, string Location, string OrganizationName, string InFromWho, 
            decimal? DocNotePkId, string DocNoteType, string ShortDesc, bool Status, bool IsReplyDoc, string ScannedFile, int? PageNum, DateTime? CreatedDate, DateTime? ReturnDate, 
            DateTime? ClosedDate)
        {
            this.PkId = PkId;
            this.ParentPkId = ParentPkId;

            this.RegNum = RegNum;
            this.RegDate = RegDate;

            this.DocNum = DocNum;
            this.DocDate = DocDate;

            this.ControlNum = ControlNum;
            this.ControlDirection = ControlDirection;

            this.StaffId = StaffId;
            this.Name = Name;

            this.ToStaffId = ToStaffId;
            this.ToName = ToName;
            this.Branch = Branch;

            this.OrganizationTypePkId = OrganizationTypePkId;
            this.Location = Location;
            this.OrganizationName = OrganizationName;
            this.InFromWho = InFromWho;

            this.DocNotePkId = DocNotePkId;
            this.DocNoteType = DocNoteType;

            this.ShortDesc = ShortDesc;
            this.Status = Status;
            this.IsReplyDoc = IsReplyDoc;

            this.ScannedFile = ScannedFile;
            this.PageNum = PageNum;

            this.CreatedDate = CreatedDate;
            this.ReturnDate = ReturnDate;
            this.ClosedDate = ClosedDate;
        }

        public decimal PkId { get; set; }
        public decimal? ParentPkId { get; set; }

        public string RegNum { get; set; }
        public DateTime RegDate { get; set; }

        public string DocNum { get; set; }
        public DateTime? DocDate { get; set; }

        public decimal? ControlNum { get; set; }
        public string ControlDirection { get; set; }

        public decimal StaffId { get; set; }
        public string Name { get; set; }

        public decimal? ToStaffId { get; set; }
        public string ToName { get; set; }
        public decimal ToBrId { get; set; }
        public string Branch { get; set; }

        public decimal? LocationPkId { get; set; }
        public string Location { get; set; }
        public decimal? OrganizationTypePkId { get; set; }
        public string OrganizationName { get; set; }
        public string InFromWho { get; set; }

        public decimal? DocNotePkId { get; set; }
        public string DocNoteType { get; set; }

        public string ShortDesc { get; set; }
        public bool Status { get; set; }
        public bool IsReplyDoc { get; set; }

        public string ScannedFile { get; set; }
        public int? PageNum { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string ExpiredDays { get; set; }
    }
}