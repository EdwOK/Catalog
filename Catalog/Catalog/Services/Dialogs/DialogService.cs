using System.Threading.Tasks;

namespace Catalog.Services.Dialogs
{
    internal class DialogConstants
    {
        internal const string DefaultAlertTitle = "Alert";
        internal const string OkayButton = "OK";
        internal const string CancelButton = "Cancel";
        internal const string DefaultConfirmTitle = "Confirm";
        internal const string DefaultErrorMessage = "Error!";
    }

    public class DialogService : IDialogService
    {
        private readonly IApplicationProvider _applicationProvider;

        public DialogService(IApplicationProvider applicationProvider)
        {
            _applicationProvider = applicationProvider;
        }

        public async Task Alert(string message, string title, string button)
        {
            await _applicationProvider.MainPage.DisplayAlert(title, message, button);
        }

        public async Task<bool> Confirm(string message, string title, string acceptButton, string cancelButton)
        {
            return await _applicationProvider.MainPage.DisplayAlert(title, message, acceptButton, cancelButton);
        }
    }
}
