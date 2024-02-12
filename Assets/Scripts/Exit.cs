using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private GameObject endingUIContainer;

    [SerializeField]
    private GameObject dumbDeath;

    [SerializeField]
    private GameObject physicalDeath;

    [SerializeField]
    private GameObject dumbEnding;

    [SerializeField]
    private GameObject smartEnding;

    public void SmartEnding()
    {
        Instantiate(smartEnding, endingUIContainer.transform);
    }

    public void DumbEnding()
    {
        Instantiate(dumbEnding, endingUIContainer.transform);
    }

    public void DumbDeath()
    {
        Instantiate(dumbDeath, endingUIContainer.transform);
    }

    public void PhysicalDeath()
    {
        Instantiate(physicalDeath, endingUIContainer.transform);
    }
}
