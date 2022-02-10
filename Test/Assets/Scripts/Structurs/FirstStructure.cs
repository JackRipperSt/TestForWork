using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class FirstStructure : Structure
{

    [SerializeField] private Transform _spawnPointToText;

    public event UnityAction ResourceCreate;
    public override event UnityAction<string,Transform> NotEnoughResourcesInStorage;
    public override event UnityAction<string, Transform> StorageLimitCreateResource;


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

            if (PastTime > TimeToCreate && CurrentCapicity < MaxCapicity)
            {
                if (CurrentCapicity < MaxCapicity)
                {
                    CreateResource(Resource, Parent);
                    CurrentCapicity++;
                    _spawnPosition = new Vector3(_createdResources.Last().transform.position.x, _createdResources.Last().transform.position.y + 0.1f, _createdResources.Last().transform.position.z);
                }

                PastTime = 0;
            
            if (CurrentCapicity >= MaxCapicity)
                CurrentCapicity = MaxCapicity;
            }

            if(CurrentCapicity == MaxCapicity && PastTime >= 6)
            {
                StartCoroutine(IAlertAtMaxCapicity());
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

        else if (_createdResources.Count < MaxCapicity)
        {
            Resources resourceToCreate = Instantiate(resource, parent.transform.position, Quaternion.identity, parent);
            _createdResources.Add(resourceToCreate);
            ResourceCreate?.Invoke();
        }
    }


    protected override IEnumerator IAlertAtMaxCapicity()
    {
        yield return new WaitForSeconds(1);
        StorageLimitCreateResource?.Invoke(TStorageLimitCreateResource, _spawnPointToText);
        yield break;
    }


    protected override void ResourceTaken()
    {
        _createdResources.Remove(_createdResources.First());
        CurrentCapicity -= 2;
        if (CurrentCapicity <= 0)
        {
            CurrentCapicity = 0;
        }
    }
}
