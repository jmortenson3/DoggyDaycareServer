﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Organizations
{
    public interface IOrganizationRepository
    {
        void Add(Organization organization);
        Task<List<Organization>> FindAllWhere(Expression<Func<Organization, bool>> filter);
        Task<List<Organization>> FindAll();
        Task<Organization> Find(int id);
        Task Save();
    }
}
