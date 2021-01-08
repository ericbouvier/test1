using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class BookRepository : Repository<Book>, IBooksRepository
    {
        public BookRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }
    }
}
