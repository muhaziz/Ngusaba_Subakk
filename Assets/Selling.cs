using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selling : MonoBehaviour
{

    [SerializeField] GameObject sellPanel;

    public void BeginSelling()
    {
        sellPanel.SetActive(true);
    }

    public void StopSelling()
    {
        sellPanel.SetActive(false);
    }
}
