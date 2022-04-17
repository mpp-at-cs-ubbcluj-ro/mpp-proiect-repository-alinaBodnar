using System;
using networking;
using teledonCS.model;

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

        public Object Data
        {
            get
            {
                return data;
            }
        }
    }
}