using Microsoft.AspNetCore.Components;

namespace Core.Components.Dialogs
{
    public partial class ErrorDialog
    {
        [Parameter] public string Message { get; set; }
        [Parameter] public bool IsHidden  { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task Close()
        {
            if (OnClose.HasDelegate)
                await OnClose.InvokeAsync();
        }
    }
}
