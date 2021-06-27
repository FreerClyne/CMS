﻿using CMS.DAL.Core;
using CMS.DAL.Models;
using CMS.BL.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.BL.Services.Implementation
{
    public class KeywordService : IKeywordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationStrategy _applicationStrategy;

        public KeywordService(IUnitOfWork unitOfWork, IApplicationStrategy applicationStrategy)
        {
            _unitOfWork = unitOfWork;
            _applicationStrategy = applicationStrategy;
        }

        public IEnumerable<Keyword> GetKeyWords()
        {
            return _unitOfWork.KeywordRepository.GetAll();
        }

        public IEnumerable<Expertise> GetExpertiseByUser(int UserId)
        {
            return _unitOfWork.ExpertiseRepository.Filter(x => x.UserId == UserId);
        }

        public async Task UpdateExpertise(int UserId, List<Keyword> keywordsToRemove, List<Keyword> KeywordsToAdd)
        {
            if (!keywordsToRemove.Any() && !KeywordsToAdd.Any())
            {
                throw new Exception();
            }

            var removedKeywords = keywordsToRemove.ToList();

            // remove keyword that exists in both add list and remove list
            foreach (var ak in KeywordsToAdd)
            {
                var keywordToDelete = keywordsToRemove.SingleOrDefault(x => x.Id == ak.Id);
                if (keywordToDelete != null)
                    keywordsToRemove.Remove(keywordToDelete);
            }

            var expertises = GetExpertiseByUser(UserId);

            // remove keywords
            if (keywordsToRemove.Count != 0)
            {
                foreach (var rk in keywordsToRemove)
                {
                    var expertise = expertises.SingleOrDefault(x => x.KeywordId == rk.Id);
                    if (expertise != null)
                        _unitOfWork.ExpertiseRepository.Delete(expertise);
                }
            }

            // add new keywords
            foreach (var ak in KeywordsToAdd)
            {
                if (expertises.Any(x => x.KeywordId == ak.Id))
                    continue;

                _unitOfWork.ExpertiseRepository.Add(new Expertise
                {
                    KeywordId = ak.Id,
                    UserId = _applicationStrategy.GetLoggedInUserInfo().User.Id
                });
            }

            await _unitOfWork.Save();
        }
    }
}
