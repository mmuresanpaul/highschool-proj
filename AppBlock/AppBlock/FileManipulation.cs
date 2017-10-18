using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Reflection;
using System.Security.Cryptography;


namespace AppBlock
{
    public class FileManipulation
    {
        private static String filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AppBlock\";
        private static String fileName = "data.ab";
        private static String fileName2 = "data2.ab";

        public FileManipulation(TimeSlot time, Processes processes)
        {
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            if (!File.Exists(filePath + fileName))
                createTimeSlotFile(time);


            if (!File.Exists(filePath + fileName2))
                createProcessesFile(processes);
        }


        public void createTimeSlotFile(TimeSlot time)
        {
           
            TextWriter tw = new StreamWriter(filePath + fileName);
           
            tw.WriteLine(Encrypt(time.getFromTime().getHour().ToString()));
            tw.WriteLine(Encrypt(time.getFromTime().getMinute().ToString()));
            tw.WriteLine(Encrypt(time.getFromTime().getSecond().ToString()));

            tw.WriteLine(Encrypt(time.getToTime().getHour().ToString()));
            tw.WriteLine(Encrypt(time.getToTime().getMinute().ToString()));
            tw.WriteLine(Encrypt(time.getToTime().getSecond().ToString()));
            tw.Close();

        
        }

        public void createProcessesFile(Processes processes)
        {

            TextWriter tw2 = new StreamWriter(filePath + fileName2);

            tw2.WriteLine(Encrypt(processes.getBlockedProcessesNo().ToString()));
            for (int i = 0; i < processes.getBlockedProcessesNo(); i++)
            {
                tw2.WriteLine(Encrypt(processes.getBlockedProcessName(i)));
            }
            tw2.Close();

        }
        public Processes getProcesses()
        {          
            Processes returnedProcesses = new Processes(getTimeSlot());
            StreamReader sr = new StreamReader(filePath + fileName2);

            int numberOfLines = Convert.ToInt32(Decrypt(sr.ReadLine()));
                

            for(int i=0; i< numberOfLines; i++)
                returnedProcesses.addBlockedProcess(Decrypt(sr.ReadLine()));

            sr.Close();

            return returnedProcesses;       

        }

        public TimeSlot getTimeSlot()
        {
          

            Time returnedFromTime = new Time(0, 0, 0);
            Time returnedToTime = new Time(0, 0, 0);
            TimeSlot returnedTime = new TimeSlot(returnedFromTime, returnedToTime);
            StreamReader sr = new StreamReader(filePath + fileName);

            returnedTime.getFromTime().setHour(Convert.ToInt32(Decrypt(sr.ReadLine())));
            returnedTime.getFromTime().setMinute(Convert.ToInt32(Decrypt(sr.ReadLine())));
            returnedTime.getFromTime().setSecond(Convert.ToInt32(Decrypt(sr.ReadLine())));

            returnedTime.getToTime().setHour(Convert.ToInt32(Decrypt(sr.ReadLine())));
            returnedTime.getToTime().setMinute(Convert.ToInt32(Decrypt(sr.ReadLine())));
            returnedTime.getToTime().setSecond(Convert.ToInt32(Decrypt(sr.ReadLine())));

            sr.Close();

            return returnedTime;

        }

        private static string Encrypt(string str)
        {
             byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
             byte[] data = Encoding.ASCII.GetBytes(str);

             string protectedData = Convert.ToBase64String(ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser));

             return protectedData;
            //return str;
        }

        private static string Decrypt(string str)
        {
            
            byte[] protectedData = Convert.FromBase64String(str);
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            string data = Encoding.ASCII.GetString(ProtectedData.Unprotect(protectedData, entropy, DataProtectionScope.CurrentUser));

            return data;
            //return str;
        }


    }
}
