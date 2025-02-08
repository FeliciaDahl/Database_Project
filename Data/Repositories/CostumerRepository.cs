
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CostumerRepository(DataContext context) : BaseRepository<CostumerEntity>(context), ICostumerRepository
{
}
