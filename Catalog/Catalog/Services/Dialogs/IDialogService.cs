using System.Threading.Tasks;

namespace Catalog.Services.Dialogs
{
    public interface IDialogService
    {
        Task Alert(string message, string title = DialogConstants.DefaultAlertTitle, string button = DialogConstants.OkayButton);

        Task<bool> Confirm(string message, string title = DialogConstants.DefaultConfirmTitle, string acceptButton = DialogConstants.OkayButton, string cancelButton = DialogConstants.CancelButton);
    }
}
