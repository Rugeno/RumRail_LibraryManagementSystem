using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RR_LibraryManagementSystem.DataAccess.Domain
{
    public class BookDetails
    {
        public int Id { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public bool Status { get; set; }
        
        [Required]
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; }

        public string CreatedBy { get; set; }
    }

   

    public class BookingDetail
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public string BookType { get; set; }
        public int NoOfDays { get; set; }
        public int RequestUser { get; set; }
        public int BookId { get; set; }
        public bool Status { get; set; }
    }

    public class BookingDetailList
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public string BookType { get; set; }
        public int NoOfDays { get; set; }
        public string RequestUser { get; set; }
        public string PhoneNo { get; set; }
        public string BookName { get; set; }
        public bool Status { get; set; }
        public bool Cancelled { get; set; }
    }

    public class Report
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Amount { get; set; }
        public string BookType { get; set; }
        public int NoOfDays { get; set; }
        public string RequestUser { get; set; }
        public string PhoneNo { get; set; }
        public string BookName { get; set; }
        public bool Status { get; set; }
        public bool Cancelled { get; set; }


    }
}

