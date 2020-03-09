﻿using CMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CMS.DAL.Repository.Interfaces;

namespace CMS.DAL.Repository.Implementation
{
    public class ConferenceTopicRepository : IConferenceTopicRepository
    {
        private readonly CMSDBEntities _context;

        public ConferenceTopicRepository(CMSDBEntities context)
        {
            _context = context;
        }

        public void Add(ConferenceTopic conferenceTopic)
        {
            _context.ConferenceTopics.Add(conferenceTopic);
        }

        public IEnumerable<ConferenceTopic> Filter(Expression<Func<ConferenceTopic, bool>> predicate)
        {
            return _context.ConferenceTopics
                .Include(x => x.Conference)
                .Include(x => x.keyword)
                .Where(predicate)
                .ToList();
        }

        public IEnumerable<ConferenceTopic> GetAll()
        {
            return _context.ConferenceTopics
                .Include(x => x.Conference)
                .Include(x => x.keyword)
                .ToList();
        }
    }
}
