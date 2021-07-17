using UnityEngine;

public class SingletonInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindObjectOfType(typeof(T));
        }
        else
        {
            Destroy(this.gameObject);
        }

        gameObject.name = gameObject.name;
        OnInitialize();
    }

    public static T GetInstance()
    {
        return instance;
    }

    protected virtual void OnInitialize() { }

    protected virtual void OnDestroy() { }
}
