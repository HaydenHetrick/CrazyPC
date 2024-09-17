using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Media;

namespace CrazyPC
{
    internal class CrazyPC
    {
        public static Random _random = new Random();
        private static CancellationTokenSource _cts;

        static void CrazyMouseThread(CancellationToken token)
        {
            Console.WriteLine("CrazyMouseThread");

            while (!token.IsCancellationRequested)
            {
                int moveX = _random.Next(-10, 10);
                int moveY = _random.Next(-10, 10);

                Cursor.Position = new Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);
                Thread.Sleep(50);
            }
        }

        static void CrazyKeyboardThread(CancellationToken token)
        {
            Console.WriteLine("CrazyKeyboardThread");

            while (!token.IsCancellationRequested)
            {
                if (_random.Next(0, 100) < 50)
                {
                    SendKeys.SendWait("a");
                }
                else
                {
                    SendKeys.SendWait("b");
                }

                Thread.Sleep(50);
            }
        }

        static void CrazySoundThread(CancellationToken token)
        {
            Console.WriteLine("CrazySoundThread");

            while (!token.IsCancellationRequested)
            {
                if (_random.Next(0, 100) < 50)
                {
                    SystemSounds.Beep.Play();
                }
                else
                {
                    SystemSounds.Exclamation.Play();
                }

                Thread.Sleep(50);
            }
        }

        public static void CrazyFunctionCall()
        {
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            Thread crazyMouseThread = new Thread(() => CrazyMouseThread(token));
            Thread crazyKeyboardThread = new Thread(() => CrazyKeyboardThread(token));
            Thread crazySoundThread = new Thread(() => CrazySoundThread(token));

            crazyMouseThread.Start();
            crazyKeyboardThread.Start();
            crazySoundThread.Start();
            DateTime future = DateTime.Now.AddSeconds(10);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            _cts.Cancel();

            // Wait for threads to finish gracefully
            crazyMouseThread.Join();
            crazyKeyboardThread.Join();
            crazySoundThread.Join();
        }
    }
}
