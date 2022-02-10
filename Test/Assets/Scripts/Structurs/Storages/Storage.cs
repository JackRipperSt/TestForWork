using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storage : MonoBehaviour
{
    [SerializeField] protected  int _maxCapicity;
    protected int _currentCapicity;

    protected abstract void ResourceRecycling();
}
