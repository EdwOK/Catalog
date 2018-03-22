using Xamarin.Forms;

namespace Catalog.Infrastructure.Behaviour
{
    public class MaxLengthTextBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthTextBehavior), 0);

        public int MaxLength
        {
            get => (int) GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += BindableOnTextChanged;
        }

        private void BindableOnTextChanged(object sender, TextChangedEventArgs eventArgs)
        {
            if (eventArgs.NewTextValue.Length > 0 && eventArgs.NewTextValue.Length > MaxLength)
            {
                ((Entry)sender).Text = eventArgs.NewTextValue.Substring(0, MaxLength);
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= BindableOnTextChanged;
        }
    }
}
