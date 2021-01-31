using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;

public class RepairCatus : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Catus>().SetState(eCatusState.Hide);
            child.gameObject.SetActive(false);
        }

        GameEventManager.Instance.CactusCompleted += () =>
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Catus>().SetState(eCatusState.Grow);
            }
        };
    }
}
