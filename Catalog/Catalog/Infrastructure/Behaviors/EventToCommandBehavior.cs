using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace Catalog.Infrastructure.Behaviors
{
    public class EventToCommandBehavior : BindableBehavior<VisualElement>
    {
        public static BindableProperty EventNameProperty =
            BindableProperty.CreateAttached("EventName", typeof(string), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);

        public static BindableProperty CommandProperty =
            BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(EventToCommandBehavior), null);

        public static BindableProperty CommandParameterProperty =
            BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(EventToCommandBehavior), null);

        public static BindableProperty EventArgsConverterProperty =
            BindableProperty.CreateAttached("EventArgsConverter", typeof(IValueConverter), typeof(EventToCommandBehavior), null);

        public static BindableProperty EventArgsConverterParameterProperty =
            BindableProperty.CreateAttached("EventArgsConverterParameter", typeof(object), typeof(EventToCommandBehavior), null);

        protected Delegate Handler;

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public IValueConverter EventArgsConverter
        {
            get => (IValueConverter)GetValue(EventArgsConverterProperty);
            set => SetValue(EventArgsConverterProperty, value);
        }

        public object EventArgsConverterParameter
        {
            get => GetValue(EventArgsConverterParameterProperty);
            set => SetValue(EventArgsConverterParameterProperty, value);
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            RegisterEvent(EventName);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            DeregisterEvent(EventName);
            base.OnDetachingFrom(bindable);
        }

        void RegisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);

            if (eventInfo == null)
            {
                throw new ArgumentException($"Can't find any event named '{EventName}' on attached type");
            }

            AddEventHandler(eventInfo);
        }

        void DeregisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            if (Handler == null)
            {
                return;
            }

            EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);

            if (eventInfo == null)
            {
                throw new ArgumentException($"Can't find any event named '{name}' on attached type");
            }

            RemoveEventHandler(eventInfo);
        }

        private void AddEventHandler(EventInfo eventInfo)
        {
            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnFired");
            Handler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociatedObject, Handler);
        }

        private void RemoveEventHandler(EventInfo eventInfo)
        {
            eventInfo.RemoveEventHandler(AssociatedObject, Handler);
            Handler = null;
        }

        private void OnFired(object sender, EventArgs eventArgs)
        {
            if (Command == null)
            {
                return;
            }

            var parameter = CommandParameter;

            if (eventArgs != null && eventArgs != EventArgs.Empty)
            {
                parameter = eventArgs;

                if (EventArgsConverter != null)
                {
                    parameter = EventArgsConverter.Convert(eventArgs, typeof(object), EventArgsConverterParameter, CultureInfo.CurrentUICulture);
                }
            }

            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }
        }

        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (EventToCommandBehavior) bindable;

            if (behavior.AssociatedObject == null)
            {
                return;
            }

            string oldEventName = (string) oldValue;
            string newEventName = (string) newValue;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }
    }
}
