﻿using CMS.DAL.Models;
using CMS.DAL.Repository.Implementation;
using System;
using System.Threading.Tasks;

namespace CMS.DAL.Core
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly CMSDBEntities _context;
        private readonly UserRepository _userRepository;
        private readonly ConferenceMemberRepository _conferenceMemberRepository;
        private readonly PaperReviewRepository _paperReview;
        private readonly ConferenceRepository _conferenceRepository;
        private readonly ConferenceTopicRepository _conferenceTopicRepository;

        public UnitOfWork()
        {
            _context = new CMSDBEntities();
            _userRepository = new UserRepository(_context);
            _conferenceMemberRepository = new ConferenceMemberRepository(_context);
            _paperReview = new PaperReviewRepository(_context);
            _conferenceRepository = new ConferenceRepository(_context);
            _conferenceTopicRepository = new ConferenceTopicRepository(_context);
        }

        public UserRepository UserRepository => _userRepository;
        public ConferenceMemberRepository ConferenceMemberRepository => _conferenceMemberRepository;
        public PaperReviewRepository PaperReviewRepository => _paperReview;
        public ConferenceRepository ConferenceRepository => _conferenceRepository;
        public ConferenceTopicRepository ConferenceTopicRepository => _conferenceTopicRepository;

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
