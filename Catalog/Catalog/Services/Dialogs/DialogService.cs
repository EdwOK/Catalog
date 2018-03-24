using System.Threading.Tasks;

namespace Catalog.Services.Dialogs
{
    internal class DialogConstants
    {
        internal const string DefaultAlertTitle = "Диалог";
        internal const string OkayButton = "Да";
        internal const string CancelButton = "Отмена";
        internal const string DefaultConfirmTitle = "Подтверждение";
        internal const string DefaultErrorMessage = "Ошибка!";
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
