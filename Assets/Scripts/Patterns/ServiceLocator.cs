using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void RegisterService<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services[type] = service;  
        }
        else
        {
            _services.Add(type, service);  
        }
    }

    public static void UnregisterService<T>()
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
    }

    public static T GetService<T>()
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            return (T)_services[type];
        }
        else
        {
            throw new Exception($"Service of type {type} not found");
        }
    }
}