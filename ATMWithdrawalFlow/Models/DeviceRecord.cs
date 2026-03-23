namespace ATMWithdrawalFlow.Models
{
    public class DeviceRecord
    {
        public DeviceStatus Status { get; set; }
        public ConnectionStatus Connection { get; set; }
    }

    public enum DeviceStatus
    {
        Active,
        Suspended
    }

    public enum ConnectionStatus
    {
        Connected,
        Disconnected
    }
}