using CodingTest.ApplicationLoop;

namespace CodingTest 
{
    class Program
    {
        public static void Main(string[] args)
        {
            Application application = new TestApplication(800, 600, "Test");
            application.Run();
        }
    }
}
