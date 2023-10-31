using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbir : MonoBehaviour
{
    public GameObject centerObject; // Objeto alrededor del cual orbitarán los objetos
    public GameObject orbiterPrefab; // Prefab del objeto que orbitará
    public float attractionForce = 5f; // Fuerza de atracción del objeto central
    public float orbitSpeed = 50f; // Velocidad de órbita de los objetos

    private List<CustomMover3D> movers = new List<CustomMover3D>();

    void Start()
    {
        int numberOfMovers = 80;
        for (int i = 0; i < numberOfMovers; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(-7f, 7f), Random.Range(-7f, 7f), Random.Range(-7f, 7f));
            Vector3 randomVelocity = new Vector3(Random.Range(0f, 5f), Random.Range(0f, 5f), Random.Range(0f, 5f));
            CustomMover3D mover = new CustomMover3D(Random.Range(0.2f, 1f), randomVelocity, randomLocation);
            movers.Add(mover);
        }
    }

    void FixedUpdate()
    {
        foreach (CustomMover3D mover in movers)
        {
            Rigidbody moverBody = mover.body;
            Vector3 force = CalculateAttraction(moverBody.position, centerObject.transform.position);

            mover.ApplyForce(force);
            mover.CalculatePosition();
        }
    }

    Vector3 CalculateAttraction(Vector3 moverPosition, Vector3 centerPosition)
    {
        Vector3 direction = centerPosition - moverPosition;
        float distance = direction.magnitude;

        direction.Normalize();
        float strength = (attractionForce * centerObject.GetComponent<Rigidbody>().mass * movers[0].body.mass) / (distance * distance);
        return direction * strength;
    }
}

public class CustomMover3D
{
    public Rigidbody body;
    private Transform transform;
    private GameObject mover;

    private Vector3 maximumPos;

    public CustomMover3D(float randomMass, Vector3 initialVelocity, Vector3 initialPosition)
    {
        mover = GameObject.CreatePrimitive(PrimitiveType.Cube); // Cambio a Cube
        Object.Destroy(mover.GetComponent<BoxCollider>()); // Cambio a BoxCollider
        transform = mover.transform;

        mover.AddComponent<Rigidbody>();
        body = mover.GetComponent<Rigidbody>();
        body.useGravity = false;

        Renderer renderer = mover.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.color = Color.magenta; // Cambio de color a morado
        mover.transform.localScale = new Vector3(randomMass, randomMass, randomMass);

        body.mass = 1;
        body.position = initialPosition;
        body.velocity = initialVelocity;
        
    }

    public void ApplyForce(Vector3 force)
    {
        body.AddForce(force, ForceMode.Force);
    }

    public void CalculatePosition()
    {
        CheckEdges();
    }

    private void CheckEdges()
    {
        Vector3 velocity = body.velocity;
        if (transform.position.x > maximumPos.x || transform.position.x < -maximumPos.x)
        {
            velocity.x *= -1 * Time.deltaTime;
        }
        if (transform.position.y > maximumPos.y || transform.position.y < -maximumPos.y)
        {
            velocity.y *= -1 * Time.deltaTime;
        }
        if (transform.position.z > maximumPos.z || transform.position.z < -maximumPos.z)
        {
            velocity.z *= -1 * Time.deltaTime;
        }
        body.velocity = velocity;
    }

   
}
