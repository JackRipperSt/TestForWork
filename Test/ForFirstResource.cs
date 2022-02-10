using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ForFirstResource : Storage
{
    [SerializeField] private SecondStructure _secondStructure;

    private List<FirstResource> _firstResourcesList = new List<FirstResource>();

    private void OnEnable()
    {
        _currentCaicity = 0;
        _secondStructure.ResourceCreate += ResourceRecycling;
    }

    private void OnDisable()
    {
        _secondStructure.ResourceCreate -= ResourceRecycling;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (player.GetComponentInChildren<FirstResource>())
            {
                var resource = player.GetComponentInChildren<FirstResource>();
                if (_currentCaicity < _maxCapicity)
                {
                    player.TransitResource(resource, transform);
                    _currentCaicity ++;
                    TransitMaterialToCreateResources(resource);
                    _firstResourcesList.Add(resource);
                    player.CurrentBagCapicity--;
                    if (_currentCaicity <= 0)
                        _currentCaicity = 0;
                }
            } 
        }
    }

    public void TransitMaterialToCreateResources(FirstResource firstResource)
    {
        _secondStructure.FirstResources.Add(firstResource);
    }

    protected override void ResourceRecycling()
    {
        Destroy(_firstResourcesList.Last().gameObject);
        _firstResourcesList.Remove(_firstResourcesList.Last());
        _currentCaicity--;
    }
}
