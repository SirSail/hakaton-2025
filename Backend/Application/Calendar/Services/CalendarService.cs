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

        public Task<CalendarItem> InsertCalendarItem(CalendarItem item)
        {
            _unitOfWork.CalendarItemRepository.Add(item);
            _unitOfWork.Commit();
            return Task.FromResult(item);
        }

        public Task<CalendarItem> GetCalendarItemById(int id)
        {
            return Task.FromResult(_unitOfWork.CalendarItemRepository.FindOrThrow(id));
        }

        public Task<IEnumerable<CalendarItem>> GetUserItemsFromDateSpan(int userId, DateOnly startDate, DateOnly endDate)
        {

            DateTime startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDateTime = endDate.ToDateTime(TimeOnly.MaxValue);

            IEnumerable<CalendarItem> calendarItems = _unitOfWork.CalendarItemRepository
                .FindAll(x => x.UserId == userId && x.Time < endDateTime && x.Time > startDateTime);


            return Task.FromResult(calendarItems);
        }

        public Task<IEnumerable<CalendarItem>> GetClosestItemsForUser(int userId, int numberOfItems)
        {
            IEnumerable<CalendarItem> calendarItems = _unitOfWork.CalendarItemRepository
                .FindAll(x => x.UserId == userId && x.Time > DateTime.Now)
                .OrderBy(x => x.Time)
                .Take(numberOfItems);

            return Task.FromResult(calendarItems);
        }

        public Task RemoveCalendarItemNotification(CalendarItem item)
        {
            item.NotificationTime = null;
            _unitOfWork.CalendarItemRepository.Update(item);
            _unitOfWork.Commit();

            return Task.CompletedTask;
        }

        public Task SetCalendarItemNotification(CalendarItem item, DateTime notificationTime)
        {
            item.NotificationTime = notificationTime;
            _unitOfWork.CalendarItemRepository.Update(item);
            _unitOfWork.Commit();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<CalendarItem>> GetCalendarItemWithNotificationTimeNow()
        {
            IEnumerable<CalendarItem> currentCalendarItems = _unitOfWork.CalendarItemRepository.FindAll
                        (
                            x => x.NotificationTime.HasValue && NormalizedDateTime(x.NotificationTime.Value) == NormalizedDateTime(DateTime.Now)
                        );

            return Task.FromResult(currentCalendarItems);
        }

        /// <summary>
        /// zwraca date time z wyzerowanymi sekundami i niżej tak aby powstał sam datetime z dniem, godzina i minutą (łatwe do porównywania)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DateTime NormalizedDateTime(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }
    }
}
