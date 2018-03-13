using Android.Content;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Forms.Droid.Platform;
using MvvmCross.Forms.Platform;

namespace Catalog.Droid
{
    public class Setup : MvxFormsAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override MvxFormsApplication CreateFormsApplication()
        {
            return new App();
        }

        protected override IMvxApplication CreateApp()
        {
            return new AppCore();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}