using Core.API;
using Core.API.Requests;
using Core.API.Responses;
using Core.API.Services;
using Core.Components.BaseClassess;
using Core.Helpers;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Core.Components.Pages
{
    public partial class Calendar : CustomComponentBase
    {
        [Inject] private ApiService ApiService { get; set; }
        private List<CalendarItemResponse> CalendarItems = [];

        private DateOnly SelectedDay { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        private DateSpan DisplayedDateSpan { get; set; }

        private Dictionary<int, string> Months { get; set; } = new Dictionary<int, string>
        {
            { 1, "Styczeń" },
            { 2, "Luty" },
            { 3, "Marzec" },
            { 4, "Kwiecień" },
            { 5, "Maj" },
            { 6, "Czerwiec" },
            { 7, "Lipiec" },
            { 8, "Sierpień" },
            { 9, "Wrzesień" },
            { 10, "Październik" },
            { 11, "Listopad" },
            { 12, "Grudzień" }
        };

        private string Debug { get; set; } = string.Empty;
        private bool IsDataLoaded { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (IsLogged)
            {
                await PrepareCalendar();
                await LoadData();
                IsDataLoaded = true;
            }
            else
            {
                NavigationManager.NavigateTo("/login", true);
            }
        }

        private async Task LoadData()
        {

            CalendarItemRequest request = new CalendarItemRequest
            {
                StartDate = DisplayedDateSpan.Start,
                EndDate = DisplayedDateSpan.End
            };
            var apiResponse =
                await ApiService.PostWithResultAsync<CalendarItemRequest, List<CalendarItemResponse>>($"api/v1/calendar-items", request);


            if (!apiResponse.IsSuccess)
            {
                Debug = apiResponse.Error.Message;
                if (string.IsNullOrEmpty(Debug))
                {
                    Debug = $"BEZ WIADOMOSCI: {ApiService.Debug}";
                }
                return;
            }
            CalendarItems = apiResponse.Data;
        }

        private async Task RefreshData()
        {
            IsDataLoaded = false;
            await LoadData();
            IsDataLoaded = true;
        }

        private Task PrepareCalendar()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var start = DateOnly.FromDateTime(DateHelper.GetStartOfWeek(DateTime.Now));
            var end = DateOnly.FromDateTime(DateHelper.GetEndOfWeek(DateTime.Now));
            DisplayedDateSpan = new DateSpan(start, end);

            return Task.CompletedTask;
        }

        private string GetDayString(DateOnly date)
        {
            return $"{date.Day} {date.GetShortDayWeekName()}";
        }

        private async Task SetSelectedDay(DateOnly date)
        {
            var lastDate = SelectedDay;
            SelectedDay = date;
            if (!DisplayedDateSpan.GetDaysBeetween().Contains(lastDate))
            {
                await RefreshData();
            }

            StateHasChanged();
        }

        private async Task MonthChanged(ChangeEventArgs e)
        {
            string selectedMonthString = e.Value.ToString();
            if (int.TryParse(selectedMonthString, out int selectedMonth))
            {
                DisplayedDateSpan.ChangeMonth(selectedMonth);
                await SetSelectedDay(DisplayedDateSpan.Start.AddDays(2));
            }

            StateHasChanged();
        }

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
                if (End < Start)
                    yield break;

                for (var date = Start; date <= End; date = date.AddDays(1))
                {
                    yield return date;
                }
            }
            public void ChangeMonth(int monthNumber)
            {
                Start = AdjustDate(Start, monthNumber);
                End = AdjustDate(End, monthNumber);
            }
            private static DateOnly AdjustDate(DateOnly original, int newMonth)
            {
                int year = original.Year;


                int day = Math.Min(original.Day, DateTime.DaysInMonth(year, newMonth));
                return new DateOnly(year, newMonth, day);
            }
        }
    }
}