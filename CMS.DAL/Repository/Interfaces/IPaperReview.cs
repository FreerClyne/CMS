﻿using CMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DAL.Repository.Interfaces
{
    public interface IPaperReviewRepository
    {
        IEnumerable<PaperReview> GetAll();
        IEnumerable<PaperReview> Filter(Expression<Func<PaperReview, bool>> predicate);
        void Add(PaperReview PaperReview);
        void Delete(PaperReview paperReview);
        void Update(PaperReview paperReview);
    }
}
