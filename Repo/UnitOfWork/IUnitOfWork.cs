using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
