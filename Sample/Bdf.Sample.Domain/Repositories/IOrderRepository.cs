﻿using System;
using Bdf.Domain.Repositories;
using Bdf.Sample.Domain.Model;

namespace Bdf.Sample.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
         
    }
}