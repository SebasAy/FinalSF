using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiting : MonoBehaviour
{
    [SerializeField] GameObject centerObject; // Objeto alrededor del cual orbitar�n los hex�gonos
    [SerializeField] GameObject hexagonPrefab; // Prefab del hex�gono a crear
    [SerializeField] int numberOfHexagons = 3; // N�mero de hex�gonos
    [SerializeField] float orbitRadius = 3f; // Radio de la �rbita
    [SerializeField] float orbitSpeed = 30f; // Velocidad de �rbita en grados por segundo

    private List<GameObject> hexagons = new List<GameObject>();

    void Start()
    {
        // Crear y posicionar los hex�gonos alrededor del objeto central
        for (int i = 0; i < numberOfHexagons; i++)
        {
            float angle = i * (360f / numberOfHexagons);
            Vector3 hexagonPosition = centerObject.transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * orbitRadius;

            GameObject hexagon = Instantiate(hexagonPrefab, hexagonPosition, Quaternion.identity);
            hexagons.Add(hexagon);
        }
    }

    void Update()
    {
        // Hacer que los hex�gonos orbiten alrededor del objeto central
        foreach (GameObject hexagon in hexagons)
        {
            hexagon.transform.RotateAround(centerObject.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}


