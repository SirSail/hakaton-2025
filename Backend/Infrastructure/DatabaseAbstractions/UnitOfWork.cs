using Domain.Models;
using Infrastructure.Database;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DatabaseAbstractions
{
    public class UnitOfWork : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly ILogger<UnitOfWork> _logger;

        public IRepository<SystemUser> UserRepository => GetRepository<SystemUser>();
        public IRepository<PatientInfo> PatientInfoRepository => GetRepository<PatientInfo>();
        public IRepository<CalendarItem> CalendarItemRepository => GetRepository<CalendarItem>();   

        public UnitOfWork(AppDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Commit()
        {
            var trackedEntites = _context.ChangeTracker.Entries();
            var logMessages = trackedEntites
                .Select(entry => $"Dodano/zmieniono wpis w bazie danych [{entry.Entity.GetType()}] => id {entry.Members.First().CurrentValue.ToString()}")
                .ToList();

            _context.SaveChanges();
            logMessages.ForEach(message => _logger.LogInformation(message));

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }

            var repository = new Repository<T>(_context.Set<T>());
            _repositories.Add(typeof(T), repository);
            return repository;
        }
    }
}
