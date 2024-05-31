using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Timers;

namespace Digital_Clock
{
    internal class Program
    {
        // Generate a List for Alarms

        private static List<DateTime> alarms = new List<DateTime>();

        // Declare Constants for easier change and oversight

        private const string ClockOption = "Clock";
        private const string TimerOption = "Timer";
        private const string CounterOption = "Stopwatch";
        private const string AlarmOption = "Alarms";

        static void Main(string[] args)
        {

            // Main Initialize

            // Start a seperate Thread that runs simultaneously to check for Time Match

            var alarmThread = new Thread(() => CheckAlarms());
            alarmThread.Start();

            // Time (MAIN) Initialize

            int Seconds = 0;
            int Minutes = 0;
            int Hours = 0;

            string Date_Raw;

            Date_Raw = string.Empty;

            string UTC_Raw;

            UTC_Raw = string.Empty;

            string MainMenu_Option;

            Seconds = DateTime.Now.Second;
            Minutes = DateTime.Now.Minute;
            Hours = DateTime.Now.Hour;

            string Return = string.Empty;

            while (Return != "return")

            {

                Console.WriteLine("");
                Console.WriteLine("Digital Clock - Main menu");
                Console.WriteLine("");
                Console.WriteLine("Possible arguments: Clock, Timer, Stopwatch and Alarms");
                Console.WriteLine("");

                MainMenu_Option = Console.ReadLine();

                switch (MainMenu_Option)
                {
                    case ClockOption:
                        {
                            Console.Clear();

                            string Clock_Format;

                            Console.WriteLine("");
                            Console.WriteLine("Clock - Menu");
                            Console.WriteLine("");
                            Console.WriteLine("Choose format: (12H / 24H):");

                            Clock_Format = Convert.ToString(Console.ReadLine());

                            if (Clock_Format == "12H") // Calls the 12H Clock Method
                            {
                                Console.Clear();
                                Clock12H(Seconds, Minutes, Hours, UTC_Raw, Date_Raw);
                            }

                            else if (Clock_Format == "24H") // Calls the 24H Clock Method
                            {
                                Console.Clear();
                                Clock24H(Seconds, Minutes, Hours, UTC_Raw, Date_Raw);
                            }
                            else // Catch invalid input
                            {
                                InvalidArgument();
                                
                                break;
                            }

                            break;
                        }

                    case TimerOption:
                        {
                            Console.Clear();

                            int timerSeconds;

                            string time;

                            Console.WriteLine("");
                            Console.WriteLine("Timer - Menu");
                            Console.WriteLine("");
                            Console.WriteLine("Type in the desired time value like this: hh:mm:ss");
                            Console.WriteLine("");
                            time = Console.ReadLine();

                            // Check if the User Input is valid

                            if (IsValidTimeFormat(time))
                            {
                                // Split the entered time into the time components

                                string[] tempTime = time.Split(':');
                                int hoursT = Convert.ToInt32(tempTime[0]);
                                int minutesT = Convert.ToInt32(tempTime[1]);
                                int secondsT = Convert.ToInt32(tempTime[2]);

                                timerSeconds = ((hoursT * 60) * 60) + (minutesT * 60) + secondsT;

                                Console.Clear();

                                while (timerSeconds > 0)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine(" There are " + timerSeconds + " seconds left");
                                    timerSeconds--;

                                    // Normalize time units

                                    if (timerSeconds < 0)
                                        break;

                                    if (timerSeconds % 60 == 0)
                                    {
                                        minutesT++;
                                        secondsT = 0;
                                    }

                                    if (minutesT % 60 == 0)
                                    {
                                        hoursT++;
                                        minutesT = 0;
                                    }

                                    Task.Delay(1000).Wait();
                                    Console.Clear();
                                }

                                Console.WriteLine("");
                                Console.WriteLine(" Timer has Reached 0");

                                // Beeper

                                PlayAlarmSound();

                                Console.Clear();

                                break;

                            }
                            else // Catch invalid input
                            {
                                InvalidArgument();
                            }

                            break;
                        }

                    case CounterOption:
                        {
                            // Initialize

                            int Seconds_SW = 0;
                            int Minutes_SW = 0;
                            int Hours_SW = 0;

                            int i_SW = 1;

                            string zeroSSW;
                            string zeroMSW;
                            string zeroHSW;

                            bool isPaused = false;

                            while (i_SW == 1)
                            {
                                var startTime = DateTime.Now;

                                if (Console.KeyAvailable)
                                {
                                    ConsoleKeyInfo key = Console.ReadKey(true);
                                    if (key.Key == ConsoleKey.Enter)
                                    {
                                        isPaused = !isPaused; // Pause Switch

                                        while (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                                        {
                                            // Consume additional Enter key presses
                                        }
                                    }
                                }

                                if (!isPaused)
                                {
                                    Seconds_SW++;

                                    zeroHSW = "0";
                                    zeroMSW = "0";
                                    zeroSSW = "0";

                                    // Time (Math) engine

                                    if (Seconds_SW > 59)
                                    {
                                        Minutes_SW++;
                                        Seconds_SW = 0;
                                    }

                                    if (Minutes_SW > 59)
                                    {
                                        Hours_SW++;
                                        Minutes_SW = 0;
                                    }

                                    if (Hours_SW > 24)
                                    {
                                        break;
                                    }

                                    // To display the Zeros

                                    if (Seconds_SW > 9)
                                    {
                                        zeroSSW = string.Empty;
                                    }

                                    if (Minutes_SW > 9)
                                    {
                                        zeroMSW = string.Empty;
                                    }

                                    if (Hours_SW > 9)
                                    {
                                        zeroHSW = string.Empty;
                                    }

                                    Console.Clear();
                                    Console.WriteLine("");
                                    Console.WriteLine(" " + zeroHSW + Hours_SW + ":" + zeroMSW + Minutes_SW + ":" + zeroSSW + Seconds_SW);
                                    Console.WriteLine("");

                                    // Calculate the one-second delay with the code Latency (Time the code needs to be completed) to improve the overall latency

                                    var elapsedTime = DateTime.Now - startTime;
                                    var remainingDelay = 1000 - (int)elapsedTime.TotalMilliseconds;
                                    if (remainingDelay > 0)
                                    {
                                        Task.Delay(remainingDelay).Wait();
                                    }
                                }
                            }

                            break;
                        }

                    case AlarmOption:
                        {
                            // Initialize

                            string Alarm_Time = string.Empty;



                            Console.Clear();

                            Console.WriteLine("");
                            Console.WriteLine("Alarms - Menu");
                            Console.WriteLine("");
                            Console.WriteLine("Please enter a time value for the Alarm to ring (Format hh:mm)");
                            Console.WriteLine("");
                            Alarm_Time = (Console.ReadLine());

                            // Check if the input time is valid

                            if (IsValidTimeFormat_Alarm(Alarm_Time))
                            {
                                // Split the entered time into the time components

                                string[] tempTime = Alarm_Time.Split(':');
                                int hours_A = Convert.ToInt32(tempTime[0]);
                                int minutes_A = Convert.ToInt32(tempTime[1]);

                                // Get the current date and set the alarm time

                                DateTime currentDate = DateTime.Now.Date;
                                DateTime alarmTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hours_A, minutes_A, 0);

                                // Add the alarm to the list

                                alarms.Add(alarmTime);

                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("New Alarm set for " + Alarm_Time);

                            }
                            Task.Delay(8000).Wait();

                            Console.Clear();

                            break;
                        }

                    // Catch invalid input (Switch)

                    default:
                        {
                            InvalidArgument();

                            break;
                        }
                }

            }

