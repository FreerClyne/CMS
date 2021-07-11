﻿using CMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CMS.DAL.Repository.Interfaces
{
    public interface IRegisterRequestRepository
    {
        Task<RegisterRequest> AddAsync(RegisterRequest RegisterRequest);
        Task<List<RegisterRequest>> FilterAsync(Expression<Func<RegisterRequest, bool>> predicate);
        Task ChangeRegisterRequestStatusAsync(int requestId, string status);
    }
}