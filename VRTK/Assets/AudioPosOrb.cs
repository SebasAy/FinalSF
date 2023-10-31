using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPsdOrb : AudioSyncer
{
    public Transform centerObject; // El objeto alrededor del cual orbitará
    public float orbitSpeed = 1.0f; // Velocidad de órbita
    public float orbitHeight = 1.0f; // Altura de órbita
    public float orbitAmplitude = 1.0f; // Amplitud de la órbita
    public TrailRenderer trailRenderer; // Referencia al Trail Renderer

    private Vector3 initialPosition;
    private Vector3 beatPosition;

    private IEnumerator MoveToPosition(Vector3 _target)
    {
        Vector3 _curr = transform.position;
        float _timer = 0;

        while (_timer < timeToBeat)
        {
            _curr = Vector3.Lerp(initialPosition, _target, _timer / timeToBeat);
            _timer += Time.deltaTime;

            transform.position = _curr;

            _UpdateTrailRenderer(_curr); // Actualiza el Trail Renderer

            yield return null;
        }

        m_isBeat = false;
    }

    public void Start()
    {
        initialPosition = transform.position;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat)
        {
            beatPosition = new Vector3(
                initialPosition.x + Mathf.Sin(Time.time * orbitSpeed) * orbitAmplitude,
                initialPosition.y + Mathf.Sin(Time.time * orbitSpeed) * orbitHeight,
                initialPosition.z + Mathf.Cos(Time.time * orbitSpeed) * orbitAmplitude
            );

            StopCoroutine("MoveToPosition");
            StartCoroutine("MoveToPosition", beatPosition);
        }
    }

    // Función para actualizar el Trail Renderer
    private void _UpdateTrailRenderer(Vector3 newPosition)
    {
        if (trailRenderer != null)
        {
            trailRenderer.AddPosition(newPosition);
        }
    }
}