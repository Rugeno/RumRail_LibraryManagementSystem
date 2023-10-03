using System;
using RR_LibraryManagementSystem.DataAccess.Domain;

namespace RR_LibraryManagementSystem.DataAccess.Interface
{
	public interface IBookDetail
	{
        IEnumerable<BookDetails> GetBookList();
        BookDetails GetBookDetailsById(int id);


        string SaveBookDetail(BookDetails obj);
        string UpdateBookDetail(BookDetails obj);
        string DeleteBookDetail(int id);


        IEnumerable<BookDetails> GetActiveBookList();

        IEnumerable<BookingDetailList> GetBookingDetails();
        IEnumerable<BookingDetailList> GetBookingDetailsOfUser(int id);

        string SaveBooking(BookingDetail obj);
        BookingDetailList GetBookingDetailById(int id);
        string VerifyBookingDetailById(int id);

        string ReturnBookingById(int id);


    }
}

