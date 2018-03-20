using System.Threading.Tasks;

namespace Catalog.Services.Dialogs
{
    public class DialogService : IDialogService
    {
        private readonly IApplicationProvider _applicationProvider;

        private const string DefaultAlertTitle = "Alert";
        private const string OkayButton = "OK";
        private const string CancelButton = "OK";
        private const string DefaultConfirmTitle = "Confirm";
        private const string DefaultErrorMessage = "Error!";

        public DialogService(IApplicationProvider applicationProvider)
        {
            _applicationProvider = applicationProvider;
        }

        public async Task Alert(string message, string title = DefaultAlertTitle, string button = OkayButton)
        {
            await _applicationProvider.MainPage.DisplayAlert(title, message, button);
        }


        public async Task<bool> Confirm(string message, string title = DefaultConfirmTitle, string acceptButton = OkayButton, string cancelButton = CancelButton)
        {
            return await _applicationProvider.MainPage.DisplayAlert(title, message, acceptButton, cancelButton);
        }

        public async Task Error()
        {
            await Alert(DefaultErrorMessage);
        }
    }
}
