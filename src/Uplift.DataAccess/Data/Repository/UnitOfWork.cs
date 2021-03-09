namespace Uplift.DataAccess.Data.Repository
{
    using System.Threading.Tasks;
    using Uplift.DataAccess.Data.Repository.IRepository;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            this.Category = new CategoryRepository(db);
            this.Frequency = new FrequencyRepository(db);
            this.Service = new ServiceRepository(db);
            this.OrderHeader = new OrderHeaderRepository(db);
            this.OrderDetails = new OrderDetailsRepository(db);
            this.User = new UserRepository(db);
            this.SP_Call = new SP_Call(db);
        }

        public ICategoryRepository Category { get; private set; }

        public IFrequencyRepository Frequency { get; private set; }
        
        public IServiceRepository Service { get; private set; }
        
        public IOrderHeaderRepository OrderHeader { get; private set; }
        
        public IOrderDetailsRepository OrderDetails { get; private set; }
        
        public IUserRepository User { get; private set; }
        
        public ISP_Call SP_Call { get; private set; }
        
        public void Dispose()
        {
            this.db.Dispose();
        }

        public void Save()
        {
            this.db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await this.db.SaveChangesAsync();
        }
    }
}
