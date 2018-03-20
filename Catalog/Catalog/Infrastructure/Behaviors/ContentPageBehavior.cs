using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Catalog.Infrastructure.Behaviors
{
    public class ContentPageBehavior : BindableBehavior<ContentPage>
    {
        public static readonly BindableProperty AppearingCommandProperty =
            BindableProperty.Create("AppearingCommand", typeof(ICommand), typeof(ContentPageBehavior), null,
                BindingMode.OneWay);

        public ICommand AppearingCommand
        {
            get => (ICommand) GetValue(AppearingCommandProperty);
            set => SetValue(AppearingCommandProperty, value);
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject.Appearing += AssociatedObject_Appearing;
        }

        void AssociatedObject_Appearing(object sender, EventArgs e)
        {
            if (AppearingCommand != null && AppearingCommand.CanExecute(null))
            {
                AppearingCommand?.Execute(null);
            }
        }

        protected override void OnDetachingFrom(ContentPage view)
        {
            AssociatedObject.Appearing -= AssociatedObject_Appearing;

            base.OnDetachingFrom(view);
        }
    }
}
