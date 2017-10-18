using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace AppBlock
{
    public class Processes
    {
        private TimeSlot allocatedTime;
        private int blockedProcessesNo = 0;
        private BlockedProcess[] blockedProcesses = new BlockedProcess[100];

        public Processes(TimeSlot allocatedTime)
        {
            this.allocatedTime = allocatedTime;

            for (int i = 0; i < 100; i++)
            {
                blockedProcesses[i] = new BlockedProcess();
            }
        }


        
        public void addBlockedProcess(String name)
        {//adds a procces and its allocated time to the array

            blockedProcesses[blockedProcessesNo].setBlockedProcessName(name);
            blockedProcessesNo++;
        }

        #region getters and setters

        public int getBlockedProcessesNo()
        {
            return blockedProcessesNo;
        }

        public String getBlockedProcessName(int position)
        {
            return blockedProcesses[position].getBlockedProcessName();
        }

        public TimeSlot getAllocatedTime()
        {
            return allocatedTime;
        }

        public void setAllocatedTime(TimeSlot allocatedTime)
        {
            this.allocatedTime = allocatedTime;
        }

        public void setBlockProcessesNo(int no)
        {
            this.blockedProcessesNo = no;
        }

        public void setBlockedProcessName(int position, String name)
        {
            this.blockedProcesses[position].setBlockedProcessName(name);
        }

        #endregion


        public static bool isAllowed(Time fromTime, Time toTime)
        {
            int fromHour = fromTime.getHour();
            int toHour = toTime.getHour();
            int currentHour = DateTime.Now.Hour;

            if (toHour < fromHour)
            {
                toHour += 24;
                if (currentHour < fromHour) currentHour += 24;
            }

            if (fromHour == currentHour)
                if (fromTime.getMinute() > DateTime.Now.Minute)
                    return false;

            if (toHour == currentHour)
                if (toTime.getMinute() < DateTime.Now.Minute)
                    return false;

            if (fromHour <= currentHour && currentHour <= toHour)
                return true;


            return false;


        }

        public static bool killProcess(string name)
        {//looks up and kills the process provided by parameter

            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.ToLower().StartsWith(name.ToLower()))
                {
                    clsProcess.Kill();
                    AppUI.displayAttempt(clsProcess.ProcessName);
                    return true;
                }
            }
            return false;
        }

        
        public void killProcesses()
        {//goes through the array of processes and kills them

            for (int i = 0; i < getBlockedProcessesNo(); i++)
                Processes.killProcess(getBlockedProcessName(i));
       
        }

    }
}
