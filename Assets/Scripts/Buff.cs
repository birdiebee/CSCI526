using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField]
    private int buff_amt;

    private bool collected = false;

    public int GetBuffAmount()
    {
        return buff_amt;
    }

    public bool GetCollectionStatus()
    {
        return collected;
    }

    public void CollectBuff()
    {
        collected = true;
    }
}
