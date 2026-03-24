using System;
using ATMWithdrawalFlow.Controllers;
using ATMWithdrawalFlow.Constants;
using ATMWithdrawalFlow.Models;

namespace ATMWithdrawalFlow.UI
{
    public class ATMConsoleClient
    {
        private readonly ATMDeviceController _controller;

        public ATMConsoleClient(ATMDeviceController controller)
        {
            _controller = controller;
        }

        public void ExecuteScenarios()
        {
            AppConstants.CurrentDeviceStatus = DeviceStatus.Active;
            AppConstants.CurrentConnectionStatus = ConnectionStatus.Connected;

            Console.WriteLine("--- Case 1: Normal Flow Happy Path ---");
            PerformTransaction(AppConstants.DefaultAccountId, AppConstants.ValidWithdrawalAmount);

            Console.WriteLine("--- Case 2: Error Customization (Insufficient Funds) ---");
           
            PerformTransaction(AppConstants.DefaultAccountId, AppConstants.OverdraftWithdrawalAmount);

            Console.WriteLine("--- Case 3: Error Customization (Device Locked) ---");

            AppConstants.CurrentDeviceStatus = DeviceStatus.Suspended;
            PerformTransaction(AppConstants.DefaultAccountId, AppConstants.ValidWithdrawalAmount);

            Console.WriteLine("--- Case 4: Error Customization (Network Connection Error) ---");

            AppConstants.CurrentDeviceStatus = DeviceStatus.Active;
            AppConstants.CurrentConnectionStatus = ConnectionStatus.Disconnected;
            PerformTransaction(AppConstants.DefaultAccountId, AppConstants.ValidWithdrawalAmount);
            
            Console.WriteLine("Program ended gracefully.");
        }

        private void PerformTransaction(string accountId, double amount)
        {
            try
            {
                _controller.Withdraw(accountId, amount);
                Console.WriteLine("Withdrawal Successful! Please retrieve your cash.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transaction Handled Gracefully: {ex.Message}\n");
            }
        }
    }
}
