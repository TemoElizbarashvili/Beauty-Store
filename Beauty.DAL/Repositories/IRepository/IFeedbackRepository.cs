using Beauty.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.Repositories.IRepository
{
    public interface IFeedbackRepository
    {
        public IQueryable<Feedback> List();
        public Task<Feedback> GetByIdAsync(int id);
        public Task CreateFeedback(Feedback fb);
        public Task DeleteFeedback(int id);
        public Task EditFeedback(Feedback fb);
    }
}
