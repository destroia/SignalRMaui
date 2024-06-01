using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Timers;

namespace SignalR
{
    public partial class MainPage : ContentPage
    {
        private readonly HubConnection _connection;
        public MainPage()
        {
            InitializeComponent();


            _connection = new HubConnectionBuilder().WithUrl("https://dev-api-glps-o-i-com.azurewebsites.net/dockAndYardHub?group=4650", x => {

                x.AccessTokenProvider = () => Task.FromResult(token);

                x.Transports = HttpTransportType.WebSockets | HttpTransportType.ServerSentEvents | HttpTransportType.LongPolling;
                x.HttpMessageHandlerFactory = (handler) =>
                {
                    if (handler is HttpClientHandler httpClientHandler)
                        httpClientHandler.MaxRequestContentBufferSize = 1024 * 1024 * 1;
                    return handler;
                };
            }).ConfigureLogging(logging =>
            {
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Trace);
            }).AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            }).Build();


            //_connection = new HubConnectionBuilder().WithUrl("http://192.168.1.4:5296/chat", x => {

            //    //x.AccessTokenProvider = () => Task.FromResult(token);
            //    //x.Transports = HttpTransportType.WebSockets | HttpTransportType.ServerSentEvents | HttpTransportType.LongPolling;
            //    //x.HttpMessageHandlerFactory = (handler) =>
            //    //{
            //    //    if (handler is HttpClientHandler httpClientHandler)
            //    //    {
            //    //        httpClientHandler.MaxRequestContentBufferSize = 1024 * 1024 * 1; // 10 MB (por ejemplo)
            //    //    }
            //    //    return handler;
            //    //};
            //}).ConfigureLogging(logging =>
            //{
            //    logging.AddDebug();
            //    logging.SetMinimumLevel(LogLevel.Trace);
            //}).AddJsonProtocol(options =>
            //{
            //    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            //}).Build();

            //_connection.On<object,object>("MessageReceived", (dsd,o) =>
            //{
            //    Mjs(dsd);
            //});
            _connection.On<Dock[], Yard[]>("DockAndYardClient", (docks,yards) =>
            {
                try
                {
                   string d = JsonSerializer.Serialize(docks);
                    string y = JsonSerializer.Serialize(yards);
                   
                }
                catch (Exception EX)
                {

                }
               
                //Mjs(dsd);
            });

            Task.Run(async () =>
                {
                    try
                    {
                       await  _connection.StartAsync();
                        int sd = 3;
                    }
                    catch (Exception ex)
                    {

                    }
                   
                });

            
        }

        private void Mjs(object dsd)
        {
           
        }



        string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsiLCJ0eXAiOiJKV1QifQ.eyJzdWIiOiJmOWIwMGE0ZS1hNTMwLTRjZDgtODFlZi1lMTIwMzhmY2FiNDEiLCJuYW1lIjoiSnVhbiBDYW1pbG8gSG9sZ3VpbiBQZXJleiIsImVtYWlscyI6WyJjYW1pbG8wMDFAZW1haWwuY29tIl0sInRmcCI6IkIyQ18xX2Rldi1HTFBTLWZsb3ciLCJub25jZSI6ImRlZmF1bHROb25jZSIsInNjcCI6ImFjY2Vzc19hc191c2VyIiwiYXpwIjoiYTMzMTU4YjYtM2M5Zi00Y2RlLTk2NjEtNjk5YzliNjY0MTJlIiwidmVyIjoiMS4wIiwiaWF0IjoxNzE3MjY0ODc4LCJhdWQiOiI3Nzc4MDk1OS04NThhLTQ0MmQtYmVhZC05NWE4ODE1YmI4YTgiLCJleHAiOjE3MTcyNjg0NzgsImlzcyI6Imh0dHBzOi8vb2lkZXZhZGIyYy5iMmNsb2dpbi5jb20vMTdkMzNiNjEtMDI0Zi00NzZjLTk2YWItZjFlYjAyNWM0ZDE3L3YyLjAvIiwibmJmIjoxNzE3MjY0ODc4fQ.ZttpC00zb5lKjESg9toM7cuAQ_59xL8AXCg5bAcV3qGBeA01DOsoNI94hVZUjJgOStclcp6vQ6uL7_2_ZpPWE0Q43_CYM_H6etfUUa-MWSzKRm5PpwW1tIs-5FqSP5n81RRLM_qF9khevB1Hh6Q2TglcWsHWOjFKOCxdMWQJSokjA6nkWr3lrk8zHo7Ebh4z32Cv_YGbjoA-90M_G7vlvDJCkmOaCo7PHfrHxC5U0KayZsdY8DCg88EWT18vAk0fepbNMQq8cWKKJMyJIjRSPG9D8V7coQfonfH8eCeS5kvZar_piwoPt2RIGjr9kFN3hMSAU2Aua3RRKzgXvHJQzA";
       
        private async void OnCounterClicked(object sender, EventArgs e)
        {
            try
            {
                var c = _connection.State;
                await _connection.StopAsync();
                var co = _connection.State;
            }
            catch (Exception ex)
            {

            }
            
        }
    }
    public class Shipment
    {
        public string id { get; set; }
        public int shipmentId { get; set; }
    }

    public class Dock
    {
        public int id { get; set; }
        public string identity { get; set; }
        public string name { get; set; }
        public string management { get; set; }
        public int dockStatusId { get; set; }
        public object dockStatusName { get; set; }
        public int materialGroupId { get; set; }
        public object materialGroupName { get; set; }
        public int storageBuildingId { get; set; }
        public string storageBuildingName { get; set; }
        public bool busy { get; set; }
        public string equipment { get; set; }
        public Shipment shipment { get; set; }
        public DateTime? timestamp { get; set; }
    }

    public class Yard
    {
        public int id { get; set; }
        public string section { get; set; }
        public string identity { get; set; }
        public string name { get; set; }
        public string management { get; set; }
        public int warehouseId { get; set; }
        public int yardStatusId { get; set; }
        public object yardStatusName { get; set; }
        public int materialGroupId { get; set; }
        public object materialGroupName { get; set; }
        public int storageBuildingId { get; set; }
        public string storageBuildingName { get; set; }
        public bool busy { get; set; }
        public Shipment shipment { get; set; }
        public DateTime? timestamp { get; set; }
    }

    public class RootObject
    {
        public List<Dock> docks { get; set; }
        public List<Yard> yards { get; set; }
    }
}
