using System;
using RR_LibraryManagementSystem.DataAccess.Domain;

namespace RR_LibraryManagementSystem.DataAccess.Interface
{
	public interface IUserAuth
	{
        string CheckLogin(Login_VM obj);
        Portal_User GetUserData(Login_VM obj);

        string CheckEmailExist(string email);
        string SaveUserData(Save_PortalUser obj);
    }
}

