using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatColors : AudioSyncer
{
    public Color targetColor; // Define el color final deseado (por ejemplo, rojo).
    public float colorChangeSpeed = 2.0f; // Velocidad de cambio de color.

    private Renderer m_renderer;
    private Material m_material;
    private Color initialColor;

    private void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_material = m_renderer.material;
        initialColor = m_material.color; // Almacena el color inicial desde el inspector.
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;

        // Cambia gradualmente el color hacia el color inicial (negro) cuando no es un beat.
        m_material.color = Color.Lerp(m_material.color, initialColor, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        // Cambia gradualmente el color hacia el color deseado (por ejemplo, rojo) en un beat.
        StopCoroutine("MoveToColor");
        StartCoroutine("MoveToColor", targetColor);
    }

    private IEnumerator MoveToColor(Color targetColor)
    {
        Color currentColor = m_material.color;
        float timer = 0;

        while (currentColor != targetColor)
        {
            currentColor = Color.Lerp(initialColor, targetColor, timer / timeToBeat);
            timer += Time.deltaTime;

            m_material.color = currentColor;

            yield return null;
        }

        m_isBeat = false;
    }
}
