using Beauty.DAL.Context;
using Beauty.DAL.Repositories.IRepository;
using Beauty.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private StoreDbContext _db;
        public FeedbackRepository(StoreDbContext db)
        {
            _db = db;
        }

        public Task CreateFeedback(Feedback fb)
        {
            _db.Feedbacks.Add(fb);
            _db.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task DeleteFeedback(int id)
        {
            var fbToDelete = _db.Feedbacks.Where(fb => fb.FeedbackId == id).FirstOrDefault();
            _db.Remove(fbToDelete);
            _db.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task EditFeedback(Feedback fb)
        {
            var fbToEdit = _db.Feedbacks.Where(fb => fb.FeedbackId == fb.FeedbackId).FirstOrDefault();
            fbToEdit.Author = fb.Author;
            fbToEdit.Title = fb.Title;
            fbToEdit.Description = fb.Description;
            fbToEdit.ImageUrl = fb.ImageUrl;
            fbToEdit.Rate = fb.Rate;
            return Task.CompletedTask;
        }

        public async Task<Feedback> GetByIdAsync(int id)
        {
            return await Task.FromResult(_db.Feedbacks.Where(fb => fb.FeedbackId == id).FirstOrDefault());
        }

        public IQueryable<Feedback> List()
        {
            return _db.Feedbacks;
        }
    }
}
