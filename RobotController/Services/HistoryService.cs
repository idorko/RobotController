using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotController.Models;

namespace RobotController.Services
{
    public class HistoryService
    {
        private List<History> histories;

        public HistoryService(List<History> histories)
        {
            this.histories = histories;
        }

        public void AddHistory(string command)
        {
            
            histories.Add(new History
            {
                Command = command,
                Order = histories.Count 
            });
        }

        public int GetHistoryCount()
        {
            return histories.Count();
        }

        public List<History> GetLastNCommands(int numCommands)
        {
            if (numCommands > histories.Count)
            {
                throw  new Exception("There aren't that many commands in the history!");
            }
            return histories.OrderByDescending(h => h.Order).ToList().GetRange(0, numCommands);
        }

        public List<History> GetFirstNCommands(int numCommands)
        {
            if (numCommands > histories.Count)
            {
                throw new Exception("There aren't that many commands in the history!");
            }
            return histories.OrderBy(h => h.Order).ToList().GetRange(0, numCommands);
        }
    }
}
