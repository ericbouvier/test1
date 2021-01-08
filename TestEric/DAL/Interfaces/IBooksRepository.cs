using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IBooksRepository: IRepository<Book>
    {
    }
}
