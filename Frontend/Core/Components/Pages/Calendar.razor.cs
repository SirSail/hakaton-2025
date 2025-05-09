using Core.API;
using Core.API.Requests;
using Core.API.Responses;
using Core.API.Services;
using Core.Components.BaseClassess;
using Core.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Globalization;

namespace Core.Components.Pages
{
    public partial class Calendar : CustomComponentBase
    {
        [Inject] private ApiService ApiService { get; set; }
        [Inject] private IJSRuntime JS { get; set; }

        private List<CalendarItemResponse> CalendarItems { get; set; } = [];
        private DateOnly SelectedDay { get; set; }
        private DateSpan DisplayedDateSpan { get; set; }
        private ElementReference DayScrollRef { get; set; }
        private bool IsDragging { get; set; } = false;
        private double DragStartX { get; set; }
        private double ScrollStartX { get; set; }

        private int SelectedMonth { get; set; } = DateTime.Now.Month;
        private Dictionary<int, string> Months { get; set; } = new Dictionary<int, string>
        {
            { 1, "Styczeń" }, { 2, "Luty" }, { 3, "Marzec" }, { 4, "Kwiecień" },
            { 5, "Maj" }, { 6, "Czerwiec" }, { 7, "Lipiec" }, { 8, "Sierpień" },
            { 9, "Wrzesień" }, { 10, "Październik" }, { 11, "Listopad" }, { 12, "Grudzień" }
        };

        private bool ShouldScrollToSelectedDay { get; set; } = false;
        private bool IsDataLoaded { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (IsLogged)
            {
                await PrepareCalendar();
                await LoadData();
                ShouldScrollToSelectedDay = true;
                IsDataLoaded = true;
            }
            else
            {
                NavigationManager.NavigateTo("/login", true);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("scrollDayIntoView", $"day-{SelectedDay:yyyy-MM-dd}");
            }

            if (ShouldScrollToSelectedDay)
            {
                ShouldScrollToSelectedDay = false;
                await JS.InvokeVoidAsync("scrollDayIntoView", $"day-{SelectedDay:yyyy-MM-dd}");
            }
        }

        private Task PrepareCalendar()
        {
            var now = DateTime.Now;
            var start = new DateOnly(now.Year, now.Month, 1);
            var end = new DateOnly(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            SelectedDay = DateOnly.FromDateTime(now);
            DisplayedDateSpan = new DateSpan(start, end);
            return Task.CompletedTask;
        }

        private async Task LoadData()
        {
            CalendarItemRequest request = new CalendarItemRequest
            {
                StartDate = DisplayedDateSpan.Start,
                EndDate = DisplayedDateSpan.End
            };

            var apiResponse = await ApiService.PostWithResultAsync<CalendarItemRequest, List<CalendarItemResponse>>("api/v1/calendar-items", request);

            if (apiResponse.IsSuccess)
            {
                CalendarItems = apiResponse.Data;
            }
        }

        private async Task RefreshData()
        {
            IsDataLoaded = false;
            await LoadData();
            IsDataLoaded = true;
        }

        

        private string GetDayString(DateOnly date) => $"{date.Day} {date.GetShortDayWeekName()}";

        private Task SetSelectedDay(DateOnly date)
        {
            SelectedDay = date;

            return Task.CompletedTask;
        }

        private async Task MonthChanged(int newMonth)
        {
            SelectedMonth = newMonth;

            int year = DateTime.Now.Year;
            int daysInSelectedMonth = DateTime.DaysInMonth(year, SelectedMonth);

            var start = new DateOnly(year, SelectedMonth, 1);
            var end = new DateOnly(year, SelectedMonth, daysInSelectedMonth);

            DisplayedDateSpan = new DateSpan(start, end);
            SelectedDay = new DateOnly(year, SelectedMonth, Math.Min(daysInSelectedMonth, DateTime.Now.Day));

            await RefreshData();
            ShouldScrollToSelectedDay = true;
        }

        private async void StartDrag(MouseEventArgs e)
        {
            IsDragging = true;
            DragStartX = e.ClientX;
            ScrollStartX = await JS.InvokeAsync<double>("getScrollLeft", "calendar-scroll-days");
        }

        private async void DragDays(MouseEventArgs e)
        {
            if (!IsDragging) return;
            var diff = DragStartX - e.ClientX;
            await JS.InvokeVoidAsync("setScrollLeft", "calendar-scroll-days", ScrollStartX + diff);
        }

        private void EndDrag(MouseEventArgs e) => IsDragging = false;

        private class DateSpan
        {
            public DateOnly Start { get; private set; }
            public DateOnly End { get; private set; }

            public DateSpan(DateOnly start, DateOnly end)
            {
                Start = start;
                End = end;
            }

            public IEnumerable<DateOnly> GetDaysBeetween()
            {
                for (var date = Start; date <= End; date = date.AddDays(1))
                    yield return date;
            }
        }
    }
}
