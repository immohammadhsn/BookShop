using Microsoft.AspNetCore.Components;

namespace BookShop.Client
{
    public partial class StatusMessage
    {
        private string? Message { get; set; }
        [Parameter] public int DurationInSeconds { get; set; } = 3;
        private MessageType Type { get; set; } = MessageType.Success;

        private bool isVisible = false;


        public async Task Error(string message)
        {
            Type = MessageType.Error;
            Message = message;
            await DisplayMessage();
        }
        public async Task Info(string message)
        {
            Type = MessageType.Success;
            Message = message;
            await DisplayMessage();
        }
        public async Task Warning(string message)
        {
            Type = MessageType.Warning;
            Message = message;
            await DisplayMessage();
        }
        protected async Task DisplayMessage()
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