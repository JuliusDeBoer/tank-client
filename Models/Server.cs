﻿using Microsoft.AspNetCore.SignalR.Client;

namespace tank_client.Models
{
    public class Server
    {
        private HubConnection Connection { get; set; }
        private Mutex Mutex { get; set; }

        public Server()
        {
            Mutex = new();
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

        public T Invoke<T>(string methodName)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName);
            return task.Result;
        }

        public object Invoke<T>(string methodName, object arg1)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1);
            return task.Result;
        }

        public object Invoke<T>(string methodName, object arg1, object arg2)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2);
            return task.Result;
        }

        public object Invoke<T>(string methodName, object arg1, object arg2, object arg3)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3);
            return task.Result;
        }
        public object Invoke<T>(string methodName, object arg1, object arg2, object arg3, object arg4)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4);
            return task.Result;
        }

        public object Invoke<T>(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4);
            return task.Result;
        }
    }
}