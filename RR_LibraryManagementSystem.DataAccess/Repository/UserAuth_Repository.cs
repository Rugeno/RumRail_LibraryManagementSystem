using System;
using System.Data;
using Microsoft.Extensions.Options;
using RR_LibraryManagementSystem.DataAccess.DbConn;
using RR_LibraryManagementSystem.DataAccess.Domain;
using RR_LibraryManagementSystem.DataAccess.Interface;
using Dapper;
using Microsoft.Data.SqlClient;

namespace RR_LibraryManagementSystem.DataAccess.Repository
{
    public class UserAuth_Repository : IUserAuth
	{
        private readonly ConnectionStrings _connection;

        public UserAuth_Repository(IOptions<ConnectionStrings> connection)
        {
            _connection = connection.Value;
        }

        public string CheckEmailExist(string email)
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Email", email);
                    string output = conn.ExecuteScalar<string>("USP_CheckEmailExist", param, commandType: CommandType.StoredProcedure);
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string CheckLogin(Login_VM obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Email", obj.Email);
                    param.Add("@Password", obj.Password);
                    string output = conn.ExecuteScalar<string>("USP_CheckLogin", param, commandType: CommandType.StoredProcedure);
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Portal_User GetUserData(Login_VM obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Email", obj.Email);
                    param.Add("@Password", obj.Password);
                    Portal_User output = conn.QueryFirstOrDefault<Portal_User>("USP_GetUserDetail", param, commandType: CommandType.StoredProcedure);
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string SaveUserData(Save_PortalUser obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connection.DBConn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@FullName", obj.FullName);
                    param.Add("@Email", obj.Email);
                    param.Add("@Password", obj.Password);
                    param.Add("@PhoneNo", obj.PhoneNo);
                    param.Add("@RoleId", obj.Role);
                    string output = conn.ExecuteScalar<string>("USP_SaveUserDetails", param, commandType: CommandType.StoredProcedure);
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

