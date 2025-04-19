using Domain.Models;
using Infrastructure.DatabaseAbstractions;

namespace Application.Calendar.Services
{
    public class CalendarService
    {
        private readonly UnitOfWork _unitOfWork;

        public CalendarService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<CalendarItem>> GetUserItemsFromDateSpan(int userId, DateOnly startDate, DateOnly endDate)
        {

            DateTime startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDateTime = endDate.ToDateTime(TimeOnly.MaxValue);

            IEnumerable<CalendarItem> calendarItems = _unitOfWork.CalendarItemRepository
                .FindAll(x => x.UserId == userId && x.Time < endDateTime && x.Time > startDateTime);
             

            return Task.FromResult(calendarItems);
        }
    }
}
