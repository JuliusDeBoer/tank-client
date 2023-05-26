using Microsoft.AspNetCore.SignalR.Client;

namespace tank_client.Models
{
    public enum ServerStatus
    {
        READY,
        CONNECTED,
        DISCONNECTED
    }

    public class Server
    {
        private HubConnection Connection { get; set; }
        public ServerStatus Status { get; private set; }

        public Server()
        {
            Status = ServerStatus.READY;
        }

        public void Connect()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:6666/api/v1/hub")
                .WithAutomaticReconnect()
                .Build();

            try
            {
                Connection.StartAsync().Wait();
            }
            catch (Exception ex)
            {
                // AAAAAAAHHHHH
            }
        }


        public Object Invoke<T>(string methodName)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName);
            return task.Result;
        }

        public Object Invoke<T>(string methodName, Object arg1)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1);
            return task.Result;
        }

        public Object Invoke<T>(string methodName, Object arg1, Object arg2)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2);
            return task.Result;
        }

        public Object Invoke<T>(string methodName, Object arg1, Object arg2, Object arg3)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3);
            return task.Result;
        }
        public Object Invoke<T>(string methodName, Object arg1, Object arg2, Object arg3, Object arg4)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4);
            return task.Result;
        }

        public Object Invoke<T>(string methodName, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4);
            return task.Result;
        }
    }
}