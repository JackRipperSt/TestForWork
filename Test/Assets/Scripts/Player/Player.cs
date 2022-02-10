using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _bag;
    [SerializeField] private int _maxBagCapicity;

    public event UnityAction AddFirstResources;
    public event UnityAction AddSecondResouces;
    public event UnityAction AddThirdResouces;
    public int CurrentBagCapicity { get; set; }

    private List<Resources> _resources ;

    private void Start()
    {
        _resources = new List<Resources>(_maxBagCapicity);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out TriggerZoneForAddResouce triggerZoneForAddResouce))
        {
            if (CurrentBagCapicity < _maxBagCapicity)
            {
                if (triggerZoneForAddResouce.GetComponentInChildren<FirstResource>())
                {
                    Resources res = triggerZoneForAddResouce.GetComponentInChildren<Resources>();
                    AddResource(res);
                    AddFirstResources?.Invoke();
                }
                else if (triggerZoneForAddResouce.GetComponentInChildren<SecondResource>())
                {
                    Resources res = triggerZoneForAddResouce.GetComponentInChildren<Resources>();
                    AddResource(res);
                    AddSecondResouces?.Invoke();
                }
                else if (triggerZoneForAddResouce.GetComponentInChildren<ThirdResource>())
                {
                    Resources res = triggerZoneForAddResouce.GetComponentInChildren<Resources>();
                    AddResource(res);
                    AddThirdResouces?.Invoke();
                }
            }
        }
    }

    private void AddResource(Resources resource)
    {
        _resources.Add(resource);
        Debug.Log(_resources.Count);
        CurrentBagCapicity++;
        TransitResource(resource, _bag);
    }

    public void TransitResource(Resources resource, Transform parent)
    {
           Vector3 spawnPosition = new Vector3(parent.position.x, parent.position.y + 0.1f, parent.position.z);
        resource.transform.position = spawnPosition;
        resource.transform.rotation = parent.rotation;
        resource.transform.parent = parent.transform;
        //spawnPosition = new Vector3(parent.position.x, _resources.Last().transform.position.y + 0.1f, parent.position.z);

        if (CurrentBagCapicity < 0)
            CurrentBagCapicity = 0;
    }
}
