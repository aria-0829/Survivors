using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class DontDestroy : MonoBehaviour
{
    private static int instances = 0;

    private void Awake()
    {
        instances++;
        if (instances == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
