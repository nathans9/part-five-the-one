using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace part_five_the_one
{ 
    internal class Program
    {
        // Nathan
        const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]

        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        const int VK_RETURN = 0x0D;

        const int WM_KEYDOWN = 0x100;

        public static bool visited = false;
        public static Random generator = new Random();
        public static int[] balance = { generator.Next(0, 2147483647), generator.Next(0, 100) };
        public static double totalBalance = balance[0] + (balance[1] / 100);
        public static ConsoleKeyInfo cki = new ConsoleKeyInfo();
        public static System.Timers.Timer tactimer = new System.Timers.Timer();
        public static System.Timers.Timer invtimer = new System.Timers.Timer();
        public static int tick = 0, bop = 0;
        public static int intPocketChange = generator.Next(0, 2001); public static double decPocketChange = generator.Next(0, 100);  public static double pocketChange = intPocketChange + (decPocketChange / 100);
        public static bool giveMoney = true, broke = false, payed1 = false, payed2 = false;

        private static void Bank()
        {
            visited = true;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            string[] main = { "[The sky is nigh black, and raindrops cascade down from the heavens onto your umbrella]", "[You are really craving a donut, but the best donut shop around only accepts cash; something that you lack]", "[In order to get a donut, you need to make a transaction at Bank Bank, the bank owned by multimedia and business tycoon, Gargolomew the Greedy]", "[You enter Bank Bank, and walk over to the ATM]" };
            string[] option = { "Choice (#): ", "1. Deposit", "2. Withdraw", "3. Bill payment", "4. View account balance", "Press the 5 key to exit the bank." };
            string accept = "Press Y at any time to accept the terms and conditions, or press any other key to deny them.";
            string[] leave = { "If you are not here for business, please leave the building.", "[Security guards appear and escort you out]", "You have insufficient funds in your account to pay the transaction fee." };

            for (int i = 0; i < main[0].Length; i++)
            {
                Console.Write(main[0][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(500);
            for (int i = 0; i < main[1].Length; i++)
            {
                Console.Write(main[1][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(500);
            for (int i = 0; i < main[2].Length; i++)
            {
                Console.Write(main[2][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(500);
            for (int i = 0; i < main[3].Length; i++)
            {
                Console.Write(main[3][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(500);
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(".");
                Thread.Sleep(200);
            }
            for (int i = 0; i < accept.Length; i++)
            {
                Console.Write(accept[i]);
                Thread.Sleep(15);
            }

            tactimer.Elapsed += new ElapsedEventHandler(TACTimer); //Timer is used to type out the terms and conditions, and allows for the ReadKey to still be active.
            tactimer.Interval = 1;
            tactimer.Enabled = true;
            Console.WriteLine("");
            cki = Console.ReadKey();
            tactimer.Enabled = false;
            tick = 0;
            Console.Clear();

            if (cki.Key != ConsoleKey.Y)
            {
                
                for (int i = 0; i < leave[0].Length; i++)
                {
                    Console.Write(leave[0][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(500);
                for (int i = 0; i < leave[1].Length; i++)
                {
                    Console.Write(leave[1][i]);
                    Thread.Sleep(15);
                }
            }
            else 
            {
                bool chosen = false;
                bool quieres = true;


                while (!chosen)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (broke)
                    {
                        for (int i = 0; i < leave[2].Length; i++)
                        {
                            Console.Write(leave[2][i]);
                            Thread.Sleep(15);
                        }
                        Console.WriteLine("");
                        for (int i = 0; i < leave[1].Length; i++)
                        {
                            Console.Write(leave[1][i]);
                            Thread.Sleep(15);
                        }
                    }
                    else
                            {
                                while (quieres)
                                {
                                    for (int i = 0; i < option[5].Length; i++)
                                    {

                                        if (i < option[1].Length)
                                        {
                                            Console.SetCursorPosition(0 + i, 0);
                                            Console.Write(option[1][i]);
                                        }

                                        if (i < option[2].Length)
                                        {
                                            Console.SetCursorPosition(37 + i, 0);
                                            Console.Write(option[2][i]);
                                        }

                                        if (i < option[3].Length)
                                        {
                                            Console.SetCursorPosition(0 + i, 2);
                                            Console.Write(option[3][i]);
                                        }
                                        if (i < option[4].Length)
                                        {
                                            Console.SetCursorPosition(37 + i, 2);
                                            Console.Write(option[4][i]);
                                        }
                                if (i < option[5].Length)
                                {
                                    Console.SetCursorPosition(0 + i, 4);
                                    Console.Write(option[5][i]);
                                }

                                Thread.Sleep(15);

                                        if (i == option[5].Length - 1)
                                            quieres = false;
                                    }
                                }

                                Console.SetCursorPosition(0, 6);

                                Thread.Sleep(200);

                                for (int i = 0; i < option[0].Length; i++)
                                {
                                    Console.Write(option[0][i]);
                                    Thread.Sleep(15);
                                }

                                Console.CursorVisible = true;

                                cki = Console.ReadKey();

                                string invalid = "That is not a valid choice.  Per the terms and conditions, we will be charging Kč0.75 for your insolence.";

                                Console.CursorVisible = false;

                                if (cki.Key == ConsoleKey.D1)
                                {
                                    BankDeposit();
                                }

                                else if (cki.Key == ConsoleKey.D2)
                                {
                                    BankWithdraw();
                                }

                                else if (cki.Key == ConsoleKey.D3)
                                {
                                    BankBill();
                                }

                                else if (cki.Key == ConsoleKey.D4)
                                {
                                    BankBalance();
                                }
                                else if (cki.Key == ConsoleKey.D5)
                                {
                                    chosen = true;
                                    Console.WriteLine("");
                                    Console.WriteLine("");
                                    for (int i = 0; i < leave[1].Length; i++)
                                    {
                                    Console.Write(leave[1][i]);
                                    Thread.Sleep(15);
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("");
                                    for (int i = 0; i < invalid.Length; i++)
                                    {
                                        Console.Write(invalid[i]);
                                        Thread.Sleep(15);
                                    }
                                }
                                quieres = true;
                            }
                }
            }
           

            Thread.Sleep(5000);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

        private static void Garage()
        {
            visited = true;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            string[] main = { "[You are attending a TED Talk on the genetic mutation of crows late at night, and need to find a place to park]", "[Luckily, there is a Gargolomew Garage within a two minute walk of the talk]", "[You pull into the Gargolomew Garage, and choose a parking space]", "[Each Gargolomew Garage parking space has their own computer which tracks how much time is spent at the Gargolomew Garage]" };
            string[] gargcpu = { "HELLO!  WELCOME TO GARGOLOMEW GARAGE!  I AM THE COMPUTER THAT WILL BE TRACKING YOUR TIME SPENT USING THE GARGOLOMEW GARAGE.", "WE WILL BE CHARGING Kc400 PER HOUR SPENT IN THE GARGOLOMEW GARAGE, FOR A MAXIMUM OF Kc2000.", "YOU MAY NOW LEAVE YOUR VEHICLE.  TO CONCLUDE YOUR TIME AT THE GARAGE, PRESS ANY KEY AND YOU WILL BE BILLED ACCORDINGLY." };
            string[] exit = { "YOU HAVE PAID YOUR PARKING BILL!  HAVE A GARG OF A DAY!", "YOU HAVE INSUFFICIENT FUNDS TO PAY YOUR BILL...", "[Policemen drop through the ceiling and arrest you]", "YOUR BILL COMES TO Kc", "PRESS ANY KEY TO PAY YOUR BILL." };
            double HtimeLeft, MtimeLeft, StimeLeft, timeLeft;
            double HtimeReturned, MtimeReturned, StimeReturned, timeReturned, timeDiff, cost = 400;
            bool text = true;
            if (text)
                {
                for (int i = 0; i < main[0].Length; i++)
                {
                    Console.Write(main[0][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(300);
                for (int i = 0; i < main[1].Length; i++)
                {
                    Console.Write(main[1][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(300);
                for (int i = 0; i < main[2].Length; i++)
                {
                    Console.Write(main[2][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(300);
                for (int i = 0; i < main[3].Length; i++)
                {
                    Console.Write(main[3][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(1000);
                for (int i = 0; i < gargcpu[0].Length; i++)
                {
                    Console.Write(gargcpu[0][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(300);
                for (int i = 0; i < gargcpu[1].Length; i++)
                {
                    Console.Write(gargcpu[1][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(300);
            }

            HtimeLeft = DateTime.Now.Hour;
            MtimeLeft = DateTime.Now.Minute;
            StimeLeft = DateTime.Now.Second;

            Console.WriteLine("[The current time is " + HtimeLeft + ":" + MtimeLeft + ":" + StimeLeft + "]");

            Console.WriteLine("");

            for (int i = 0; i < gargcpu[2].Length; i++)
            {
                Console.Write(gargcpu[2][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");

            MtimeLeft = MtimeLeft / 60;
            StimeLeft = StimeLeft / 60;
            timeLeft = HtimeLeft + MtimeLeft + StimeLeft;

            Console.CursorVisible = true;
            Console.ReadKey();
            Console.CursorVisible = false;

            HtimeReturned = DateTime.Now.Hour;
            MtimeReturned = DateTime.Now.Minute;
            StimeReturned = DateTime.Now.Second;

            Console.WriteLine("[You have returned at " + HtimeLeft + ":" + MtimeLeft + ":" + StimeLeft + "]");
            Console.WriteLine("");

            for (int i = 0; i < exit[3].Length; i++)
            {
                Console.Write(exit[3][i]);
                Thread.Sleep(15);
            }
            for (int i = 0; i < Convert.ToString(cost).Length; i++)
            {
                Console.Write(Convert.ToString(cost)[i]);
                Thread.Sleep(15);
            }
            Console.WriteLine(".");
            Console.WriteLine("");
            Thread.Sleep(300);
            for (int i = 0; i < exit[4].Length; i++)
            {
                Console.Write(exit[4][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");


            MtimeReturned = MtimeReturned / 60;
            StimeReturned = StimeReturned / 3600;
            timeReturned = HtimeReturned + MtimeReturned + StimeReturned;
            timeDiff = timeReturned - timeLeft;


            
            if (timeDiff >= 1 && timeDiff < 2)
                cost = cost * 2;
            else if (timeDiff >= 2 && timeDiff < 3)
                cost = cost * 3;
            else if (timeDiff >= 3 && timeDiff < 4)
                cost = cost * 4;
            else
                cost = cost * 5;

            if (pocketChange >= cost)
            {
                for (int i = 0; i < exit[0].Length; i++)
                {
                    Console.Write(exit[0][i]);
                    Thread.Sleep(15);
                }
            }
            else 
            {
                for (int i = 0; i < exit[1].Length; i++)
                {
                    Console.Write(exit[1][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(300);
                for (int i = 0; i < exit[2].Length; i++)
                {
                    Console.Write(exit[2][i]);
                    Thread.Sleep(15);
                }
            }


            Thread.Sleep(5000);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

        private static void Hurricane()
        {
            visited = true;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            string[] main = { "[During a very educational TED Talk on crow eugenics and mutation, you are informed that the new mutated crows can safely fly through category 4 hurricanes]", "[This intruigues you, and you decide to research more into the categories of hurricanes]", "[You head to a nearby library, the Gargolomew library, and get onto the Gargolomew PC in order to scower the Gargolomew web for more info]"  };
            string[] menu = { "Which category of hurricane would you like to learn more about (#)?", "1. Category 1", "2. Category 2", "3. Category 3", "4. Category 4", "5. Category 5" };
            string[] cat = { "You can select another category to learn about, or you can press 6 to log off.", "Category 1 hurricanes can generate wind speeds of ", "Category 2 hurricanes can generate wind speeds of ", "Category 3 hurricanes can generate wind speeds of ", "Category 4 hurricanes can generate wind speeds of ", "Category 5 hurricanes can generate wind speeds of greater than ", "Thank you for your time!", "That is an invalid selection.  If you want to leave, press the 6 key, or you can select a category of hurricane to learn about." };
            bool chosen = false, quieres = true;
            double lcat1 = 119, hcat1 = 153, lcat2 = 154, hcat2 = 177, lcat3 = 178, hcat3 = 209, lcat4 = 210, hcat4 = 249, lcat5 = 250;

            for (int i = 0; i < main[0].Length; i++)
            {
                Console.Write(main[0][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(300);
            for (int i = 0; i < main[1].Length; i++)
            {
                Console.Write(main[1][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(300);
            for (int i = 0; i < main[2].Length; i++)
            {
                Console.Write(main[2][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(1000);

            while (quieres)
            {
                for (int i = 0; i < menu[5].Length; i++)
                {

                    if (i < menu[1].Length)
                    {
                        Console.SetCursorPosition(0 + i, 6);
                        Console.Write(menu[1][i]);
                    }

                    if (i < menu[2].Length)
                    {
                        Console.SetCursorPosition(37 + i, 6);
                        Console.Write(menu[2][i]);
                    }

                    if (i < menu[3].Length)
                    {
                        Console.SetCursorPosition(0 + i, 8);
                        Console.Write(menu[3][i]);
                    }
                    if (i < menu[4].Length)
                    {
                        Console.SetCursorPosition(37 + i, 8);
                        Console.Write(menu[4][i]);
                    }
                    if (i < menu[5].Length)
                    {
                        Console.SetCursorPosition(0 + i, 10);
                        Console.Write(menu[5][i]);
                    }

                    Thread.Sleep(15);

                    if (i == menu[5].Length - 1)
                        quieres = false;
                }
            }

            Console.SetCursorPosition(0, 12);
            for (int i = 0; i < cat[0].Length; i++)
            {
                Console.Write(cat[0][i]);
                Thread.Sleep(15);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(300);

            while (!chosen)
            {
                Console.CursorVisible = true;
                int p = Console.CursorTop;
                cki = Console.ReadKey();
                Console.CursorVisible = true;
                Console.SetCursorPosition(0, p);
                Console.Write(" ");
                Console.SetCursorPosition(0, p);
                bool text2 = true;

                switch (cki.Key) //I didn't want to make more methods for the seperate cases.  I probably should have.  I put if statements in there so I could just minimise the code.
                {
                    case ConsoleKey.D1:
                        if (text2)
                        {
                            for (int i = 0; i < cat[1].Length; i++)
                            {
                                Console.Write(cat[1][i]);
                                Thread.Sleep(15);
                            }
                            for (int i = 0; i < Convert.ToString(lcat1).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat1)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat1).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat1)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("km/h, ");
                            for (int i = 0; i < Convert.ToString(lcat1/1.609).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat1/1.609)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat1/1.609).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat1/1.609)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("mph, and ");
                            for (int i = 0; i < Convert.ToString(lcat1 / 1.852).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat1 / 1.852)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat1 / 1.852).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat1 / 1.852)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("kt.");
                            Console.WriteLine("");
                            Console.WriteLine("");
                        }
                        break;
                    case ConsoleKey.D2:
                        if (text2)
                        {
                            for (int i = 0; i < cat[2].Length; i++)
                            {
                                Console.Write(cat[2][i]);
                                Thread.Sleep(15);
                            }
                            for (int i = 0; i < Convert.ToString(lcat2).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat2)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat2).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat2)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("km/h, ");
                            for (int i = 0; i < Convert.ToString(lcat2 / 1.609).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat2 / 1.609)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat2 / 1.609).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat2 / 1.609)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("mph, and ");
                            for (int i = 0; i < Convert.ToString(lcat2 / 1.852).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat2 / 1.852)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat2 / 1.852).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat2 / 1.852)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("kt.");
                            Console.WriteLine("");
                            Console.WriteLine("");
                        }
                        break;
                    case ConsoleKey.D3:
                        if (text2)
                        {
                            for (int i = 0; i < cat[3].Length; i++)
                            {
                                Console.Write(cat[3][i]);
                                Thread.Sleep(15);
                            }
                            for (int i = 0; i < Convert.ToString(lcat3).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat3)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat3).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat3)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("km/h, ");
                            for (int i = 0; i < Convert.ToString(lcat3 / 1.609).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat3 / 1.609)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat3 / 1.609).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat3 / 1.609)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("mph, and ");
                            for (int i = 0; i < Convert.ToString(lcat3 / 1.852).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat3 / 1.852)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat3 / 1.852).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat3 / 1.852)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("kt.");
                            Console.WriteLine("");
                            Console.WriteLine("");
                        }
                        break;
                    case ConsoleKey.D4:
                        if (text2)
                        {
                            for (int i = 0; i < cat[4].Length; i++)
                            {
                                Console.Write(cat[4][i]);
                                Thread.Sleep(15);
                            }
                            for (int i = 0; i < Convert.ToString(lcat4).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat4)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat4).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat4)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("km/h, ");
                            for (int i = 0; i < Convert.ToString(lcat4 / 1.609).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat4 / 1.609)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat4 / 1.609).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat4 / 1.609)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("mph, and ");
                            for (int i = 0; i < Convert.ToString(lcat4 / 1.852).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat4 / 1.852)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("-");
                            for (int i = 0; i < Convert.ToString(hcat4 / 1.852).Length; i++)
                            {
                                Console.Write(Convert.ToString(hcat4 / 1.852)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("kt.");
                            Console.WriteLine("");
                            Console.WriteLine("");
                        }
                        break;
                    case ConsoleKey.D5:
                        if (text2)
                        {
                            for (int i = 0; i < cat[5].Length; i++)
                            {
                                Console.Write(cat[5][i]);
                                Thread.Sleep(15);
                            }
                            for (int i = 0; i < Convert.ToString(lcat5).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat5)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("km/h, ");
                            for (int i = 0; i < Convert.ToString(lcat5 / 1.609).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat5 / 1.609)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("mph, and ");
                            for (int i = 0; i < Convert.ToString(lcat4 / 1.852).Length; i++)
                            {
                                Console.Write(Convert.ToString(lcat4 / 1.852)[i]);
                                Thread.Sleep(15);
                            }
                            Console.Write("kt.");
                            Console.WriteLine("");
                            Console.WriteLine("");
                        }
                        break;
                    case ConsoleKey.D6:
                        for (int i = 0; i < cat[6].Length; i++)
                        {
                            Console.Write(cat[6][i]);
                            Thread.Sleep(15);
                        }
                        chosen = true;
                        break;
                    default:
                        for (int i = 0; i < cat[7].Length; i++)
                        {
                            Console.Write(cat[7][i]);
                            Thread.Sleep(15);
                        }
                        Console.WriteLine("");
                        Console.WriteLine("");
                        break;
                }
            }

            Thread.Sleep(5000);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

        private static void Leave()
        {
            Console.Clear();
            string main = "[You drift off back into a deep slumber...]";

            for (int i = 0; i < main.Length; i++)
            {
                Console.Write(main[i]);
                Thread.Sleep(15);
            }

            Thread.Sleep(5000);
            Environment.Exit(0);
        }

        private static void BankDeposit()
        {
            bop = 0;
            giveMoney = true;
            Console.Clear();
            tick = 0;
            string tengo = "You have ";
            string[] sdeposit = { "How much would you like to deposit (#)? Kč", "Press any key to return to the menu.", "This is an invalid amount.  If you have no money to deposit, then you can press any key to return to the menu, or wait eleven seconds to enter a valid amount." };
            string amount = "";
            string[] asset = { ".", "Kč" };
            double ddeposit;
            invtimer.Elapsed += new ElapsedEventHandler(DepositTimer); //timer that tracks how long a user is idle if they make an invalid selction
            invtimer.Interval = 1000;
            Console.WriteLine(tengo + asset[1] + pocketChange + asset[0]);

            for (int i = 0; i < sdeposit[0].Length; i++)
            {
                Console.Write(sdeposit[0][i]);
                Thread.Sleep(15);
            }

            while (giveMoney)
            {
                Console.CursorVisible = true;
                Console.SetCursorPosition(sdeposit[0].Length, 1);
                for (int i = 0; i <= amount.Length; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(0, 3);
                for (int i = 0; i <= sdeposit[2].Length * 2; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(sdeposit[0].Length, 1);
                amount = Console.ReadLine();
                Console.CursorVisible = false;
                bool parse = double.TryParse(amount, out ddeposit);

                Console.WriteLine("");

                if (parse)
                {
                    if (ddeposit <= pocketChange)
                    {
                        pocketChange = pocketChange - ddeposit;
                        totalBalance = totalBalance + ddeposit;
                        Console.WriteLine(asset[1] + ddeposit + " has successfully been deposited into your account.");
                        Console.WriteLine("");
                        Thread.Sleep(1000);
                        for (int i = 0; i < sdeposit[1].Length; i++)
                        {
                            Console.Write(sdeposit[1][i]);
                            Thread.Sleep(15);
                        }
                        Console.ReadKey();
                        giveMoney = false;
                    }
                    else
                    {
                        for (int i = 0; i < sdeposit[2].Length; i++)
                        {
                            Console.Write(sdeposit[2][i]);
                            Thread.Sleep(15);
                        }
                        invtimer.Enabled = true;
                        while (tick < 10000)
                        {
                            Console.ReadKey();
                            if(tick < 11000)
                            {
                                invtimer.Enabled = false;
                                giveMoney = false;
                                tick = 100000;
                            }
                        }
                        
                        tick = 0;
                    }
                }
                else
                {
                    for (int i = 0; i < sdeposit[2].Length; i++)
                    {
                        Console.Write(sdeposit[2][i]);
                        Thread.Sleep(15);
                    }
                    invtimer.Enabled = true;
                    while (tick < 10000)
                    {
                        Console.ReadKey();
                        if (tick < 11000)
                        {
                            invtimer.Enabled = false;
                            giveMoney = false;
                            tick = 100000;
                        }
                    }
                    tick = 0;
                }
                tick = 0;
            }
            if (totalBalance >= 0.75)
                totalBalance = totalBalance - 0.75;
            else
                broke = true;
            tick = 0;

            Console.Clear();
        }

        private static void BankWithdraw()
        {
            bop = 1;
            giveMoney = true;
            Console.Clear();
            tick = 0;
            string[] swithdraw = { "How much would you like to withdraw (#)? Kč", "Press any key to return to the menu.", "This is an invalid amount.  If you have no money to withdraw, then you can press any key to return to the menu, or wait eleven seconds to enter a valid amount." };
            string amount = "";
            string[] asset = { ".", "Kč" };
            double dwithdraw;
            invtimer.Elapsed += new ElapsedEventHandler(DepositTimer); 
            invtimer.Interval = 1000;

            for (int i = 0; i < swithdraw[0].Length; i++)
            {
                Console.Write(swithdraw[0][i]);
                Thread.Sleep(15);
            }

            while (giveMoney)
            {
                Console.CursorVisible = true;
                Console.SetCursorPosition(swithdraw[0].Length, 1 - bop);
                for (int i = 0; i <= amount.Length; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(0, 3 - bop);
                for (int i = 0; i <= swithdraw[2].Length * 2; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(swithdraw[0].Length, 0);
                amount = Console.ReadLine();
                Console.CursorVisible = false;
                bool parse = double.TryParse(amount, out dwithdraw);

                Console.WriteLine("");

                if (parse)
                {
                    if (dwithdraw <= totalBalance)
                    {
                        pocketChange = pocketChange + dwithdraw;
                        totalBalance = totalBalance - dwithdraw;
                        Console.WriteLine(asset[1] + dwithdraw + " has successfully been withdrawn from your account.");
                        Console.WriteLine("");
                        Thread.Sleep(1000);
                        for (int i = 0; i < swithdraw[1].Length; i++)
                        {
                            Console.Write(swithdraw[1][i]);
                            Thread.Sleep(15);
                        }
                        Console.ReadKey();
                        giveMoney = false;
                    }
                    else
                    {
                        for (int i = 0; i < swithdraw[2].Length; i++)
                        {
                            Console.Write(swithdraw[2][i]);
                            Thread.Sleep(15);
                        }
                        invtimer.Enabled = true;
                        while (tick < 10000)
                        {
                            Console.ReadKey();
                            if (tick < 11000)
                            {
                                invtimer.Enabled = false;
                                giveMoney = false;
                                tick = 100000;
                            }
                        }
                        tick = 0;
                    }
                }
                else
                {
                    for (int i = 0; i < swithdraw[2].Length; i++)
                    {
                        Console.Write(swithdraw[2][i]);
                        Thread.Sleep(15);
                    }
                    invtimer.Enabled = true;
                    while (tick < 10000)
                    {
                        Console.ReadKey();
                        if (tick < 11000)
                        {
                            invtimer.Enabled = false;
                            giveMoney = false;
                            tick = 100000;
                        }
                    }
                    tick = 0;
                }
                tick = 0;
            }
            tick = 0;
            if (totalBalance >= 0.75)
                totalBalance = totalBalance - 0.75;
            else
                broke = true;
            Console.Clear();
        }

        private static void BankBill()
        {
            Console.Clear();
            string[] bills = { "You currently have unpaid bills: ", 
                "1. Kč2100000 to be paid to Sir Gargolomew.", 
                "2. Your monthly Kč6000 subscription to the \"Joke Man and his pet monkey\" magazine.", 
                "You will be paying through your current account balance.  If you don't want to pay any bills, press the 3 key.", 
                "Which bill would you like to pay (#)? ",
                "Your bill has been paid!  Press any key to return to the menu.", 
                "You have insufficient funds to pay the bill.  Press any key to return to the menu.",
                "Press any key to return to the menu.", 
                "You have paid all outstanding bills."};
            int[] ibills = { 90000, 1200000, 6000 };
            bool quieres = true, chosen = false;
            string invalid = "That is not a valid choice.";
            int j = 0;

            if (payed1 && payed2)
            {
                chosen = true;
                for (int i = 0; i < bills[8].Length; i++)
                {
                    Console.Write(bills[8][i]);
                    Thread.Sleep(15);
                }
                Thread.Sleep(1000);
                Console.WriteLine("");
                for (int i = 0; i < bills[7].Length; i++)
                {
                    Console.Write(bills[7][i]);
                    Thread.Sleep(15);
                }
            }
            else
            {
                for (int i = 0; i < bills[0].Length; i++)
                {
                    Console.Write(bills[0][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");

                while (!chosen)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    while (quieres)
                    {
                            for (int i = 0; i < bills[2].Length; i++)
                            {
                                if (payed1)
                                    j = 1;
                                else
                                {
                                    if (i < bills[1].Length)
                                    {
                                        Console.SetCursorPosition(0 + i, 2);
                                        Console.Write(bills[1][i]);
                                    }
                                }
                            if (!payed2)
                            {
                                if (i < bills[2].Length)
                                {
                                    Console.SetCursorPosition(0 + i, 3 - j);
                                    Console.Write(bills[2][i]);
                                }
                            }
                                Thread.Sleep(15);

                                if (i == bills[2].Length - 1)
                                    quieres = false;
                            }
                    }
                    Console.SetCursorPosition(0, 4);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    for (int i = 0; i < bills[3].Length; i++)
                    {
                        Console.Write(bills[3][i]);
                        Thread.Sleep(15);
                    }
                    Console.WriteLine("");
                    Thread.Sleep(500);
                    for (int i = 0; i < bills[4].Length; i++)
                    {
                        Console.Write(bills[4][i]);
                        Thread.Sleep(15);
                    }

                    Console.CursorVisible = true;
                    cki = Console.ReadKey();
                    Console.CursorVisible = false;

                    Console.WriteLine("");
                    if (cki.Key == ConsoleKey.D1)
                    {
                        if (!payed1)
                        {
                            chosen = true;
                            if (totalBalance >= ibills[0])
                            {
                                for (int i = 0; i < bills[5].Length; i++)
                                {
                                    Console.Write(bills[5][i]);
                                    Thread.Sleep(15);
                                }
                                totalBalance = totalBalance - ibills[0];
                                payed1 = true;
                            }
                            else
                            {
                                for (int i = 0; i < bills[6].Length; i++)
                                {
                                    Console.Write(bills[6][i]);
                                    Thread.Sleep(15);
                                }
                            }
                        }
                        else if (payed1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            for (int i = 0; i < invalid.Length; i++)
                            {
                                Console.Write(invalid[i]);
                                Thread.Sleep(15);
                            }
                        }
                    }
                    else if (cki.Key == ConsoleKey.D2)
                    {
                        if (!payed2)
                        {
                            chosen = true;
                            if (totalBalance >= ibills[1])
                            {
                                for (int i = 0; i < bills[5].Length; i++)
                                {
                                    Console.Write(bills[5][i]);
                                    Thread.Sleep(15);
                                }
                                totalBalance = totalBalance - ibills[1];
                                payed2 = true;
                            }
                            else
                            {
                                for (int i = 0; i < bills[6].Length; i++)
                                {
                                    Console.Write(bills[6][i]);
                                    Thread.Sleep(15);
                                }
                            }
                        }
                        else if (payed2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            for (int i = 0; i < invalid.Length; i++)
                            {
                                Console.Write(invalid[i]);
                                Thread.Sleep(15);
                            }
                        }
                    }
                    else if (cki.Key == ConsoleKey.D3)
                    {
                        chosen = true;
                        for (int i = 0; i < bills[7].Length; i++)
                        {
                            Console.Write(bills[7][i]);
                            Thread.Sleep(15);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        for (int i = 0; i < invalid.Length; i++)
                        {
                            Console.Write(invalid[i]);
                            Thread.Sleep(15);
                        }
                    }
                }
            }

            Console.ReadKey();

            if (totalBalance >= 0.75)
                totalBalance = totalBalance - 0.75;
            else
                broke = true;
            Console.Clear();
        }

        private static void BankBalance()
        {
            Console.Clear();
            string[] cbalance = { "Your current balance is Kč", "Press any key to return to the menu." };

            for (int i = 0; i < cbalance[0].Length; i++)
            {
                Console.Write(cbalance[0][i]);
                Thread.Sleep(15);
            }
            for (int i = 0; i < Convert.ToString(totalBalance).Length; i++)
            {
                Console.Write(Convert.ToString(totalBalance)[i]);
                Thread.Sleep(15);
            }
            Console.Write(".");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(2000);
            for (int i = 0; i < cbalance[1].Length; i++)
            {
                Console.Write(cbalance[1][i]);
                Thread.Sleep(15);
            }
            Console.ReadKey();

            if (totalBalance >= 0.75)
                totalBalance = totalBalance - 0.75;
            else
                broke = true;
            Console.Clear();
        }

        static void Main(string[] args)
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND); //You can't resize the console anymore, so the program cannot be bugged.
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            string[] main = { "{After getting shot by the joke monkey, you fell into a deep sleep]", "[Memories of a distant past begin flooding into your brain]", "Which memory would you like to recall?" };
            string[] option = { "Choice (#): ", "1. Recall the bank memory", "2. Recall the parking garage memory", "3. Recall the hurricane memory", "4. Return to a deep sleep" };
            string drag = "Drag the window to the top left corner of the screen.  Press any key when you have finished.";
            if (!visited)
            {
                for (int i = 0; i < drag.Length; i++)
                {
                    Console.Write(drag[i]);
                    Thread.Sleep(15);
                } 
                //You will see a lot of these for loops.  I never thought of making them a method...  This will be implemented in the future, but I like these loops because it makes the text scroll and it looks cool.
                Console.ReadKey();
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                for (int i = 0; i < main[0].Length; i++)
                {
                    Console.Write(main[0][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(500);
                for (int i = 0; i < main[1].Length; i++)
                {
                    Console.Write(main[1][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(500);
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < main[2].Length; i++)
                {
                    Console.Write(main[2][i]);
                    Thread.Sleep(15);
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Thread.Sleep(500);
            }

            bool chosen = false;
            bool quieres = true;
            int j;

            while (!chosen) //menu
            {
                if (!visited)
                    j = 0;
                else
                    j = 7;
                while (quieres) //prints the text all at the same time
                {
                    for (int i = 0; i < option[2].Length; i++)
                    {

                        if (i < option[1].Length)
                        {
                            Console.SetCursorPosition(0 + i, 7 - j);
                            Console.Write(option[1][i]);
                        }

                        if (i < option[2].Length)
                        {
                            Console.SetCursorPosition(37 + i, 7 - j);
                            Console.Write(option[2][i]);
                        }

                        if (i < option[3].Length)
                        {
                            Console.SetCursorPosition(0 + i, 9 - j);
                            Console.Write(option[3][i]);
                        }
                        if (i < option[4].Length)
                        {
                            Console.SetCursorPosition(37 + i, 9 - j);
                            Console.Write(option[4][i]);
                        }

                        Thread.Sleep(15);

                        if (i == option[2].Length - 1)
                            quieres = false;
                    }
                }

                Console.SetCursorPosition(0, 11 - j);

                Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < option[0].Length; i++)
                {
                    Console.Write(option[0][i]);
                    Thread.Sleep(15);
                }

                Console.CursorVisible = true;

                string invalid = "That is not a valid choice.";

                cki = Console.ReadKey(); //more convenient than a ReadLine

                Console.CursorVisible = false;

                if (cki.Key == ConsoleKey.D1)
                {
                    Bank();
                }

                else if (cki.Key == ConsoleKey.D2)
                {
                    Garage();
                }

                else if (cki.Key == ConsoleKey.D3)
                {
                    Hurricane();
                }

                else if (cki.Key == ConsoleKey.D4)
                {
                    Leave();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    for (int i = 0; i < invalid.Length; i++)
                    {
                        Console.Write(invalid[i]);
                        Thread.Sleep(15);
                    }
                }
                quieres = true;
            }   
        }

        private static void TACTimer(object source, ElapsedEventArgs e)
        {
            string termsAndConditions = @"    TERMS AND CONDITIONS: 
*1. The Practical Plan allows you to have one Canadian or U.S. Dollar Primary Chequing Account or Interest Chequing Account (existing customers) and one Premium Rate Savings Account under the Plan. All other Bank Plans allow you to have up to a total of 20 eligible accounts in Canadian or U.S. dollars, any combination of Primary Chequing Accounts, Premium Rate Savings Accounts, and Interest Chequing Accounts (existing customers only) under the Plan.
* 2.Per - item fees will apply when you exceed the number of transactions included in your Plan.
* 3.You can have up to twenty(20) Canadian or U.S.Dollar Primary Chequing, Premium Rate Savings or Interest Chequing Accounts(for existing customers only) excluding Savings Amplifier Accounts, Smart Saver Accounts(for existing customers only) and Savings Builder Account under one(1) Plan.One(1) account must be designated as the lead account. The lead account is the one you designate to pay any fees required by your Bank Plan, for example, your monthly Plan fees and transaction fees.If you select a U.S.dollar account as the lead account, all Plan and transaction fees will be charged in U.S.dollars.All accounts covered by the Plan are subject to the monthly transaction limit(s) for the selected Plan
* 4.The monthly Bank Plan fee(excluding the AIR MILES Plan and Practical Plan) can be eliminated by maintaining the minimum monthly balance indicated at all times in a Primary Chequing account, which has been designated as the lead account for your Bank Plan.The lead account is the one you designate to pay any fees required by your Bank Plan for example, your monthly Plan fees and transaction fees.You are responsible for all transactions, services and product fees not included in the Bank Plan.
* 5.You are responsible for all fees relating to any transactions, services and products not included in your Bank Plan.
* 6.The monthly Plan fee is waived.You are responsible for all transaction, service and product fees not included in your Bank Plan.
* 7.This account is no longer available with pay - per - use pricing and must now be included in an Everyday Bank Plan.This information is for existing customers without an Everyday Bank Plan.
* 8.Your Primary Chequing Account or Interest Chequing Account(for existing customers) must be your lead account.The lead account is the one you designate to pay any fees required by your Bank Plan, for example, your monthly Plan fees and transaction fees.Get AIR MILES Reward Miles when you make debit card purchases minus refunds from your Primary Chequing Account.The number of Miles will be rounded down to the nearest whole number.Fractions of Miles will not be awarded.
* 9.Award of AIR MILES reward miles is made for debit card purchases minus refunds from your Primary Chequing Account when your Primary Chequing Account or Interest Chequing Account(existing customers) is the lead account for the AIR MILES Plan.The lead account is the one you designate to pay any fees required by your Bank Plan, for example, your monthly Plan fees and transaction fees.The number of Reward Miles will be rounded down to the nearest whole number.Fractions of Reward Miles will not be awarded.
* 10.You will receive a rebate on the credit card annual fee if you are the primary cardholder of an eligible BMO credit card and a lead accountholder of a Premium Plan. A rebate of $150(the “Rebate”) will apply to the BMO Ascend World Elite Mastercard and a $120 rebate(the “Rebate”) will apply to the BMO AIR MILES World Elite Mastercard, BMO CashBack World Elite Mastercard or BMO eclipse Visa Infinite Card(each a “BMO Credit Card”). You will receive this rebate each year with a BMO Premium Plan provided you continue to meet the Terms and Conditions. Limit of one Rebate per Premium Plan, per person. If the lead account is joint, and both accountholders have a BMO Credit Card, the Rebate will be applied to the BMO Credit Card with the higher annual fee. Credit Cards issued through BMO Nesbitt Burns and BMO Private Banking are not eligible for the Rebate. Your BMO Credit Card account and your BMO Account must be in good standing at the time the Rebate is applied.Full details are available online at bmo.com / PremiumRebate.
* 11.Subject to the Monthly Plan Limit.
* 12.Interest is calculated on the daily account closing balance and paid monthly on the last business day of the month.Interest is paid in the same currency as the account.Interest rates are calculated on a per annum basis.Interest rates and balance tiers are subject to change without advance notice.Interest rates are available online at bmo.com / rates, at all BMO branches where deposit accounts are kept and by calling 1 - 877 - 225 - 5266.
* 13.Interest is calculated on the daily account closing balance and paid monthly on the last business day of the month.Interest is paid in the same currency as the account.Interest rates are calculated on a per annum basis.The bonus interest rate & posted rate may change any time without any prior notice.Posted rate to be applied on Savings Amplifier Account after the offer period.
* 14.When comparing the BMO Savings Builder Account with other savings accounts available on the public websites of CIBC, TD Canada Trust, RBC Royal Bank, Scotiabank, Tangerine, PC Financial as of March 15, 2017.
Each additional transfer is $5.00. Bill payments, Pre-Authorized Payments, debit card purchases, Interac e-Transfer, Interac Online purchases and cheques are not permitted on this account.
The interest rate of 2.50% is comprised of a base interest rate of 0.500% plus a regular bonus interest rate of 2.000%. The base interest rate applies to the entire balance in your Savings Builder Account (“Account”). The bonus interest rate applies to your Account balance up to $250,000 each month in which you increase your monthly Account balance by $200.00 or more. To determine if you are eligible for bonus interest in a month, we will calculate the difference between the closing balance on the last business day of the previous month and the closing balance on the last business day of the current month excluding base interest from the current month, bonus interest and any promotional interest credited from the previous month and any withdrawal or transfer fees, and that difference must be at least $200.00. The base interest is calculated on the daily closing balance and paid monthly on the last business day of the month. The bonus interest is calculated on the daily closing balance and paid monthly on the second business day of the following month. The bonus interest is calculated from the first business day of the month, regardless of which day you may have qualified for bonus interest. The base interest rate and bonus interest rate and balance tiers may be changed at any time without notice. Current interest rates are available at BMO branches where accounts are serviced, at bmo.com/rates and by calling 1-877-225-5266.
*15. You will receive an annual credit card fee rebate up to $40 (the “Rebate”) if you are the primary cardholder of an eligible BMO credit card and a lead accountholder of a Performance Plan.A rebate of $40 will apply to the following eligible credit cards:  BMO AIR MILES World Elite Mastercard; BMO CashBack World Elite Mastercard; BMO Ascend World Elite Mastercard; BMO AIR MILES World Mastercard; BMO CashBack World Mastercard; BMO Affinity AIR MILES World Mastercard; BMO Affinity CashBack World  Mastercard or BMO eclipse Visa Infinite Card and rebate of $20 will apply to the BMO Preferred Rate Mastercard(each a “BMO Credit Card”). You will receive this rebate each year with a Performance Plan provided you continue to meet the Terms and Conditions.   Limit of one Rebate per Performance Plan, per person. If the lead account is joint, and both accountholders have a BMO Credit Card, the Rebate will be applied to the BMO Credit Card with the higher annual fee.BMO employee Credit Cards and Credit Cards issued through BMO Nesbitt Burns and BMO Private Banking clients are not eligible for the Rebate. Your BMO Credit Card account and your BMO Account must be in good standing at the time the Rebate is applied.Full details are available online at bmo.com/PerformanceRebate.
*16. The base interest rate applies to the entire balance in a Savings Builder Account (“Account”). You will earn bonus interest on your Account balance up to $250,000.00, each month in which you increase your monthly Account balance by $200 or more. To determine if you are eligible for bonus interest in a month, we will calculate the difference between the closing balance on the last business day of the previous month and the closing balance on the last business day of the current month (excluding base interest from the current month, bonus interest credited from the previous month and any withdrawal or transfer fees); that difference must be at least $200. The base interest is calculated on the daily closing balance and paid monthly on the last business day of the month.The bonus interest is calculated on the daily closing balance and paid monthly on the second business day of the following month.The bonus interest is calculated from the first business day of the month, regardless of which day you qualified for bonus interest. The base interest rate and bonus interest rate each may be changed at any time without notice.Current interest rates are available at BMO branches where accounts are serviced, at bmo.com/rates and by calling 1-877-225-5266.
*17. We may change the monthly Account balance increase amount required to earn Bonus Interest at any time.
*18. Paid on the entire balance once the minimum balance is reached.
*19. Paid on the portion of the balance within this tier.
*20. Subject to credit approval. Interest will be calculated on the daily overdraft balance at prevailing overdraft interest rates and charged to the account on the last business day of the month.
The Overdraft Protection Standard monthly fee is waived if you have selected the Premium Plan.
Overdraft Protection fees apply individually to each account with an authorized limit within the Plan.
*21. Subject to credit approval. Interest will be calculated on the daily overdraft balance at prevailing overdraft interest rates and charged to the account on the last business day of the month.
The Overdraft Protection Occasional per-item fee is waived if you have selected the Premium Plan.
Overdraft Protection fees apply individually to each account with an authorized limit within the Plan.
The Overdraft per-item fee applies to each item that creates or increases overdraft.For cheques and pre-authorized debits, we process debit transactions against accounts in the order in which we receive them via the clearing system. Overdraft per item charges will be incurred in the same order as items are received from the clearing system.
*22. You are required to provide initial set-up instructions for this service.The Overdraft Transfer Service fee is in addition to any debit transaction fees. Per transfer fee is waved if you have selected the Premium Plan and for customers with the Student and Recent Graduate or Seniors Discounted Banking Program.
*23. Fee will apply for each additional transfer and for each withdrawal. Refer to Agreements, Bank Plans and Fees for Everyday Banking.
*24. You are required to provide initial set-up instructions for this service.
*25. Bill payments, point-of-sale transactions, and cheques are not permitted on this account.
*26. This account cannot be included as part of an Everyday Bank Plan.
*27. A bill payment handling fee of $1.50 may also apply where applicable.
*28. Additional fees may be charged by the merchant for use of point-of-sale devices.
*29. One free funds transfer out of the account to another BMO account via BMO ATM, BMO Telephone Banking, BMO Digital Banking, at a branch, with a customer contact associate or by automatic transfer out of the account to another BMO account is allowed per month.A $5.00 fee applies for each additional transfer.
*30. These fees apply when you complete transactions through the interactive voice response (IVR) system. Assisted-service fees will apply when transactions are completed with a customer contact associate.
*31. Passbooks are no longer offered and are only available to existing customers who have this service.
*32. There is no charge to access BMO Mobile Banking.Transactions completed through BMO Mobile Banking may be subject to transaction fees depending on your bank account or Bank Plan. Your mobile carrier or service provider may charge fees when you access BMO Mobile Banking.Check with your mobile carrier or hardware provider if you have questions about your specific device.
*33. BMO Online Banking and BMO Mobile Banking may not be available in all countries.
*34. You must be registered for BMO Online Banking before you can use BMO Mobile Banking.
*15. Preferred exchange rate for U.S.dollar transfer(s) using BMO Digital Banking services(up to US$5,000 daily)
*36. Subject to interruptions in telecommunications or online systems or in power supply or any other factor or event beyond the control of BMO.
*37. Transaction limits apply to orders placed by BMO Telephone Banking and BMO Online Banking.
*38. Recipients will receive an email notification when an Interac e-Transfer is available.There may be a delay between sending and receiving the money.Funds are credited to the recipient’s bank account immediately after they complete the transaction if they deposit the money online into a bank account at one of the participating financial institutions in Canada.Visit interac.ca for a list of participating institutions.
*39. Interac e-Transfer transactions are subject to maximum send and receive dollar amounts.For Bank Plans that do not include unlimited transactions, excess transaction fees may apply if you perform an Interac e-Transfer transaction over and above the number of transactions allowed for the Plan. A cancellation fee may still apply when you cancel the transaction.
*40. This fee is charged if you cancel an Interac e-Transfer unless you cancel it on the same day it is sent (Eastern Time). It is also charged when the Bank reverses an Interac e-Transfer regardless of whether you asked us to reverse it or not. This fee is in addition to the non-refundable Interac e-Transfer fee that may apply when you send an Interac e-Transfer, if applicable.
*41. Digital Banking Includes Mobile and Online Banking.
*42. BMO Alerts may not be available outside of Canada.Standard messaging and data charges may apply.
BMO Alerts may be delayed or prevented because of a variety of factors.We do not guarantee the delivery or the accuracy of the contents of any alert.By using BMO Alerts, you also agree that we will not be liable for any of the following: (i) any delays, failure to deliver, or misdirected delivery of any alert; (ii) for any errors in the content of an alert; or(iii) for any actions taken or not taken by you or any third party in reliance on an alert.BMO Alerts received as text messages on your mobile access device may incur a charge from your mobile access service provider.
*43. Terms, conditions and restrictions apply. Identification and proof of purchase required.
*44. Cheque images are not available with eStatements.
*45. You will be able to view, save and print cheque images from the previous three months through BMO Mobile Banking and BMO Online Banking. If in the future you require any copies of cheques that are no longer available through BMO Mobile Banking and BMO Online Banking you may request a copy at a BMO branch or by calling 1-877-225-5266. A per-item fee may apply. Cheques made payable to you or to cash, and cashed at any BMO branch, may not be returned but are described on your statement.
*46. One paper statement is included with the Practical Plan, Kids discounted banking program and Senior Plan (discontinued Bank Plan) at no charge.This fee will apply for each additional account statement.
*47. Fee is in addition to any debit transaction fees. The set-up fee is not applicable where the transfer is for automatic savings or regular contributions to a mutual fund, RSP or savings account.
*48. You must have a signed verbal/facsimile agreement with your branch before instructions can be accepted by telephone.
*49. Cost of personalized cheque orders for personal deposit accounts will depend on quantity and type selected.
*50. You are not able to put a stop payment on Pre-Authorized Debit for BMO credit products such as loans, mortgages, credit cards, lines of credit.
*51. Foreign currency items deposited and subsequently returned may incur a foreign currency loss due to rate fluctuations.
*52. Includes everyday banking transactions at a BMO branch, BMO ATM, BMO Telephone Banking, BMO Online Banking, BMO Mobile Banking, debit card purchases, cheques drawn on your account and Pre-Authorized Debits.
*53. Some non-BMO ATMs may charge you a convenience fee. The convenience fee is not a BMO fee and is added to the total amount of your withdrawal. You are responsible for the convenience fee that may be applied to your transaction.
*54. We and/or other financial institutions may charge additional fees for refunds or replacements of lost or stolen money orders or drafts.The receiving financial institution may charge associated fees. U.S.dollar money orders and drafts are available at no cost when purchased through a U.S.Dollar personal account.Foreign currency drafts are subject to sanctions compliance regulations and payee names are screened against persons listed under international sanctions. Depending on the result of that screening, a payee might not be able to cash a foreign currency draft.
*55. Applies to purchases made outside of Canada on the Maestro and Mastercard networks.Other transaction or network fees may apply.
*56. Five (5) monthly debit transactions using non-BMO ATMs on the Cirrus Network and unlimited monthly direct payment purchases at merchants using Maestro service.
*57. Per-transaction and cumulative limits may apply.Upon request, this feature can be deactivated.
*58. For purchases made through the Mastercard network, a hold may be put on your bank account in the amount of your purchase in Canadian dollars. The hold will be removed when the transaction is debited from your bank account.The exchange rate for converting foreign currency transactions to Canadian dollars is the rate charged to us by Mastercard International on the date the transaction is posted to your account, plus 2.5% for purchases and minus 2.5% for refunds.For foreign currency transactions other than U.S.dollars, the amount is first converted to U.S.dollars and then to Canadian dollars.
*59. Subject to the terms and conditions of your agreement for banking services. For complete details of your reporting obligations and responsibilities: personal banking customers should refer to the Electronic Banking Services Agreement part of the Agreements, Bank Plans and Fees for Everyday Banking; and business banking customers should refer to the BMO Debit Card for Business and Telephone Banking/Online Banking section of the Agreement for Business Banking.Both agreements are available online at bmo.com and at your local BMO branch.
*60. Fee does not apply for use of BMO Harris ATMs or Allpoint branded ATMs in the United States on the Cirrus network.Non-BMO ATMs in Canada and ATMs outside of Canada (excluding BMO Harris ATMs or Allpoint branded ATMs in the United States) may charge a convenience fee.The convenience fee is not a BMO fee and is added to the total amount of your withdrawal.You are responsible for the convenience fee that may be applied to your transaction. Per-item fees will apply when you exceed the number of transactions included in your Plan.
*61. Other financial institutions may have associated fees for incoming and outgoing wire transfers.Inquiries/traces etc. may be subject to fees other than those collected by BMO.Inquire in branch for details.When an investigation is requested, an investigation fee may be collected.
*62. 0.25% bonus is applied to the posted rate for non-redeemable 1-year to 5-years BMO Guaranteed Investment Certificate at the time of purchase and is not applicable to any other term or product, and cannot be combined with any other special rate offer. Posted and Bonus rates may be changed or withdrawn at any time. The GIC must be purchased at a BMO branch.Offers may be changed, withdrawn, or extended at any time without notice.
*63. Please check with your local BMO branch for availability.Receive a $60 cash bonus with the BMO NewStart Program when you open a Primary Chequing Account in the Performance Plan under the BMO NewStart Program and rent a safety deposit of any size. Not all sizes are available at all BMO branches.
*64. All sizes may not be available at all BMO branches. Fees for related services will be debited from your BMO deposit account. Sizes are measured in inches.
*65. For the Students discounted banking program, you must provide proof of registration. We may require proof of expected program completion, up to a maximum of four years, otherwise you may be required to provide annual proof of eligibility.Program benefits will end in the year in which you are scheduled to graduate (maximum of four years). To continue your eligibility for the Program, you must provide proof of expected program completion date or proof of registration by November 30 of the year in which your Program benefits end.Plan and other fees paid prior to confirming eligibility may not be refunded.
*66. The Senior Discounted Banking Program provides you with a monthly discount that can be applied to the Practical, Plus, Performance, AIR MILES or Premium Plan.To qualify for the Senior Discounted Banking Program you must be 60 years of age or older; or, if your account is joint, one of the accountholders is 60 years of age or older.The Senior discounted banking program will be applied automatically the month after you turn 60 if proof of age is on file and you are in an eligible Plan.The discounted banking program is not eligible on Plans no longer offered.
*67. If you are the beneficiary of an RDSP or, if you open the Plan to hold account(s) in trust for the beneficiary, you are eligible to receive a discount to the Premium, Performance, AIR MILES, Plus or Practical monthly Plan fee.To qualify, you are required to provide proof of eligibility before the discount will be applied to the monthly Plan fee.Additional fees paid before eligibility is confirmed may not be refunded.
*68. You are required to provide proof of eligibility once every three years otherwise the full monthly Plan fee will be applied automatically. Plan and other fees paid prior to confirming eligibility may not be refunded.
*69. To qualify for any special offer pertaining to the BMO NewStart program as set out above, a customer must be a permanent resident or foreign worker who arrived in Canada within the last five (5) years.Proof that status was obtained within the last five(5) years is required as evidenced by their Canadian Permanent Resident Card, Confirmation of permanent residency or work permit(IMM 1442). The Bank Plan offer included in the BMO NewStart program applies to the Performance Plan only.Other Bank Plans are available but are not included in the free banking offer. You are eligible to receive the full monthly Plan fee rebated for a twelve (12) month period and are responsible for all transaction, service and product fees not included in your Bank Plan.After the twelve(12) months of being enrolled in the BMO NewStart program, eligibility for the monthly Bank Plan fee waiver ends and the full monthly Bank Plan fee($16.95/month) will be applied to your account automatically.
*70. For any of the discounted banking plans, proof of eligibility is required.Customer is responsible for all the fees of any transactions, services and products not included in the Everyday Bank Plan.
*71. Teens discount is for 13 to 18 years of age.Teens are eligible to receive the Plus Plan with no monthly Plan fee or receive a discount equivalent to the Plus Plan fee from the Performance or Premium Plan fee. You are responsible for all fees relating to any transactions, services and products not included in your Bank Plan.On your 19th birthday, eligibility for the Teens discount ends and the full monthly Plan fee will apply.
*72. Kids discount is for children 12 years of age and under.Kids are eligible to receive the Plus Plan with no monthly Plan fee or receive a discount equivalent to the Plus Plan fee from the Performance or Premium Plan fee. You are responsible for all fees relating to any transactions, services and products not included in your Bank Plan.On your 13th birthday, your eligibility for the Kids discount ends and you will be included in the Teens Discounted Banking Program.
*73. Students at a full-time university, college or registered private vocational school are eligible to receive the Performance Plan with no monthly Plan fee or receive a discount equivalent to the Performance Plan fee from the Premium Plan fee.Recent Graduates from a full-time university, college or registered private vocational school are eligible to receive the Performance Plan with no monthly Plan fee or receive a discount equivalent to the Performance Plan fee from the Premium Plan fee for up to one year after graduation.You are responsible for all fees relating to any transactions, services, and products not included in your Bank Plan.Proof of eligibility is required.
*74. Plan, transaction, service and product fees may still apply.You’re eligible for OnGuard if you are a BMO customer who has a lead account* with one of the following Bank Plans: Performance Plan, Premium Plan, Platinum Plan or Employee Plan.In addition, to qualify for OnGuard, you must be a Canadian resident who has reached the age of majority for your province or territory, and you must be registered for BMO Online Banking and/or Mobile Banking and you must have a valid email address on your BMO profile and your lead account must be in good standing. Customers with an eligible Bank Plan with the Kids or Teens discounted banking program do not qualify for OnGuard.All accountholders of a lead account with an eligible Bank Plan qualify for OnGuard provided they meet the above eligibility requirements. If you switch your lead account to an ineligible Bank Plan, then you will no longer qualify for the OnGuard service.OnGuard retail value is $155.88 annually (charged at $12.99 per month). OnGuard is provided by Sigma Loyalty Group and Intersections Inc.Terms and conditions can be found at www.bmo.com/onguard/SLGconditions.
* The lead account is the one you designate to pay any fees required by your Bank Plan, for example, your monthly Plan fees and transaction fees.
*75. Must meet eligibility requirements. Refer to the FAQs on bmo.com/onguard for details.
*76. Plan, transaction, service and product fees may still apply.
*76.1 All other terms and conditions do not apply except for 76.1 and 76.2, as this is Bank Bank.
*76.2 Each session is limited to one transaction, and a transaction fee of Kč0.75 will be charged once the session concludes.  If an invalid option is selected, Kč0.75 will be deducted from your account, and deposited directly into the account of Gargolomew the Greedy.
*77. The discount will be applied to the Plan fee after proof of eligibility is provided.Plan and other fees paid prior to confirming eligibility may not be refunded.
*78. You are required to provide proof of eligibility by November 30 of the year you graduate.If proof is not provided by November 30, the Student Program benefits will automatically end and the full monthly Plan fee will be applied starting in December.Fees charged prior to providing proof of eligibility may not be refunded.
*79. The monthly Plan fee is waived.You are responsible for all transaction, service and product fees not included in your Bank Plan.Under the CDCB Kids (up to 12 years old) discounted banking program, you can receive the Plus Plan at no cost.For the CDCB Teens(ages 13 to 18) and CDCB Students discounted banking program, you can receive the Performance Plan at no cost.For the CDCB Student discounted program, the discount will be applied to the Plan fee after proof of eligibility is provided.Plan and other fees paid prior to confirming eligibility may not be refunded.
*80. Proof of CDCB eligibility is required.Offers may be modified or withdrawn by BMO at any time without notice.The monthly Performance Plan fee is waived.You, the customer, are responsible for all transaction, service, and product fees not included in the Plan.
*81. Applications and the amount you can borrow are subject to meeting BMO’s usual credit criteria. Some conditions may apply. These special offers are not available for the 5-year or 10-year BMO Smart Fixed Mortgage or a Homeowner ReadiLine.To qualify for the CDCB special rates on 5-year fixed and 5-year variable rate mortgage, you must have a Canadian Dollar Primary Chequing Account (Chequing Account) with a CDCB Performance or Premium Plan; and set up the Chequing Account as the funding account for the BMO Mortgage; and have one(1) recurring direct deposit into the Chequing Account.
*82. Proof of CDCB eligibility is required.Offers may be changed or withdrawn at any time without notice.Applications and the amount you can borrow are subject to meeting BMO’s usual credit criteria.You must be a Canadian citizen or landed immigrant enrolled in a Canadian or non-Canadian post-secondary school or university. Co-signer may be required. Subject to credit availability and verification of identity.
*83. Seniors discount is for 60 years of age and older.Seniors are eligible to receive the Practical Plan with no monthly Plan fee or receive a discount equivalent to the Practical Plan fee from the Plus, Performance, AIR MILES or Premium Plan fee. You are responsible for all fees relating to any transactions, services, and products not included in your Bank Plan.
*84. For illustration purposes only. The savings of $203 assumes the following: (i) one monthly Performance Plan fee of $16.95 per month ($203.00 per year) is shared between you and your spouse or partner, rather than each person paying a separate monthly Plan fee of $16.95, (ii) no other per-item or over-limit transaction fees apply, and (iii) the account is not eligible for the monthly balance Plan fee waiver during the twelve-month period.
*85. For illustration purposes only. The savings of $360 assumes the following: (i) one monthly Premium Plan fee of $30.00 per month ($360.00 per year) is shared between you and your family member, rather than each person paying a separate monthly Plan fee of $30.00, (ii) no other per-item or over-limit transaction fees apply, and (iii) the account is not eligible for the monthly balance Plan fee waiver during the twelve-month period.
*86. No-fee daily banking under our Family Bundle is available to all family members who reside at the same household as the customer with a Lead Account on an eligible Performance or Premium Bank Plan. The “Lead Account” is the one you designate to pay a standard fee per month for eligible no-fee daily banking Bank Plans (Performance: $16.95/month, Premium: $30.00/month) unless a minimum balance is maintained, in which case the monthly Bank Plan fee is waived.Note that the balance in a family member’s account does not count toward the minimum balance for the monthly Bank Plan fee waiver.No-fee daily banking with our Family Bundle may be added at no additional fee and is capped at 20 accounts (held jointly or individually) in Canadian or U.S.dollars per eligible Bank Plan.Family Bundle option is not available on Practical Plan.
For full terms and conditions please visit www.bmo.com/familybundle, and read our Agreements, Bank Plans and Fees for Everyday Banking.
*87. No-fee daily banking with our Family Bundle offers an unlimited number of transactions and Interac e-Transfers transactions per month provided the daily Interac e-Transfer total does not exceed $3,000. Transactions include everyday banking transactions at a BMO branch, BMO ATM, BMO Telephone Banking, BMO Online Banking, BMO Mobile Banking , debit card purchases, cheques drawn on your account and Pre-Authorized Payments. Other service and product fees may still apply.
*88. Some billers, including financial institutions, do not accept the use of PowerSwitch service for transfers, including pay cheques.Financial institutions that do, may request confirmation of the transfer from the customers.
*89. Fee applies for debit transaction and/or account history inquires in excess of monthly transaction limit.Account histories completed through BMO Mobile, BMO Online Banking and the telephone interactive voice response (IVR) system do not count towards the monthly transaction limit.
*90. No-fee daily banking with our Family Bundle offers an unlimited number of transactions and Interac e-Transfer transactions per month provided the daily Interac e-Transfer total does not exceed $3,000. Transactions include everyday banking transactions at a BMO branch, BMO ATM, BMO Telephone Banking, BMO Online Banking, BMO Mobile Banking, debit card purchases, cheques drawn on your account and Pre-Authorized Payments. Other service and product fees may still apply.
*91. For illustration purposes only. The savings value displayed assumes the following: (i) one monthly Performance Plan fee of $16.95 per month ($203.00 per year) is shared between you and your family member(s), rather than each person paying a separate monthly Plan fee of $16.95, (ii) no other per-item or over-limit transaction fees apply, and (iii) the account is not eligible for the monthly balance Plan fee waiver during the twelve-month period.
*92. For illustration purposes only. The savings value displayed assumes the following: (i) one monthly Premium Plan fee of $30.00 per month ($360.00 per year) is shared between you and your family member(s), rather than each person paying a separate monthly Plan fee of $30.00, (ii) no other per-item or over-limit transaction fees apply, and (iii) the account is not eligible for the monthly balance Plan fee waiver during the twelve-month period.
*93. You are eligible to receive the full monthly Plan fee rebated for a twelve (12) month period and are responsible for all transaction, service and product fees not included in your Performance Plan chequing account.After the twelve(12) months of opening a new Performance Plan chequing account and being enrolled in the BMO Indigenous Personal Banking program, eligibility for the monthly Performance Plan chequing account fee waiver ends and the full monthly Performance Plan chequing account fee($16.95/month) will be applied to your account automatically.
*94. Interest will be calculated on the daily overdraft balance and charged to the account at the end of the month.Interest rates are available online at bmo.com/rates, at all BMO branches where deposit accounts are kept and by calling 1-877-225-5266.
*95. The bonus interest rate is applied to the posted rate at the time of purchase of a non-redeemable 1- year up to a 5-year BMO Guaranteed Investment Certificate (GIC) and is not applicable to any other term or product, and cannot be combined with any other special rate offer. Posted and bonus interest rates may be changed at any time.The GIC must be purchased at a BMO branch or with an associate at the Customer Contact Centre. Offers may be changed, withdrawn or extended at any notice.
*96. The transfer fee is waived when the transaction is completed through an eligible account with the Premium Plan or the Performance Plan in the BMO NewStart Program.
*97. In most instances, transactions are completed within two to five business days.As with any wire transfer-like transaction, funds may be rejected, held, or delayed because of incomplete or inaccurate information submitted; and for regulatory, legal, fraud, and/ or anti-money laundering/ anti-terrorist financing screening processes.The timing of the deposit and availability of funds by the recipient may vary with each receiving institution.These circumstances and delays are outside of the control of BMO.
*98. A fee of $5 per transaction will be charged unless the transaction is completed from an account with the Premium Plan, Bank at Work Programs, or the Performance Plan that is part of the NewStart Discounted Banking Program.Transfer limits apply.Fees charged on U.S.dollar accounts are charged in U.S.dollars.There is no charge when sending funds from your BMO account to a BMO Harris Bank account in the United States.Bank Plans that do not include unlimited transactions may be subject to excess transaction fees in addition to the transfer fee when you exceed the number of transactions in your Bank Plan.
*99. There is no fee charged for transfers from your Saving Amplifier Account to your other BMO account through BMO Mobile and Online Banking, at a BMO ATM, or when using the BMO Telephone Banking interactive voice response(or self-serve). A fee applies for each pre-authorized debit, cash withdrawal, and transfers from your Savings Amplifier Account to your other BMO account when performed at a branch or with a Customer Contact Centre associate.Refer to Agreements, Bank Plans and Fees for Everyday Banking.
*100. To open a new Premium Rate Savings account online, you are required to have a Primary Chequing Account with a Bank Plan.New customers can open a Premium Rate Savings Account at a local BMO branch without the requirement to have a Primary Chequing Account.
®§ Interac e-Transfer is a registered trademark
"; //Terms and Conditions copied from BMO
            
                if (tick < termsAndConditions.Length)
                {
                    Console.Write(termsAndConditions[tick]);
                    tick = tick + 1;
                }
            

        }

        private static void DepositTimer(object source, ElapsedEventArgs e)
        {
            string finish = "Eleven seconds have now passed.";
            Console.SetCursorPosition(39 + bop, 4 - bop);
            Console.Write("  ");

            Debug.WriteLine(tick);
                if (tick <= 10000)
                {
                tick = tick + 1000;
                Console.SetCursorPosition(39 + bop, 4 - bop);
                Console.Write(tick / 1000);
                }
                else
                {
                    invtimer.Enabled = false;
                    for (int i = 0; i < finish.Length; i++)
                {
                    Console.Write(finish[i]);
                    Thread.Sleep(15);
                }
                    IntPtr hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

                    PostMessage(hWnd, WM_KEYDOWN, VK_RETURN, 0); //The previous two lines simulate an enter key press.  This stops the ReadKey automatically so that the user can return to the menu.
                Thread.Sleep(1000);
                }

        }
    }
}
