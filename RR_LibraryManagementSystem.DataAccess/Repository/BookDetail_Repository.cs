using System;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using RR_LibraryManagementSystem.DataAccess.DbConn;
using RR_LibraryManagementSystem.DataAccess.Domain;
using System.Data;
using RR_LibraryManagementSystem.DataAccess.Interface;

namespace RR_LibraryManagementSystem.DataAccess.Repository
{
	public class BookDetail_Repository: IBookDetail
	{
        private readonly ConnectionStrings _connection;

        public BookDetail_Repository(IOptions<ConnectionStrings> connection)
        {
            _connection = connection.Value;
        }

        public string DeleteBookDetail(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConnection))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", id);
                    string output = conn.ExecuteScalar<string>("USP_DeleteCarDetails", param, commandType: CommandType.StoredProcedure);
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<BookDetails> GetActiveBookList()
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConnection))
                {
                    IEnumerable<BookDetails> output = conn.Query<BookDetails>("USP_GetActiveBookList", commandType: CommandType.StoredProcedure);
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

   



       

        public BookDetails GetBookDetailsById(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConnection))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", id);
                    BookDetails output = conn.QueryFirstOrDefault<BookDetails>("USP_GetBookDetailById", param, commandType: CommandType.StoredProcedure);
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<BookDetails> GetBookList()
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConnection))
                {
                    IEnumerable<BookDetails> output = conn.Query<BookDetails>("USP_GetBookList", commandType: CommandType.StoredProcedure);
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

     

        public string SaveBookDetail(BookDetails obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConnection))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@BookName", obj.BookName);
                    param.Add("@Author", obj.Author);
                    param.Add("@CreatedBy", obj.CreatedBy);
                    param.Add("@Description", obj.Description);
                    param.Add("@Stock", obj.Stock);
                    string output = conn.ExecuteScalar<string>("USP_SaveBookDetails", param, commandType: CommandType.StoredProcedure);
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string UpdateBookDetail(BookDetails obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConnection))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", obj.Id);
                    param.Add("@BookName", obj.BookName);
                    param.Add("@Author", obj.Author);
                    param.Add("@Status", obj.Status);
                    param.Add("@Description", obj.Description);
                    param.Add("@Stock", obj.Stock);
                    string output = conn.ExecuteScalar<string>("USP_UpdateBookDetails", param, commandType: CommandType.StoredProcedure);
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

 
  

   
    }
}


