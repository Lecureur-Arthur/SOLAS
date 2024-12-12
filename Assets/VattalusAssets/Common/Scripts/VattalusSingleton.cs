using UnityEngine;

public abstract class VattalusSimpleSingleton<T> where T : class, new()
{
    protected static readonly T _instance = new T();

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }
}

public abstract class VattalusSingleton<T> : ScriptableObject where T : ScriptableObject
{
    protected static readonly T _instance = CreateInstance<T>();

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }
}

public class VattalusUnitySingleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    [HideInInspector]
    public static bool hasInstance = false;

    public static T Instance
    {
        get
        {
            /*
                        if (_instance == null)
                        {
                            _instance = FindObjectOfType<T>();
                            if (_instance == null)
                            {
                                GameObject obj = new GameObject();
                                //obj.hideFlags = HideFlags.HideAndDontSave;
                                _instance = obj.AddComponent<T>();
                                hasInstance = true;
                            }
                        }
            */
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            CustomAwake();
        }
        /*
                else
                {
                    DestroyImmediate(gameObject);
                }
        */
    }

    protected virtual void CustomAwake()
    {
    }

    void OnDestroy()
    {
        _instance = null;
        hasInstance = false;
    }
}

public class VattalusUnitySingletonPersistent<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    public static bool hasInstance = false;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject { hideFlags = HideFlags.HideAndDontSave };
                    _instance = obj.AddComponent<T>();
                    hasInstance = true;
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this as T;
            CustomAwake();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    protected virtual void CustomAwake()
    {

    }
}