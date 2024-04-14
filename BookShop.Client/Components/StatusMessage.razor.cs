using Microsoft.AspNetCore.Components;

namespace BookShop.Client
{
    public partial class StatusMessage
    {
        [Parameter] public string? Message { get; set; }
        [Parameter] public int DurationInSeconds { get; set; } = 3;
        [Parameter] public DateTime DateTime { get; set; }
        [Parameter] public MessageType Type { get; set; } = MessageType.Success;

        private bool isVisible = false;

        protected override async Task OnParametersSetAsync()
        {
            if (Message != null)
            {
                isVisible = true;

                // StateHasChanged to trigger re-render
                StateHasChanged();

                await Task.Delay(DurationInSeconds * 1000);

                isVisible = false;
                Message = null;

                // StateHasChanged again for final cleanup
                StateHasChanged();
            }
        }
        

        private string GetBackgroundColor()
        {
            return Type switch
            {
                MessageType.Success => "#28a745", // Green for success
                MessageType.Warning => "#ffc107", // Yellow for warning
                MessageType.Error => "#dc3545",   // Red for error
                _ => "#007bff",                    // Default blue color
            };
        }
    }

    public partial class StatusMessage
    {
        public enum MessageType
        {
            Success,
            Warning,
            Error
        }
    }
}