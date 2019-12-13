using System;
namespace KyAApp.Controls
{
    public sealed class StatusChangedEventArgs : EventArgs
    {
        public StatusChangedEventArgs(ExpandStatus status)
        {
            Status = status;
        }

        public ExpandStatus Status { get; }
    }
}
