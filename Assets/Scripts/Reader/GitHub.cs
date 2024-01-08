using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHub : MonoBehaviour
{
    [SerializeField] string link;

    public void Click()
    {
        Application.OpenURL(link);
    }
}
