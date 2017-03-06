using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RobotController.Models;

namespace RobotController.Services
{
    public class CommandService
    {
        private SlotService slotService;
        private HistoryService historyService;
        private DisplayService displayService;

        public CommandService(SlotService slotService, HistoryService historyService, DisplayService displayService)
        {
            this.slotService = slotService;
            this.displayService = displayService;
            this.historyService = historyService;
        }

        public void ParseCommand(string command, bool isUndo = false)
        {
            var actionReg = new Regex(@"^\w*");
            if (!actionReg.IsMatch(command))
            {
                //with more time I would add more detailed error messaging.
                throw new Exception("Invalid command, Please try again.");
            }

            var action = actionReg.Match(command).Value;
            var slotIds = new int[2];

            if (action != "size" && !slotService.AreSlotsInitialized())
            {
                throw new Exception("You must run the size command first.");
            }

            switch (action)
            {
                case "size":
                    var numSlots = ParseParameters(command);
                    slotService.Size(numSlots[0]);
                    if (!isUndo)
                    {
                        displayService.DisplaySlots();
                    }
                    break;
                case "add":
                    slotIds = ParseParameters(command);
                    slotService.AddBlock(slotIds[0]);
                    if (!isUndo)
                    {
                        displayService.DisplaySlots();
                    }
                    break;
                case "mv":
                    slotIds = ParseParameters(command);
                    slotService.MoveBlock(slotIds[0], slotIds[1]);
                    if (!isUndo)
                    {
                        displayService.DisplaySlots();
                    }
                    break;
                case "rm":
                    slotIds = ParseParameters(command);
                    slotService.RemoveBlock(slotIds[0]);
                    if (!isUndo)
                    {
                        displayService.DisplaySlots();
                    }
                    break;
                case "replay":
                    var numHistories = ParseParameters(command);
                    displayService.DisplayLastNHistories(numHistories[0]);
                    break;
                case "undo":
                    var numCommands = ParseParameters(command);
                    UndoCommands(numCommands[0]);
                    if (!isUndo)
                    {
                        displayService.DisplaySlots();
                    }
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

        public void UndoCommands(int numCommands)
        {
            /* 
             * Idea is to reset the slots and re-run all commands up to the number to "undo".
             * This creates a slight problem when "undoing" a "undo" command as it creates a 
             * never ending loop. If I had more time I would optimize to reset the histories
             * after an undo as well to prevent this.
             */
            var currentHistories = historyService.GetHistoryCount();
            var history = historyService.GetFirstNCommands(currentHistories - numCommands);
            slotService.ResetSlots();
            foreach (var command in history)
            {
                ParseCommand(command.Command, true);
            }

        }
    }
}
