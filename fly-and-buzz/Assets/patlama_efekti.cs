using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patlama_efekti : MonoBehaviour
{

    void OnEnable()
    {
        Invoke("pasif_ol", 0.5f);
    }


    void pasif_ol()
    {
        gameObject.SetActive(false);
    }
}
