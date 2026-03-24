using System;

namespace   ATMWithdrawalFlow.Exceptions
{
    public class DeviceLockedException : Exception
    {
        public DeviceLockedException(string message) : base(message)
        {
        }
    }
}
