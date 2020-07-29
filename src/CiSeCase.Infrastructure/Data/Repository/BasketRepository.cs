using CiSeCase.Core.Interfaces.Repository;
using CiSeCase.Core.Models;
using CiSeCase.Infrastructure.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace CiSeCase.Infrastructure.Data.Repository
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(DbContext context) : base(context)
        {
        }
    }
}