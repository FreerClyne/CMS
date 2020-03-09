﻿using CMS.DAL.Repository.Interfaces;
using System.Threading.Tasks;

namespace CMS.DAL.Core
{
    /// <summary>
    /// Providers services access to repositories through a shared context
    /// </summary>
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IConferenceMemberRepository ConferenceMemberRepository { get; }
        IPaperReviewRepository PaperReviewRepository { get; }
        IConferenceRepository ConferenceRepository { get; }
        IConferenceTopicRepository ConferenceTopicRepository { get; }
        IKeywordRepository KeywordRepository { get; }
        IExpertiseRepository ExpertiseRepository { get; }
        IPaperRepository PaperRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        IPaperTopicRepository PaperTopicRepository { get; }

        /// <summary>
        /// Invokes SaveChangesAsync on shared context
        /// </summary>
        Task<int> Save();
    }
}