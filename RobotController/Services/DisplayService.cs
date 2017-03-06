using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotController.Models;

namespace RobotController.Services
{
    public class DisplayService
    {
        private SlotService slotService;
        private HistoryService historyService;

        public DisplayService(SlotService slotService, HistoryService historyService)
        {
            this.slotService = slotService;
            this.historyService = historyService;
        }

        public void DisplaySlots()
        {
            foreach (var slot in slotService.GetSlots())
            {
                DisplaySlot(slot);
            }
        }

        public void DisplaySlot(Slot slot)
        {
            StringBuilder output = new StringBuilder();
            output.Append(string.Format("{0}:", slot.SlotId));
            foreach (var block in slot.Blocks)
            {
                output.Append(string.Format(" {0}", block.Identifier));
            }
            Console.WriteLine(output);
        }

        public void DisplayLastNHistories(int numHistories)
        {
            var histories = historyService.GetLastNCommands(numHistories);
            foreach (var history in histories)
            {
                DisplayHistory(history);
            }
        }

        public void DisplayHistory(History history)
        {
            Console.WriteLine(history.Command);
        }
    }
}
