using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class FirstStructure : Structure
{
    
    public override event UnityAction StorageLimitResource;
    public override event UnityAction<TMP_Text> StorageLimitCreateResource;

    private Vector3 _spawnPosition;

    private void OnEnable()
    {
        Player.AddFirstResources += ResourceTaken;
    }

    private void OnDisable()
    {
        Player.AddFirstResources -= ResourceTaken;
    }

    private void Update()
    {
        PastTime += Time.deltaTime;
        if (PastTime > TimeToCreate)
        {
            CurrentCapicity++;
            if (CurrentCapicity <= MaxCapicity)
            {
                CreateResource(Resource, Parent);
                _spawnPosition = new Vector3(_createdResources.Last().transform.position.x, _createdResources.Last().transform.position.y + 0.1f, _createdResources.Last().transform.position.z);

                if (_createdResources.Count >= MaxCapicity)
                {
                    StorageLimitCreateResource?.Invoke();
                }
            }
            PastTime = 0;
        }
    }

    protected override void CreateResource(Resources resource, Transform parent)
    {

         if( _createdResources.Count == 0)
        {
            Resources resourcesToCreate = Instantiate(resource, parent);
            _createdResources.Add(resourcesToCreate);
        }

        else if (_createdResources.Count != 0)
        {
            Resources resourceToCreate = Instantiate(resource, _spawnPosition, Quaternion.identity, parent);
            _createdResources.Add(resourceToCreate);
        }
        else if(_createdResources.Count >= MaxCapicity)
        {
            StorageLimitResource?.Invoke();
        }
    }

    

    protected override void ResourceTaken()
    {
        _createdResources.Remove(_createdResources.First());
        CurrentCapicity -= 2;
        if(CurrentCapicity <= 0)
        {
            CurrentCapicity = 0;
        }
    }
}
