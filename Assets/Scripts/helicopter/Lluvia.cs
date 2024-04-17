using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lluvia : MonoBehaviour
{

    private bool _lluvia;
    public GameObject particleSystemLluvia;
    void Start()
    {
        _lluvia = false;
        particleSystemLluvia.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _lluvia = !_lluvia;
            particleSystemLluvia.SetActive(_lluvia);
        }
    }
}
