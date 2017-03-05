using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RobotController.Models;

namespace RobotController.Services
{
    public class CommandService
    {
        private SlotService slotService;

        public CommandService(SlotService slotService)
        {
            this.slotService = slotService;
        }

        public void ParseCommand(string command)
        {
            var actionReg = new Regex(@"^\w*");
            if (!actionReg.IsMatch(command))
            {
                //with more time I would add more detailed error messaging.
                throw new Exception("Invalid command, Please try again.");
            }

            var action = actionReg.Match(command).Value;
            var slotIds = new int[2];
            switch (action)
            {
                case "size":
                    var numSlots = ParseParameters(command);
                    slotService.Size(numSlots[0]);
                    break;
                case "add":
                    slotIds = ParseParameters(command);
                    slotService.AddBlock(slotIds[0]);
                    break;
                case "mv":
                    slotIds = ParseParameters(command);
                    slotService.MoveBlock(slotIds[0], slotIds[1]);
                    break;
                case "rm":
                    slotIds = ParseParameters(command);
                    slotService.RemoveBlock(slotIds[0]);
                    break;
                default:
                    throw new Exception("Invalid command. Please try again.");

            }
        }

        public int[] ParseParameters(string command)
        {
            var paramReg = new Regex(@"(?<=\s|^)\d+(?=\s|$)");
            if (!paramReg.IsMatch(command))
            {
                /*
                 * with more time I would add more detailed error messaging.
                 * I would want to check for negative numbers, for example.
                 */
                throw new Exception("Invalid paramaters Please try again.");
            }
            var matches = paramReg.Matches(command);
            var param = matches[0].Value;
            var firstParam = int.Parse(param);
            var secondParam = 0;
            if (matches.Count > 1)
            {
                param = matches[1].Value;
                secondParam = int.Parse(param);
            }
            return new int[]
            {
                firstParam, secondParam
            };
        }
    }
}
