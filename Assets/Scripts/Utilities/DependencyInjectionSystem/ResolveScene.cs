using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolveScene : MonoBehaviour
{
    private void Awake()
    {
        var dependencyResolver = new DependencyResolver();
        dependencyResolver.ResolveScene();
    }
}
