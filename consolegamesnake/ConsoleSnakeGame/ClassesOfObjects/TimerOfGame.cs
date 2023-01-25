using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSnakeGame.ClassesOfObjects
{
    class TimerOfGame
    {
        public static int sec = 0;
        public static async  void Timer()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    sec++;
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
