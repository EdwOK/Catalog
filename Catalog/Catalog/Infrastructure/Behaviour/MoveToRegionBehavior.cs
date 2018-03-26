using System;
using Behaviors;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Catalog.Infrastructure.Behaviour
{
    [Preserve(AllMembers = true)]
    public sealed class MoveToRegionBehavior : BehaviorBase<Map>
    {
        public static readonly BindableProperty RequestProperty = BindableProperty.Create("Request", typeof(MoveToRegionRequest), typeof(MoveToRegionBehavior), default(MoveToRegionRequest), propertyChanged: OnRequestChanged);

        private static void OnRequestChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MoveToRegionBehavior)bindable).OnRequestChanged(oldValue as MoveToRegionRequest, newValue as MoveToRegionRequest);
        }

        private void OnRequestChanged(MoveToRegionRequest oldValue, MoveToRegionRequest newValue)
        {
            if (oldValue != null)
            {
                oldValue.MoveToRegionRequested -= OnMoveToRegionRequested;
            }
            if (newValue != null)
            {
                newValue.MoveToRegionRequested += OnMoveToRegionRequested;
            }
        }

        private void OnMoveToRegionRequested(object sender, MoveToRegionRequestedEventArgs moveToRegionRequestedEventArgs)
        {
            if (moveToRegionRequestedEventArgs.MapSpan != null)
            {
                AssociatedObject.MoveToRegion(moveToRegionRequestedEventArgs.MapSpan);
            }
        }
    }

    public sealed class MoveToRegionRequest
    {
        internal event EventHandler<MoveToRegionRequestedEventArgs> MoveToRegionRequested;

        public void MoveToRegion(MapSpan mapSpan)
        {
            MoveToRegionRequested?.Invoke(this, new MoveToRegionRequestedEventArgs(mapSpan));
        }
    }

    internal sealed class MoveToRegionRequestedEventArgs : EventArgs
    {
        internal MapSpan MapSpan { get; }

        internal MoveToRegionRequestedEventArgs(MapSpan mapSpan)
        {
            MapSpan = mapSpan;
        }
    }
}
