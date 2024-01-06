using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GlobalStorage")]
public class Storage : ScriptableObject
{
    static Storage instance;
    public static Storage Instance
    {
        get
        {
            if(instance == null)
                instance = Resources.Load<Storage>("Storage");
            return instance;
        }
    }

    public MenusStorage menuStorage;
    public PrefabStorage prefabs;
}
