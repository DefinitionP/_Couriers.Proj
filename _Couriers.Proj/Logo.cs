using System;
using System.Collections.Generic;
using System.Threading;


namespace _Couriers.Proj
{
    internal class Logo
    {
        static int Duration = 5;    // Длительность глитча в секундах
        public static int SizeX = Console.LargestWindowWidth;  // Ширина
        public static int SizeY = Console.LargestWindowHeight; // Высота
        static int LogoX = 154, LogoY = 11;    // "Габариты" лого, в символах

        static List<string> LogoStrings = new List<string>();

        /// <summary>
        /// Вывод заставки компании
        /// </summary>
        public static void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            LogoStrings.Add("           ,g@@@@@@@@g         gg@@@@@@@@g       $@@L         ]@@F    $@@@@@@@@@@@@g       ]@@F    ,@@@@@@@@@@@@@@@    @@@@@@@@@@@@g,        ,g@@@@@@@@@g,");
            LogoStrings.Add("         g@@@*\"   \"]@@@     ,@@@M'    -j@@@     ]@@L         j@@@    j@@M       `%@@@     j@@@     $@@L               $@@-       \"$@@r    ,@@@M\"-    \"*%N\"");
            LogoStrings.Add("       ,@@@\"        $NNP   g@@F         $@@    ;@@@         ,@@@    ,@@@         ]@@@    ,@@@     $@@L               $@@F         $@@`   ,@@@             ");
            LogoStrings.Add("      ,@@@                ]@@F         ]@@K    @@@          $@@\"    @@@`        ,@@@     @@@\"    y@@P               j@@M         g@@F    @@@L             ");
            LogoStrings.Add("      $@@`               ;@@@         ,@@@    $@@L         $@@L    $@@C     ,,g@@@\"     ]@@L    ,@@@gggggggggg     ,@@@      ,,g@@R\"     ]@@@@@gg,,       ");
            LogoStrings.Add("     $@@C               ,@@@          @@@`   $@@F         j@@F    ]@@@@@@@@@@@M*`      j@@@     $@@MMMMMMMMMM'     @@@@@@@@@@@@M\"          -\"\"*MN@@@@g    ");
            LogoStrings.Add("    ]@@M                $@@\"         ]@@L   j@@M         ,@@@    ;@@@      $@@K       ,@@@     $@@C               $@@L     '@@@                   '$@@    ");
            LogoStrings.Add("    @@@         gggM   ]@@@         g@@L    $@@         ,@@@     @@@       ]@@@       $@@'    g@@F               j@@K       $@@L                  y@@P    ");
            LogoStrings.Add("    %@@g     ,g@@N\"    ]@@@,     ,g@@R'     $@@g     ,,@@@F     $@@'        @@@L     $@@L    j@@@               ,@@@        ]@@@    g@@g,      ,g@@@\"     ");
            LogoStrings.Add("     *@@@@@@@@N\"`       \"N@@@@@@@@N\"        \"%@@@@@@@@@R*      ]@@F         ]@@@    g@@P     @@@@@@@@@@@@@@@    @@@`        '@@@L   \"%N@@@@@@@@@NP\"       ");
            LogoStrings.Add("         --                 ---                  --                                                                                       ---             ");



            for (int i = 0; i < ((SizeY - LogoY) / 2); i++)     // Промежуток между лого и верхним краем консоли
            {

                Console.WriteLine();
            }
            Thread.Sleep(40);
            for (int i = 0; i < LogoStrings.Count; i++)     // Построчный вывод логотипа
            {
                for (int q = 0; q < ((SizeX - LogoX) / 2); q++)
                {

                    Console.Write(' ');
                }
                Thread.Sleep(20);

                Console.WriteLine(LogoStrings[i]);
            }




            Glitch();
            Console.Clear();
            Console.ResetColor();
        }


        private static void Glitch()
        {
            DateTime StartGlitch = DateTime.Now;
            Random rnd = new Random();

            List<string> GlitchStrings = new List<string>();
            GlitchStrings.Add("#####################################################################################################################################################");
            GlitchStrings.Add(".....................................................................................................................................................");
            GlitchStrings.Add("/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");



            for (; ; )
            {
                int tick1 = rnd.Next(200, 500);
                int tick2 = rnd.Next(50, 100);
                int str = rnd.Next(0, 2);
                int GlitchY = rnd.Next((SizeY - LogoY) / 2, (SizeY - LogoY) / 2 + LogoY);
                int shift = rnd.Next(-3, 3);
                int GlitchType = rnd.Next(1, 4);

                if (GlitchType == 1)
                {

                    CursorToEnd(); Thread.Sleep(tick1);

                    Console.SetCursorPosition((SizeX - LogoX) / 2, GlitchY);
                    Console.WriteLine(GlitchStrings[str]);

                    CursorToEnd(); Thread.Sleep(tick2);

                    Console.SetCursorPosition((SizeX - LogoX) / 2, GlitchY);
                    Console.WriteLine(LogoStrings[GlitchY - (SizeY - LogoY) / 2]);
                }
                else
                {
                    int tick3 = rnd.Next(50, 200);
                    int tick4 = rnd.Next(100, 200);

                    CursorToEnd(); Thread.Sleep(tick3);

                    Console.SetCursorPosition((SizeX - LogoX) / 2 + shift, GlitchY);
                    Console.WriteLine(LogoStrings[GlitchY - (SizeY - LogoY) / 2]);

                    CursorToEnd(); Thread.Sleep(tick4);

                    Console.SetCursorPosition((SizeX - LogoX) / 2, GlitchY);
                    Console.WriteLine(LogoStrings[GlitchY - (SizeY - LogoY) / 2]);
                }
                if ((CheckGlitchTime(StartGlitch) == 1) | (Program.LogoThreadCondition == false)) // Условие завершения демонстрации заставки
                {
                    Program.LogoThreadCondition = false;
                    break;
                }
            }
        }
        public static void Escape()
        {
            ConsoleKeyInfo Escape;
            var EscapeKey = ConsoleKey.Spacebar;
            while (Program.LogoThreadCondition == true)
            {
                Escape = Console.ReadKey(true);
                if ((Escape.Key == EscapeKey) | (Program.LogoThreadCondition == false))
                {
                    Program.LogoThreadCondition = false;
                    break;
                }
                else
                {
                    Console.CursorTop = SizeY; Console.CursorLeft = (int)(SizeX / 2 - 10);
                    Console.WriteLine("ПРОБЕЛ - пропустить");
                }
            }
            return;
        }
        private static int CheckGlitchTime(DateTime StartGlitch)
        {
            DateTime now = DateTime.Now;
            TimeSpan del = now - StartGlitch;
            TimeSpan GlitchDuration = new TimeSpan(0, 0, Duration);
            if (del > GlitchDuration) return 1;
            else return 0;
        }
        private static void CursorToEnd()
        {
            Console.SetCursorPosition((SizeX - LogoX) / 2 + LogoX, (SizeY - LogoY) / 2 + LogoY);
        }
    }
}
