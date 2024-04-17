using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverHelices : MonoBehaviour
{
   
    public GameObject[] helices;
    public float velocidadGiro = 0f;
    public bool activar ;
   

    private void Start()
    {
       
        activar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activar)
            return;
        velocidadGiro = 1000;// GetComponent<PlaneController>().Velocidad()*100+100;
        for (int i = 0; i < helices.Length; i++)
        {
            helices[i].transform.Rotate(Vector3.down * velocidadGiro * Time.deltaTime);
            helices[++i].transform.Rotate(Vector3.right * velocidadGiro * Time.deltaTime);
        }
    }

}
