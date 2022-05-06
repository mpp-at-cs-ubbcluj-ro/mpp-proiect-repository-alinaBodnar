using System;
using networking;

namespace clientCs
{
    public enum TeledonUserEvent
    {
        NEW_DONATION
    };
    public class TeledonUserEventArgs:EventArgs
    {
        private readonly TeledonUserEvent userEvent;
        private readonly Object data;

        public TeledonUserEventArgs(TeledonUserEvent userEvent, object data)
        {
            this.userEvent = userEvent;
            this.data = data;
        }

        public TeledonUserEvent UserEventType
        {
            get
            {
                return userEvent;
            }
        }

        public object Data
        {
            get
            {
                return data;
            }
        }
    }
}