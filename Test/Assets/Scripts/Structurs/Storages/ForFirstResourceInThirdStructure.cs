using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ForFirstResourceInThirdStructure : Storage
{
    [SerializeField] private ThirdStructure _thirdStructure;

   [SerializeField] private List<FirstResource> _firstResourcesList = new List<FirstResource>();


    private void OnEnable()
    {
        _currentCapicity = 0;
        _thirdStructure.ResourceCreate += ResourceRecycling;
    }

    private void OnDisable()
    {
        _thirdStructure.ResourceCreate -= ResourceRecycling;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (player.GetComponentInChildren<FirstResource>())
            {
                var resource = player.GetComponentInChildren<FirstResource>();
                if (_currentCapicity < _maxCapicity)
                {
                    player.TransitResource(resource, transform);
                    _currentCapicity++;
                    TransitMaterialToCreateResources(resource);
                    _firstResourcesList.Add(resource);
                    player.CurrentBagCapicity--;
                    if (_currentCapicity <= 0)
                        _currentCapicity = 0;
                }
            }
        }
    }

    private void TransitMaterialToCreateResources(FirstResource firstResource)
    {
        _thirdStructure.FirstResources.Add(firstResource);
    }

    protected override void ResourceRecycling()
    {
        Destroy(_firstResourcesList.Last().gameObject);
        _firstResourcesList.Remove(_firstResourcesList.Last());
        _currentCapicity--;
    }
}
