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
            var slotService =  new SlotService(slots);
            var commandService = new CommandService(slotService);
            var displayService = new DisplayService(slots);

            while (true)
            {
                try
                {
                    Console.Write(">");
                    var command = Console.ReadLine();
                    commandService.ParseCommand(command);
                    displayService.DisplaySlots();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
    
            }
        }
    }
}
