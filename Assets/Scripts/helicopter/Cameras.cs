using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// Camaras
public class Cameras : MonoBehaviour
{
    [System.Serializable]
    public struct CameraKey
    {
        public GameObject camera;
        public KeyCode keyCode;
    }

    public CameraKey[] cameraArray;

    // Cámaras automáticas
    private int camaraActual;
    public bool automatico;
    private bool _estaActivadaCoorutina;
    public int frecuenciaCambio;


    private void Start()
    {
        foreach (var cam in cameraArray)
        {
            cam.camera.GetComponent<AudioListener>().enabled = false;
            cam.camera.GetComponent<Camera>().enabled = false;
        }

        cameraArray[0].camera.GetComponent<Camera>().enabled = true;
        cameraArray[0].camera.GetComponent<AudioListener>().enabled = true;

        camaraActual = 0;
        automatico = false;
        _estaActivadaCoorutina = false;
    }

    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Z)))
        {
            automatico = !automatico;
            if (_estaActivadaCoorutina)
            {
                _estaActivadaCoorutina = false;
                StartCoroutine(CamarasAutomaticas());
            }
            else
            {
                _estaActivadaCoorutina = true;
                StartCoroutine(CamarasAutomaticas());
            }
        }

        if (Input.anyKeyDown)
        {
            int i = 0;
            foreach (var cameraKey in cameraArray)
            {
                if (Input.GetKeyDown(cameraKey.keyCode))
                {
                    //Desactivar todas las cámaras
                    foreach (var cam in cameraArray)
                    {
                        cam.camera.GetComponent<AudioListener>().enabled = false;
                        cam.camera.GetComponent<Camera>().enabled = false;
                    }

                    cameraKey.camera.GetComponent<Camera>().enabled = true;
                    cameraKey.camera.GetComponent<AudioListener>().enabled = true;
                    camaraActual = i;
                }

                i++;
            }
        }
    }

    private IEnumerator CamarasAutomaticas()
    {
        while (true)
        {
            if (!_estaActivadaCoorutina)
            {
                yield break;
            }

            yield return new WaitForSeconds(frecuenciaCambio);
            foreach (var cam in cameraArray)
            {
                cam.camera.GetComponent<AudioListener>().enabled = false;
                cam.camera.GetComponent<Camera>().enabled = false;
            }

            int _cs = _CamaraSiguiente();
            cameraArray[_cs].camera.GetComponent<AudioListener>().enabled = true;
            cameraArray[_cs].camera.GetComponent<Camera>().enabled = true;
        }
    }

    private int _CamaraSiguiente()
    {
        if (camaraActual==cameraArray.Length-1)
        {
            camaraActual = 0;
        }
        else
        {
            camaraActual++;
        }

        return camaraActual;
    }

    public Camera CamaraActual()
    {
        return cameraArray[camaraActual].camera.GetComponent<Camera>();
    }
}













