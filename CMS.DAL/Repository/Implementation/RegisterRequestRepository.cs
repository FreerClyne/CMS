﻿using CMS.DAL.Models;
using CMS.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DAL.Repository.Implementation
{
    public class RegisterRequestRepository : IRegisterRequestRepository
    {
        private readonly CMSDBEntities _context;

        public RegisterRequestRepository(CMSDBEntities context)
        {
            _context = context;
        }

        public void Add(RegisterRequest RegisterRequest)
        {
            _context.RegisterRequests.Add(RegisterRequest);
        }

        public IEnumerable<RegisterRequest> Filter(Expression<Func<RegisterRequest, bool>> predicate)
        {
            return _context.RegisterRequests
                .Include(x => x.Conference)
                .Include(x => x.Role)
                .Where(predicate)
                .ToList();
        }

        public IEnumerable<RegisterRequest> GetAll()
        {
            return _context.RegisterRequests
                .Include(x => x.Conference)
                .Include(x => x.Role)
                .ToList();
        }

        public void Delete(RegisterRequest RegisterRequest)
        {
            _context.RegisterRequests.Remove(RegisterRequest);
        }

        public void Update(RegisterRequest RegisterRequest)
        {
            _context.Entry(RegisterRequest).State = EntityState.Modified;
        }
    }
}
