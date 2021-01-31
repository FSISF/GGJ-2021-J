using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneManager : MonoBehaviour
{
    public Text cryFace, restarting;
    
    // Start is called before the first frame update
    void Start()
    {
        Common.Timer(2, () =>
        {
            cryFace.enabled = true;
        });
        
        Common.Timer(4, () =>
        {
            restarting.enabled = true;
        });
        
        Common.Timer(8, () =>
        {
            SceneManager.LoadScene(0);
        });

        cryFace.enabled = restarting.enabled = false;
    }
}
