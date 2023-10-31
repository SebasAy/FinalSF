using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPosOrb : AudioSyncer
{
    public float maxYPosition = 2.0f; // La posici�n m�xima en el eje Y a la que llegar� el objeto
    public float minYPosition = 0.0f; // La posici�n m�nima en el eje Y a la que regresar� el objeto
    public float moveSpeed = 1.0f; // Velocidad de movimiento gradual

    private Vector3 initialPosition;
    private float targetYPosition;

    private IEnumerator MoveToTargetPosition(float _targetY)
    {
        float _timer = 0;

        while (_timer < timeToBeat)
        {
            float t = _timer / timeToBeat;
            float newY = Mathf.Lerp(initialPosition.y, _targetY, Mathf.Sin(t * Mathf.PI / 2)); // Aplica una funci�n senoidal
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            _timer += Time.deltaTime;

            yield return null;
        }

        // Asegurarse de que el objeto est� en la posici�n exacta despu�s de la transici�n
        transform.position = new Vector3(transform.position.x, _targetY, transform.position.z);

        m_isBeat = false;
    }

    public override void OnBeat()
    {
        base.OnBeat();

        initialPosition = transform.position;

        // Determina una nueva posici�n Y de destino al azar dentro del rango especificado
        targetYPosition = Random.Range(minYPosition, maxYPosition);

        StartCoroutine("MoveToTargetPosition", targetYPosition);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}