using System;
using ATMWithdrawalFlow.Exceptions;
using ATMWithdrawalFlow.Models;
using ATMWithdrawalFlow.Services;
using ATMWithdrawalFlow.Constants;

namespace ATMWithdrawalFlow.Controllers 
{
    public class ATMDeviceController 
    {
        private readonly ATMService _atmService = new ATMService();

        public void Withdraw(string accountId, double amount) 
        {
            DeviceHandle handle = _atmService.GetHandle(AppConstants.DefaultDeviceId);
            if (handle == DeviceHandle.Invalid) 
            {
                throw new Exception("Unknown Error");
            }

            DeviceRecord record = _atmService.GetDeviceRecord(handle);

            if (record.Status == DeviceStatus.Suspended) 
            {
                throw new DeviceLockedException("Device is suspended");
            }

            if (record.Connection != ConnectionStatus.Connected) 
            {
                throw new NetworkConnectionException();
            }

            if (_atmService.GetBalance(accountId) < amount) 
            {
                throw new InsufficientFundsException();
            }

            _atmService.DispenseCash(handle, amount);
        }
    }
}