﻿using cAlgo.API.Alert.UI;
using System;
using System.Globalization;
using System.Threading;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net;
using System.IO;
using System.Text;

namespace cAlgo.API.Alert.Tester
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Bootstrapper bootstrapper = null;

            Thread windowThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    bootstrapper = new Bootstrapper(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\alerts.csv", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\options.xml");

                    bootstrapper.Run();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex);
                }
            }));

            windowThread.SetApartmentState(ApartmentState.STA);
            windowThread.CurrentCulture = CultureInfo.InvariantCulture;
            windowThread.CurrentUICulture = CultureInfo.InvariantCulture;

            windowThread.Start();

            Thread.Sleep(10000);

            bootstrapper.AddAlert(new UI.Models.AlertModel { TradeSide = "Buy", Comment = "Alert new comment", TriggeredBy = "afhacker algo", Time = DateTimeOffset.Now, Symbol = "EURUSD", TimeFrame = "1 Hour" });
        }
    }
}