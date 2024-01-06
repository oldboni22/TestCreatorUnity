using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] List<MonoBeh> Objects;

    private void Awake()
    {
        foreach (var obj in Objects) 
        {
            obj.OnAwake();        
        }
    }

    private void Update()
    {
        foreach (var obj in Objects)
        {
            obj.OnUpdate();
        }
    }
}
