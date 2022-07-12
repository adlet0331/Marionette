using System;
using UnityEngine;

namespace SerializableManager
{
    [Serializable]
    public class Wrapper<T>
    {
        public T[] resources;
    }
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.resources;
        }
    }
}