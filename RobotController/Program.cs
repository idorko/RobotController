using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotController.Models;
using RobotController.Services;

namespace RobotController
{
    class Program
    {
        static void Main(string[] args)
        {
            var slots = new List<Slot>();
            var histories = new List<History>();
            var slotService =  new SlotService(slots);
            var historyService = new HistoryService(histories);
            var displayService = new DisplayService(slots, historyService);
            var commandService = new CommandService(slotService, historyService, displayService);

            while (true)
            {
                try
                {
                    Console.Write(">");
                    var command = Console.ReadLine();
                    commandService.ParseCommand(command);
                    /* 
                     * I making an assumption that we only want to save the history for valid commands.
                     */
                     historyService.AddHistory(command);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
    
            }
        }
    }
}
