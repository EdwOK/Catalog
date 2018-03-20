using System.Threading.Tasks;

namespace Catalog.Services.Dialogs
{
    public interface IDialogService
    {
        Task Alert(string message, string title, string button);

        Task<bool> Confirm(string message, string title, string acceptButton, string cancelButton);

        Task Error();
    }
}
