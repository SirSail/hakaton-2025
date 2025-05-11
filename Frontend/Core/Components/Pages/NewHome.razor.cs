using Core.API.Models;
using Core.API.Requests;
using Core.Components.BaseClassess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components.Pages
{
    public partial class NewHome : CustomComponentBase
    {
        private string UserName { get; set; } = string.Empty;


        private List<CalendarItemModel> ClosestCalendarItems { get; set; } = [];

        private string ErrorText { get; set; } = string.Empty;

        private bool IsDataLoded { get; set; } = false;
        private bool IsHiddenErrorDialog { get; set; } = true;



        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (!string.IsNullOrEmpty(authState.User.Identity?.Name))
            {
                UserName = authState.User.Identity.Name;
            }

            await LoadData();

            IsDataLoded = true;
        }

        private async Task LoadData()
        {
            ClosestCalendarItemsRequest request = new() { NumberOfItems = 3 };
            var response = await ApiService.PostWithResultAsync<ClosestCalendarItemsRequest, List<CalendarItemModel>>("api/v1/calendar-items/closest",request);

            if(response.IsSuccess)
            {
                ClosestCalendarItems = response.Data;
            }
            else
            {
                ErrorText = response.Error.Message;
                IsHiddenErrorDialog = false;
            }   


        }

        private async Task NavigateToCalendar()
        {
            NavigationManager.NavigateTo("/calendar", true);
        }

        private string CalendarItemTimeText(CalendarItemModel calendarItem)
        {
            DateTime calendarItemtext = calendarItem.Time;

            if (calendarItemtext.Date == DateTime.Now.Date)
            {
                return $"Dziś {calendarItem.Time.Hour}.{calendarItem.Time.Minute}";
            }
            else if (calendarItemtext.Date == DateTime.Now.AddDays(1).Date)
            {
                return $"Jutro {calendarItem.Time.Hour}.{calendarItem.Time.Minute} ";
            }
            else
            {
                int diffrence = (calendarItemtext.Date - DateTime.Now.Date).Days;

                return $"Za {diffrence} dni {calendarItem.Time.Hour}.{calendarItem.Time.Minute}";
            }
        }
    }
}
