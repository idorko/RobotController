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
        private List<Slot> slots;

        public DisplayService(List<Slot> slots)
        {
            this.slots = slots;
        }

        public void DisplaySlots()
        {
            foreach (var slot in slots)
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
    }
}
