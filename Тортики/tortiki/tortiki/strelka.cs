//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Strela
//{
//    internal class Strelka
//    {
//        public int minStrelka;
//        public int maxStrelka;
//        public int Where()
//        {
//            int pos = 2;
//            ConsoleKeyInfo key;
//            do
//            {
//                Console.SetCursorPosition(0, pos);
//                Console.WriteLine("~>");

//                key = Console.ReadKey();

//                Console.SetCursorPosition(0, pos);
//                Console.WriteLine("  ");

//                if (key.Key == ConsoleKey.UpArrow && pos!= minStrelka)
//                {
//                    pos--;
//                }
//                else if(key.Key == ConsoleKey.DownArrow && pos != maxStrelka)
//                {
//                    pos++;
//                }

                
//            } while (key.Key != ConsoleKey.Enter);
//            return pos;
//        }
//    }
//}