            alarmThread.Join();
        }

        static int Clock12H(int Seconds, int Minutes, int Hours, string Date_Raw, string UTC_Raw)
        {

            // Initialize

            string zeroH;
            string zeroM;
            string zeroS;

            string UTCzeroH;
            string UTCzeroM;
            string UTCzeroS;

            while (true)
            {
                var startTime = DateTime.Now;

                zeroH = "0";
                zeroM = "0";
                zeroS = "0";

                UTCzeroH = "0";
                UTCzeroM = "0";
                UTCzeroS = "0";

                // Import the Time from the System

                Seconds = DateTime.Now.Second;
                Minutes = DateTime.Now.Minute;
                Hours = DateTime.Now.Hour;

                // Ensure that the Zero String for each time component is empty if the Value reaches 10 (As there would be 010 if not so)

                if (Minutes > 9)
                {
                    zeroM = string.Empty;
                }

                if (Seconds > 9)
                {
                    zeroS = string.Empty;
                }

                // Split the Time and Date to only use the Date

                Date_Raw = Convert.ToString(DateTime.Today);
                string[] Temp_Date = Date_Raw.Split();
                string Date = Temp_Date[0];

                // Split the Time and Date to only use Time (UTC)

                UTC_Raw = Convert.ToString(DateTime.UtcNow);
                string[] Temp_UTC = UTC_Raw.Split();
                string UTC = Temp_UTC[1];

                // Split the UTC into Hours Minutes and Seconds

                string[] Temp_UTC12H = UTC.Split(':');
                int UTC12H_H = Convert.ToInt32(Temp_UTC12H[0]);
                int UTC12H_M = Convert.ToInt32(Temp_UTC12H[1]);
                int UTC12H_S = Convert.ToInt32(Temp_UTC12H[2]);

                if (UTC12H_M > 9)
                {
                    UTCzeroM = string.Empty;
                }

                if (UTC12H_S > 9)
                {
                    UTCzeroS = string.Empty;
                }

                Console.WriteLine("");
                Console.WriteLine(" The Date is:       " + Date);

                if (Hours < 12)
                {
                    Console.WriteLine("");
                    Console.Write(" The local time is:  " + zeroH + Hours + ":" + zeroM + Minutes + ":" + zeroS + Seconds + " Am");
                }

                if (Hours == 12)
                {
                    Console.WriteLine("");
                    Console.Write(" The local time is:  " + zeroH + Hours + ":" + zeroM + Minutes + ":" + zeroS + Seconds + " Pm");
                }

                if (Hours > 12)
                {
                    Hours = Hours - 12;
                    if (Hours > 9)
                    {
                        zeroH = string.Empty;
                    }
                    Console.WriteLine("");
                    Console.Write(" The local time is:  " + zeroH + Hours + ":" + zeroM + Minutes + ":" + zeroS + Seconds + " Pm");
                }

                if (UTC12H_H < 12)
                {
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine(" The UTC time is:    " + UTCzeroH + UTC12H_H + ":" + UTCzeroM + UTC12H_M + ":" + UTCzeroS + UTC12H_S + " Am");
                }

                if (UTC12H_H == 12)
                {
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine(" The UTC time is:    " + UTCzeroH + UTC12H_H + ":" + UTCzeroM + UTC12H_M + ":" + UTCzeroS + UTC12H_S + " Pm");
                }

                if (UTC12H_H > 12)
                {
                    UTC12H_H = UTC12H_H - 12;
                    if (UTC12H_H > 9)
                    {
                        UTCzeroH = string.Empty;
                    }
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine(" The UTC time is:    " + UTCzeroH + UTC12H_H + ":" + UTCzeroM + UTC12H_M + ":" + UTCzeroS + UTC12H_S + " Pm");
                }

                // Calculate the one second delay with the code Latency (Time the code needs to be completed) to improve the overall latency

                var elapsedTime = DateTime.Now - startTime;
                var remainingDelay = 1000 - (int)elapsedTime.TotalMilliseconds;
                if (remainingDelay > 0)
                    Task.Delay(remainingDelay).Wait();

                Console.Clear();
            }
        }

