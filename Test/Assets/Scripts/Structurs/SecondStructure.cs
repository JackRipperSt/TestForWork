using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class SecondStructure : Structure
{
    [SerializeField] private Transform _createPlaceBox;
    [SerializeField] private Transform _storagePlaceBox;

    public event UnityAction ResourceCreate;
    public override event UnityAction<string, Transform> NotEnoughResourcesInStorage;
    public override event UnityAction<string, Transform> StorageLimitCreateResource;

    public List<FirstResource> FirstResources => _firstResources;

    [SerializeField] private List<FirstResource> _firstResources = new List<FirstResource>();
    private Vector3 _spawnPosition;

    private void OnEnable()
    {
        Player.AddSecondResouces += ResourceTaken;
    }

    private void OnDisable()
    {
        Player.AddSecondResouces -= ResourceTaken;
    }
    private void Update()
    {
        PastTime += Time.deltaTime;

        if (_firstResources.Count > 0)
         {
             if (PastTime > TimeToCreate)
             {
                if (CurrentCapicity < MaxCapicity)
                {
                    CreateResource(Resource, Parent);
                    CurrentCapicity++;
                    _spawnPosition = new Vector3(_createdResources.Last().transform.position.x, _createdResources.Last().transform.position.y + 0.1f, _createdResources.Last().transform.position.z);
                    _firstResources.Remove(_firstResources.Last());
                PastTime = 0;
                }
            }
            if (CurrentCapicity >= MaxCapicity)
                CurrentCapicity = MaxCapicity;
        }
            
        if (CurrentCapicity == MaxCapicity && PastTime > 6)
        {
            StartCoroutine(IAlertAtMaxCapicity());
            PastTime = 0;
        }
        if (_firstResources.Count == 0 && PastTime > 6)
        {
            StartCoroutine(INotEnoughResourcesInStorage());
            PastTime = 0;
        }
        
    }


    protected override void CreateResource(Resources resource, Transform parent)
    {

        
        if (_createdResources.Count == 0)
        {
            Resources resourcesToCreate = Instantiate(resource, parent);

            _createdResources.Add(resourcesToCreate);
            ResourceCreate?.Invoke();
        }

        else if (_createdResources.Count != 0)
        {
            Resources resourceToCreate = Instantiate(resource, _spawnPosition, Quaternion.identity, parent);
            _createdResources.Add(resourceToCreate);
            ResourceCreate?.Invoke();
        }

    }


    protected override IEnumerator IAlertAtMaxCapicity()
    {
        yield return new WaitForSeconds(1);
        StorageLimitCreateResource?.Invoke(TStorageLimitCreateResource, _createPlaceBox);
        yield break;
    }

    private IEnumerator INotEnoughResourcesInStorage()
    {
        yield return new WaitForSeconds(1);
        NotEnoughResourcesInStorage?.Invoke(TNotEnoughResourcesInStorage, _storagePlaceBox);
        yield break;
    }

    protected override void ResourceTaken()
    {
        _createdResources.Remove(_createdResources.Last());
        CurrentCapicity -= 1;
        if (CurrentCapicity <= 0)
        {
            CurrentCapicity = 0;
        }
    }
}
