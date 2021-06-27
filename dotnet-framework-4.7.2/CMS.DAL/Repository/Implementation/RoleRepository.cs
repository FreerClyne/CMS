﻿using CMS.DAL.Models;
using CMS.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CMS.DAL.Repository.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CMSContext _context;

        public RoleRepository(CMSContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> Filter(Expression<Func<Role, bool>> predicate)
        {
            return _context.Roles.Where(predicate).ToList();
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.ToList();
        }
    }
}
