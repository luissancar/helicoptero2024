using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
   
    public float rotVel1 = 100;
    public float rotVel2 = 75;
    public int maxVel = 500;
    public int vel;


    private float giroHorizontal;
    private float giroVertical;
    private Rigidbody rb;



    private bool arrancado;


    public AudioSource audioMotor;
    
    
    public float thrust = 10f;
    public bool maintainHeight = false; // Variable para controlar si se mantiene en la altura

    
   
    
    
    
    void Start()
    {
        vel = 0;
        arrancado = false;
        rb = GetComponent<Rigidbody>();
        audioMotor = GetComponent<AudioSource>();
    }

  
    void Update()
    {
        if (!Arrancado())
            return;
        MovimientoUpdate();
        UpDown();
        ControlarVelocidad();

    }

    private void UpDown()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.up * thrust, ForceMode.Impulse);
            maintainHeight = true; // Mantener la altura cuando se presiona la tecla W
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector3.down * thrust, ForceMode.Impulse);
            maintainHeight = true; // Mantener la altura cuando se presiona la tecla S
        }
        else
        {
            if (maintainHeight)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Detener el movimiento en el eje Y
                maintainHeight = false; // Ya no mantener la altura
            }
        }
    }


    private void MovimientoUpdate()
    { 
        if (Altitud() > 5 )
            rb.useGravity = false;
        else
            rb.useGravity = true;

        giroHorizontal = Input.GetAxis("Horizontal");
      //  giroVertical = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.K))
        {
            transform.Rotate(Vector3.forward * (rotVel1 * Time.deltaTime));
        }
        
        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(Vector3.back * (rotVel1 * Time.deltaTime));
        }
        
    }
    private void FixedUpdate()
    {
        MovimientoFixedUpdate();
    }

    private void MovimientoFixedUpdate()
    {
        
        transform.position += transform.forward * (vel * Time.fixedDeltaTime);
        // Girar el avión
        transform.Rotate(Vector3.up * (giroHorizontal * rotVel1 * Time.fixedDeltaTime));
        transform.Rotate(Vector3.right * (-giroVertical * rotVel2 * Time.fixedDeltaTime));

    }

    private bool Arrancado()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            arrancado = !arrancado;
            if (arrancado)
            {
                GetComponent<MoverHelices>().activar=true;
                audioMotor.Play();
            }
            else
            {
                GetComponent<MoverHelices>().activar=false;
                audioMotor.Stop();
            }
        }

        return arrancado;
    }

    private void ControlarVelocidad()
    {
        if (Input.GetKeyDown(KeyCode.M))
            if (vel < maxVel)
                vel+=5;
        if (Input.GetKeyDown(KeyCode.N))
            if (vel >= 5)
                vel-=5;
       
    }

    
    

    public int Velocidad()
    {
       // Vector3 velocidad = rb.velocity;
        //return velocidad.magnitude*10;
        return vel;
    }


    public float Altitud()
    {
        
        float distanciaAlSuelo=0;
        float distanciaMaxima = 10000f;
        // Lanza un rayo hacia abajo desde la posición del objeto
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, distanciaMaxima))
        {
            // Calcula la distancia al suelo
            distanciaAlSuelo = hit.distance;

        }

        return distanciaAlSuelo;
    }
}
