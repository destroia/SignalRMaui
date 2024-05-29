using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

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
               // x.Transports = HttpTransportType.None;
                x.Transports = HttpTransportType.WebSockets | HttpTransportType.ServerSentEvents | HttpTransportType.LongPolling;
                x.UseDefaultCredentials = true;
                x.Headers.Add("Authorization", "Bearer " + token);
            }).Build();

           // _connection = new HubConnectionBuilder().WithUrl("http://192.168.1.13:5296/chat").Build();
            //MessageReceived
            _connection.On<object>("DockAndYardClient", (message) =>
            {
              
            });
           
                Task.Run(async () =>
                {
                    try
                    {
                         _connection.StartAsync().Wait();

                        if(_connection.ConnectionId != string.Empty)
                        {
                            await Task.Delay(4000);

                        }
                           
                        int sd = 3;
                    }
                    catch (Exception ex)
                        {

                    }
                   
                });

            
        }
        string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsiLCJ0eXAiOiJKV1QifQ.eyJzdWIiOiJmOWIwMGE0ZS1hNTMwLTRjZDgtODFlZi1lMTIwMzhmY2FiNDEiLCJuYW1lIjoiSnVhbiBDYW1pbG8gSG9sZ3VpbiBQZXJleiIsImVtYWlscyI6WyJjYW1pbG8wMDFAZW1haWwuY29tIl0sInRmcCI6IkIyQ18xX2Rldi1HTFBTLWZsb3ciLCJzY3AiOiJhY2Nlc3NfYXNfdXNlciIsImF6cCI6ImEzMzE1OGI2LTNjOWYtNGNkZS05NjYxLTY5OWM5YjY2NDEyZSIsInZlciI6IjEuMCIsImlhdCI6MTcxNzAyMTA4MCwiYXVkIjoiNzc3ODA5NTktODU4YS00NDJkLWJlYWQtOTVhODgxNWJiOGE4IiwiZXhwIjoxNzE3MDI0NjgwLCJpc3MiOiJodHRwczovL29pZGV2YWRiMmMuYjJjbG9naW4uY29tLzE3ZDMzYjYxLTAyNGYtNDc2Yy05NmFiLWYxZWIwMjVjNGQxNy92Mi4wLyIsIm5iZiI6MTcxNzAyMTA4MH0.C26qQ57FVJUOZgEs-tNS3U0vYW6e0cnjnVfXJnfOFDB7cvqjJpaIKVQRq_4l2qBSUU_NFs2Sk91N1RTp-lF0Yddy1u7NAH3vubA29s0Onmi1-OLj6PSdK6E-7-WoKIWEeab1D9-a2R5Aj5vJroXOYQH5FVvYRVz1B4Caa0Vf5FEQLbdBjrkVqfdX3z2WDhlLCxKSzwx_xt2vtIsxh_gApGZLY9FAn8PNDuj9Pv3eJ-f_O36QiHsguT1CLu_I_R-gq-socxI_6TW03CzohWtsmnfAJ2PyWIExUq3LlJZEFOIKce62f2lBLUOT0Lm8sFgzfep6X5xzoZy_xt_h4qxKOw";
        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await _connection.InvokeCoreAsync("SendMessage", args: new[] {"asdas" });


        }
    }

}
