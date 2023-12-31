﻿using Beauty.DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public IFeedbackRepository FeedbackRepository { get; }
        public IShoppingCartRepository ShoppingCartRepository { get; }
        public IOrderRepository OrderRepository { get; }
    }
}
