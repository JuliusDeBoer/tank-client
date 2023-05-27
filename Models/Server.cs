﻿using Microsoft.AspNetCore.SignalR.Client;

namespace tank_client.Models
{
    public static class Server
    {
        private static HubConnection Connection { get; set; }

        public static void Connect()
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

        // Im gonna cry
        public static T Invoke<T>(string methodName)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName);
            return task.Result;
        }
        public static void Invoke(string methodName)
        {
            Connection.InvokeAsync(methodName);
        }

        public static T Invoke<T>(string methodName, object arg1)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1);
            return task.Result;
        }

        public static void Invoke(string methodName, object arg1)
        {
            Connection.InvokeAsync(methodName, arg1);
        }

        public static T Invoke<T>(string methodName, object arg1, object arg2)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2);
            return task.Result;
        }
        public static void Invoke(string methodName, object arg1, object arg2)
        {
            Connection.InvokeAsync(methodName, arg1, arg2);
        }

        public static T Invoke<T>(string methodName, object arg1, object arg2, object arg3)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3);
            return task.Result;
        }
        public static void Invoke(string methodName, object arg1, object arg2, object arg3)
        {
            Connection.InvokeAsync(methodName, arg1, arg2, arg3);
        }

        public static T Invoke<T>(string methodName, object arg1, object arg2, object arg3, object arg4)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4);
            return task.Result;
        }
        public static void Invoke(string methodName, object arg1, object arg2, object arg3, object arg4)
        {
            Connection.InvokeAsync(methodName, arg1, arg2, arg3, arg4);
        }

        public static T Invoke<T>(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4, arg5);
            return task.Result;
        }

        public static void Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            Connection.InvokeAsync(methodName, arg1, arg2, arg3, arg4, arg5);
        }
    }
}