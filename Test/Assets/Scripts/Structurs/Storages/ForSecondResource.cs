using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ForSecondResource : Storage
{
    [SerializeField] private ThirdStructure _thirdStructure;

    private List<SecondResource> _firstResourcesList = new List<SecondResource>();


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
            if (player.GetComponentInChildren<SecondResource>())
            {
                var resource = player.GetComponentInChildren<SecondResource>();
                if (_currentCapicity < _maxCapicity)
                {
                    player.TransitResource(resource, transform);
                    TransitMaterialToCreateResources(resource);
                    _currentCapicity++;
                    _firstResourcesList.Add(resource);
                    player.CurrentBagCapicity--;
                    if (_currentCapicity <= 0)
                        _currentCapicity = 0;
                }
            }
        }
    }

    private void TransitMaterialToCreateResources(SecondResource secondResource)
    {
        _thirdStructure.SecondResources.Add(secondResource);
    }

    protected override void ResourceRecycling()
    {
        Destroy(_firstResourcesList.Last().gameObject);
        _firstResourcesList.Remove(_firstResourcesList.Last());
        _currentCapicity--;
    }
}
