using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingletone<T> : MonoBeh where T: MonoSingletone<T>
{
    static T _instance;
    public static T Instance => _instance;

    public override void OnAwake()
    {
        _instance = (T)this;
    }
}
