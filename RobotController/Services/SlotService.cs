using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotController.Models;

namespace RobotController.Services
{
    public class SlotService
    {
        private List<Slot> slots;
        private bool slotsIntialized;
        public SlotService(List<Slot> slots)
        {
            this.slots = slots;
            if (this.slots.Count > 0)
            {
                slotsIntialized = true;
            }
            else
            {
                slotsIntialized = false;
            }
        }

        public List<Slot> Size(int numSlots)
        {
            if (slots == null)
            {
                slots = new List<Slot>();
            }
            var currentNumSlots = slots.Count();
            if (currentNumSlots < numSlots)
            {
                for (int i = 0; i < numSlots - currentNumSlots; i++)
                {
                    slots.Add(new Slot
                    {
                        Blocks = new List<Block>(),
                        SlotId = currentNumSlots + i + 1
                    });
                }
            }

            if (currentNumSlots > numSlots)
            {
                /* 
                 * Assuming when resizing smaller that it's okay to get rid of slots 
                 * currently containing blocks. With more time I would probably add
                 * the ability to choose which slot to get rid of, or the option to 
                 * move blocks to a different slot when resizing smaller.
                 */

                for (int i = 0; i < currentNumSlots - numSlots; i++)
                {
                    slots.RemoveAt(currentNumSlots - 1 - i);
                }
            }

            slotsIntialized = true;
            return slots;
        }

        public List<Slot> AddBlock(int slotId)
        {
            var slot = slots[slotId-1];
            if (slot == null)
            {
                throw new ArgumentOutOfRangeException(string.Format("Cannot add block to Slot {0}. Slot {0} does not exist.", slotId));
            }
            
            slot.Blocks.Add(new Block
            {
                Identifier = "X"
            });

            return slots;
        }

        public List<Slot> MoveBlock(int fromSlotId, int toSlotId)
        {

            if (fromSlotId > slots.Count + 1)
            {
                throw new ArgumentOutOfRangeException(string.Format("Cannot move block from Slot {0}. Slot {0} does not exist.", fromSlotId));
            }
            if (toSlotId > slots.Count + 1)
            {
                throw new ArgumentOutOfRangeException(string.Format("Cannot move block to Slot {0}. Slot {0} does not exist.", toSlotId));
            }

            var fromSlot = slots[fromSlotId - 1];
            var toSlot = slots[toSlotId - 1];

            if (!fromSlot.Blocks.Any())
            {
                throw new Exception(string.Format("Cannt remove block from Slot {0}. Slot {0} doesn't contain any blocks.", fromSlotId));
            }

            var block = fromSlot.Blocks[fromSlot.Blocks.Count() - 1];
            toSlot.Blocks.Add(block);
            fromSlot.Blocks.Remove(block);
            return slots;

        }

        public List<Slot> RemoveBlock(int slotId)
        {
            var slot = slots[slotId-1];
            if (slot == null)
            {
                throw new ArgumentOutOfRangeException(string.Format("Cannot remove block from Slot {0}. Slot {0} does not exist.", slotId));
            }
            if (!slot.Blocks.Any())
            {
                throw new Exception(string.Format("Cannt remove block from Slot {0}. Slot {0} doesn't contain any blocks.", slotId));
            }
            slot.Blocks.RemoveAt(slot.Blocks.Count() - 1);

            return slots;
        }

        public bool AreSlotsInitialized()
        {
            return slotsIntialized;
        }

        public void ResetSlots()
        {
            slots = new List<Slot>();
            slotsIntialized = false;
        }
    }
}
