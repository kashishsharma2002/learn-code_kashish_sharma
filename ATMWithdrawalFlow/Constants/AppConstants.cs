using ATMWithdrawalFlow.Models;

namespace ATMWithdrawalFlow.Constants
{
    public static class AppConstants
    {
        public const string DefaultAccountId = "ACC123";
        public const string DefaultDeviceId = "DEV1";
        
        public const double StartingBalance = 1000.0;
        public const double ValidWithdrawalAmount = 500.0;
        public const double OverdraftWithdrawalAmount = 1500.0;

        public static DeviceStatus CurrentDeviceStatus = DeviceStatus.Active;
        public static ConnectionStatus CurrentConnectionStatus = ConnectionStatus.Connected;
    }
}
