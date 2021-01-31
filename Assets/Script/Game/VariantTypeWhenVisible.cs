using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariantTypeWhenVisible : MonoBehaviour {
    #region Variables

    public GameObject[] types;

    #endregion

    #region Behaviour

    private void OnBecameVisible() {
        int hit = Random.Range(0, types.Length);

        for (int i = 0; i < types.Length; ++i) {
            types[i].SetActive(i == hit);
        }
    }

    #endregion
}
