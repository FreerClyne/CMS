﻿using CMS.DAL.Models;
using CMS.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.BL.Services.Interface
{
    public interface IUserService
    {
        bool AuthenticateUser(string email, string passWord);
        Task AddUser(User user);
        IEnumerable<User> GetAssignedReviewersByPaper(int paperId);
        int GetMaxUserId();
        IEnumerable<User> GetReviewers(int conferenceId);
        IEnumerable<UserRoleModel> GetUsersWithRole();
        Task UpdateUser(string Name, string Email, string Contact, string oldPasswrd, string newPasswrd);
    }
}