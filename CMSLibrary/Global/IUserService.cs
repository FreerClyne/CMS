﻿using System.Collections.Generic;
using CMSLibrary.Model;

namespace CMSLibrary.Global
{
    public interface IUserService
    {
        void AddUser(User user);
        List<User> GetAssignedReviewersByPaper(int paperId);
        int GetMaxUserId();
        List<User> GetReviewers();
        List<User> GetReviewersByConference(int conferenceId);
        List<UserRoleModel> GetUserRole();
        List<User> GetUsers();
        void UpdateUser(string userName, string userEmail, string userContact, string oldPasswrd, string newPasswrd);
    }
}