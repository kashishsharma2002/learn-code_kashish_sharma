using ATMWithdrawalFlow.Controllers;
using ATMWithdrawalFlow.UI;

namespace ATMWithdrawalFlow
{
    class Program
    {
        static void Main(string[] args)
        {

            var controller = new ATMDeviceController();
            var clientApp = new ATMConsoleClient(controller);

            clientApp.ExecuteScenarios();
        }
    }
}