using UnityEngine;

namespace Amsterdam.Managers.Extentions
{
    public abstract class BaseSettings<T> : ScriptableObject where T : ScriptableObject
    {
        public static T Current => Resources.Load<T>(typeof(T).Name);
    }
}