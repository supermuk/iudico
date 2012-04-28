namespace IUDICO.UnitTests.TestingSystem.Selenium
{
    using System;
    using System.Threading;

    using global::Selenium;

    public static class ExSelenium
    {
        public static ISelenium Selenium { get; set; }

        public static string Timeout { get; set; }

        public static bool IsSeleniumStarted { get; private set; }

        static ExSelenium()
        {
            IsSeleniumStarted = false;
        }

        public static void StartSelenium()
        {
            Selenium.Start();
            IsSeleniumStarted = true;
        }

        public static void StopSelenium()
        {
            Selenium.Stop();
            IsSeleniumStarted = false;
        }

        public static void WaitForElement(string locator)
        {
            int sleepTime = 0;
            while (!Selenium.IsElementPresent(locator))
            {
                if (sleepTime > Convert.ToInt32(Timeout))
                {
                    return;
                }
                Thread.Sleep(100);
                sleepTime += 100;
            }
        }
    }
}