        static int Clock24H(int Seconds, int Minutes, int Hours, string Date_Raw, string UTC_Raw)
        {

            // Initiate

            string zeroH;
            string zeroM;
            string zeroS;

            while (true)
            {
                var startTime = DateTime.Now;

                zeroH = "0";
                zeroM = "0";
                zeroS = "0";

                // Import the Time from the System

                Seconds = DateTime.Now.Second;
                Minutes = DateTime.Now.Minute;
                Hours = DateTime.Now.Hour;

                // Ensure that the Zero String for each time component is empty if the Value reaches 10 (As there would be 010 if not so)

                if (Hours > 9)
                {
                    zeroH = string.Empty;
                }

                if (Minutes > 9)
                {
                    zeroM = string.Empty;
                }

                if (Seconds > 9)
                {
                    zeroS = string.Empty;
                }

                // Split the Time and Date to only use the Date

            Date_Raw = Convert.ToString(DateTime.Today);
            string[] Temp_Date = Date_Raw.Split();
            string Date = Temp_Date[0];

                // Split the Time and Date to only use Time (UTC)

            UTC_Raw = Convert.ToString(DateTime.UtcNow);
            string[] Temp_UTC = UTC_Raw.Split();
            string UTC = Temp_UTC[1];

                Console.WriteLine("");
                Console.WriteLine(" The Date is:       " + Date);

                if (Hours < 12)
                {
                    Console.WriteLine("");
                    Console.Write(" The local time is:  " + zeroH + Hours + ":" + zeroM + Minutes + ":" + zeroS + Seconds);
                }

                if (Hours == 12)
                {
                    Console.WriteLine("");
                    Console.Write(" The local time is:  " + zeroH +  Hours + ":" + zeroM + Minutes + ":" + zeroS + Seconds);
                }

                if (Hours > 12)
                {
                    Console.WriteLine("");
                    Console.Write(" The local time is:  " + zeroH + Hours + ":" + zeroM + Minutes + ":" + zeroS + Seconds);
                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" The UTC time is:    " + UTC);

                // Calculate the one second delay with the code Latency (Time the code needs to be completed) to improve the overall latency

                var elapsedTime = DateTime.Now - startTime;
                var remainingDelay = 1000 - (int)elapsedTime.TotalMilliseconds;
                if (remainingDelay > 0)
                Task.Delay(remainingDelay).Wait();

                Console.Clear();
            }
        }

