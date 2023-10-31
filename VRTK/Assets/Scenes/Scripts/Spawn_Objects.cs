using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class Spawn_Objects : MonoBehaviour
{
    [Header("Elementos a invocar")]
    public GameObject[] fruits_prefabs;
    public int amount;

    [Header("Zona de invocación")]
    public NavMeshSurface navMeshSurface;
    public float scale = 0.1f;
    public float height = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = navMeshSurface.GetComponent<Renderer>().bounds;
        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;

        for (int i = 0; i < amount; i++)
        {
            foreach (GameObject prefab in fruits_prefabs)
            {
                Vector3 posicionAleatoria = GenerarPosicionAleatoriaEnNavMesh(center, extents);
                posicionAleatoria.y = height; // Ajusta la altura deseada en "y"
                Instantiate(prefab, posicionAleatoria, Quaternion.identity);
            }
        } 
    }

    // Funcion para obtener posiciones aleatorias en el Nav Mesh
    Vector3 GenerarPosicionAleatoriaEnNavMesh(Vector3 center, Vector3 extents)
    {
        float x = Random.Range(center.x - extents.x, center.x + extents.x);
        float z = Random.Range(center.z - extents.z, center.z + extents.z);
        
        // Ajusta la altura a la altura deseada en "y"
        float y = height;

        return new Vector3(x, y, z);
    }
}
