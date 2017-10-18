using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlock
{
    class BlockedProcess
    {
        private String blockedProcessesName;

        public BlockedProcess() { }
        public BlockedProcess(String name)
        {
            this.blockedProcessesName = name;
        }     

        #region setters and getters

        public String getBlockedProcessName()
        {
            return blockedProcessesName;
        }

        public void setBlockedProcessName(String blockedProcessesName)
        {
            this.blockedProcessesName = blockedProcessesName;
        }
        #endregion

    }
}
