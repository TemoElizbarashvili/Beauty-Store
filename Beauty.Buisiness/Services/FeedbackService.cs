using Beauty.Buisiness.Services.IServices;
using Beauty.DAL.UnitOfWork;
using Beauty.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.Buisiness.Services
{
    public class FeedbackService : IFeedbackService
    {
        private IUnitOfWork _uow;

        public FeedbackService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task CreateFeedback(Feedback fb)
        {
            return _uow.FeedbackRepository.CreateFeedback(fb);
        }

        public Task DeleteFeedback(int id)
        {
            return _uow.FeedbackRepository.DeleteFeedback(id);
        }

        public Task EditFeedback(Feedback fb)
        {
            return _uow.FeedbackRepository.EditFeedback(fb);
        }

        public Task<Feedback> GetByIdAsync(int id)
        {
            return _uow.FeedbackRepository.GetByIdAsync(id);
        }

        public IQueryable<Feedback> List()
        {
            return _uow.FeedbackRepository.List();
        }
    }
}
