using UnityEngine;

namespace THUtils
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static Singleton<T> _instance;

        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                if (_instance == null && typeof(IEditorSingleton).IsAssignableFrom(typeof(T)))
                {
                    var findObjectsOfType = FindObjectsOfType<T>();
                    if (findObjectsOfType.Length > 0)
                    {
                        _instance = findObjectsOfType[0];
                    }
                }
#endif
                return _instance as T;
            }
        }

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }

            // if this Singleton implements IEditorSingleton we might already have an instance!
            if (_instance == this && this is IEditorSingleton)
            {
                return;
            }
#endif
            if (_instance != null)
            {
                Debug.Log(string.Format("Multiple instances of script {0}!", GetType()), gameObject);
                Debug.Log("Other instance: " + _instance, _instance);
            }
            else
            {
                _instance = this;
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }

    public interface IEditorSingleton
    {
    }
}