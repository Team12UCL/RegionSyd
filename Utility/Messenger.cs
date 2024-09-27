using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Utility
{
    public static class Messenger
    {
        private static readonly Dictionary<string, Action<object>> _registeredActions = new Dictionary<string, Action<object>>();

        public static void Register(string messageKey, Action<object> action)
        {
            if (!_registeredActions.ContainsKey(messageKey))
            {
                _registeredActions.Add(messageKey, action);
            }
        }

        public static void Unregister(string messageKey)
        {
            if (_registeredActions.ContainsKey(messageKey))
            {
                _registeredActions.Remove(messageKey);
            }
        }

        public static void Send(string messageKey, object parameter = null)
        {
            if (_registeredActions.ContainsKey(messageKey))
            {
                _registeredActions[messageKey]?.Invoke(parameter);
            }
        }
    }

}