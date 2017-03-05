using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController.Models
{
    public class Slot
    {
        public int SlotId { get; set; }
        public List<Block> Blocks { get; set; }
    }
}
