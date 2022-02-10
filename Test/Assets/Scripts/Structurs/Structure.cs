using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class Structure : MonoBehaviour
{
    [SerializeField] protected Player Player;
    [SerializeField] protected Resources Resource;
    [SerializeField] protected Transform Parent;
    [SerializeField] protected float TimeToCreate;
    [SerializeField] protected int MaxCapicity;
    [SerializeField] protected string TNotEnoughResourcesInStorage;
    [SerializeField] protected string TStorageLimitCreateResource;

    protected List<Resources> _createdResources = new List<Resources>();

    protected float PastTime;
    protected int CurrentCapicity;

    public abstract event UnityAction<string,Transform> NotEnoughResourcesInStorage;
    public abstract event UnityAction<string,Transform> StorageLimitCreateResource;

    protected abstract void CreateResource(Resources resource, Transform parent);

    protected abstract void ResourceTaken();

    protected abstract IEnumerator IAlertAtMaxCapicity();

}
