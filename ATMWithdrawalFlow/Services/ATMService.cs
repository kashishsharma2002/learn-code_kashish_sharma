using ATMWithdrawalFlow.Models;
using ATMWithdrawalFlow.Constants;

namespace ATMWithdrawalFlow.Services
{
    public class ATMService
    {
        public DeviceHandle GetHandle(string deviceId)
        {
            return DeviceHandle.Valid;
        }

        public DeviceRecord GetDeviceRecord(DeviceHandle handle)
        {
            return new DeviceRecord
            {
                Status = AppConstants.CurrentDeviceStatus,
                Connection = AppConstants.CurrentConnectionStatus
            };
        }

        public double GetBalance(string accountId)
        {
            return AppConstants.StartingBalance;
        }

        public void DispenseCash(DeviceHandle handle, double amount)
        {
        }
    }
}