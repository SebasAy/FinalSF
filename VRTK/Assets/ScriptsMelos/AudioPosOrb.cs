using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPosOrb : AudioSyncer
{
    public float maxYPosition = 3.0f; // La posición máxima en el eje Y a la que llegará el objeto
    public float minYPosition = -3.0f; // La posición mínima en el eje Y a la que regresará el objeto
    public float moveSpeed = 1.0f; // Velocidad de movimiento gradual

    public Vector3 initialPosition;
    private float targetYPosition;

    private IEnumerator MoveToTargetPosition(float _targetY)
    {
        float _timer = 0;

        while (_timer < timeToBeat)
        {
            float t = _timer / timeToBeat;
            float newY = Mathf.Lerp(initialPosition.y, _targetY, Mathf.Sin(t * Mathf.PI / 2)); // Aplica una función senoidal
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            _timer += Time.deltaTime;

            yield return null;
        }

        // Asegurarse de que el objeto esté en la posición exacta después de la transición
        transform.position = new Vector3(transform.position.x, _targetY, transform.position.z);

        m_isBeat = false;
    }

    public override void OnBeat()
    {
        base.OnBeat();

        initialPosition = transform.position;

        // Determina una nueva posición Y de destino al azar dentro del rango especificado
        targetYPosition = Random.Range(minYPosition, maxYPosition);

        StartCoroutine("MoveToTargetPosition", targetYPosition);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}