using Microsoft.AspNetCore.SignalR.Client;

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

        //
        // 1 john 1:9
        //

        public static void On(string methodName, Action handler)
        {
            Connection.On(methodName, handler);
        }

        public static void On<T1>(string methodName, Action<T1> handler)
        {
            Connection.On(methodName, handler);
        }

        public static void On<T1, T2>(string methodName, Action<T1, T2> handler)
        {
            Connection.On(methodName, handler);
        }

        public static void On<T1, T2, T3>(string methodName, Action<T1, T2, T3> handler)
        {
            Connection.On(methodName, handler);
        }

        public static void On<T1, T2, T3, T4>(string methodName, Action<T1, T2, T3, T4> handler)
        {
            Connection.On(methodName, handler);
        }

        public static void On<T1, T2, T3, T4, T5>(string methodName, Action<T1, T2, T3, T4, T5> handler)
        {
            Connection.On(methodName, handler);
        }

        public static void On<T1, T2, T3, T4, T5, T6>(string methodName, Action<T1, T2, T3, T4, T5, T6> handler)
        {
            Connection.On(methodName, handler);
        }

        public static void On<T1, T2, T3, T4, T5, T6, T7>(string methodName, Action<T1, T2, T3, T4, T5, T6, T7> handler)
        {
            Connection.On(methodName, handler);
        }
        public static void On<T1, T2, T3, T4, T5, T6, T7, T8>(string methodName, Action<T1, T2, T3, T4, T5, T6, T7, T8> handler)
        {
            Connection.On(methodName, handler);
        }

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

        public static T Invoke<T>(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4, arg5, arg6);
            return task.Result;
        }

        public static void Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
        {
            Connection.InvokeAsync(methodName, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static void Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7)
        {
            Connection.InvokeAsync(methodName, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static T Invoke<T>(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return task.Result;
        }

        public static void Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8)
        {
            Connection.InvokeAsync(methodName, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static T Invoke<T>(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return task.Result;
        }

        public static void Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9)
        {
            Connection.InvokeAsync(methodName, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 );
        }

        public static T Invoke<T>(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            return task.Result;
        }

        public static void Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10)
        {
            Connection.InvokeAsync(methodName, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        public static T Invoke<T>(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10)
        {
            Task<T> task = Connection.InvokeAsync<T>(methodName, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            return task.Result;
        }
    }
}