        private static bool IsValidTimeFormat(string time)
        {
            // Split the String into seperate Seconds, Minutes and Hours to test if they are within range and not negative

            string[] timeComponents = time.Split(':');

            if (timeComponents.Length == 3)
            {
                for (int i = 0; i < timeComponents.Length; i++)
                {
                    if (!int.TryParse(timeComponents[i], out int result) || result < 0)
                    {
                        return false;
                    }

                    // Ensure minutes and seconds are within the valid range

                    if ((i == 1 || i == 2) && (result >= 60 || result < 0))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private static bool IsValidTimeFormat_Alarm(string Alarm_Time)
        {
            // Split the String into separate Hours, Minutes, and Seconds to test if they are within range and not negative

            string[] timeComponents = Alarm_Time.Split(':');

            if (timeComponents.Length == 2)
            {
                for (int i = 0; i < timeComponents.Length; i++)
                {
                    if (!int.TryParse(timeComponents[i], out int result) || result < 0)
                    {
                        return false;
                    }

                    // Ensure hours and minutes are within the valid range

                    if ((i == 0 && (result >= 24 || result < 0)) || (i == 1 && (result >= 60 || result < 0)))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        static void CheckAlarms()
        {
            while (true)
            {
                List<DateTime> alarmsCopy;

                lock (alarms) // Lock the list to prevent modifications during copying
                {
                    alarmsCopy = new List<DateTime>(alarms);
                }

                foreach (var alarm in alarmsCopy)
                {
                    var now = DateTime.Now;

                    if (now.Hour == alarm.Hour && now.Minute == alarm.Minute)
                    {
                        // Calculate the remaining seconds until the next minute

                        var remainingSeconds = 60 - now.Second;

                        // Adjust waiting time based on remaining seconds

                        var waitingTime = (alarm.Minute - now.Minute - 1) * 60 + remainingSeconds;

                        if (waitingTime >= 0 && now.Hour == alarm.Hour)
                        {
                            Thread.Sleep(waitingTime * 1000);
                            PlayAlarmSound();
                        }
                    }
                }

                Thread.Sleep(1000); // Wait one second to check alarm every second
            }
        }

        static void PlayAlarmSound()
        {
            int S = 10;

            // Beeper

            while (S > 1)
            {
                Console.Beep(456, 750);
                Task.Delay(500).Wait();
                S--;
            }
        }

        private static void InvalidArgument()
        {
            Console.WriteLine("");
            Console.WriteLine("Incorrect Argument / Input");
            Thread.Sleep(2000);
            Console.Clear();
        }
    }
}