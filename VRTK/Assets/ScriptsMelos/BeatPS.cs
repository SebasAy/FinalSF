using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatPS : AudioSyncer
{
    public ParticleSystem particleSystem; // El sistema de part�culas al que deseas cambiar la velocidad.
    public float minSpeed = 1.0f; // Velocidad m�nima de las part�culas.
    public float maxSpeed = 5.0f; // Velocidad m�xima de las part�culas.

    private ParticleSystem.MainModule particleMainModule;

    private void Start()
    {
        particleMainModule = particleSystem.main;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;

        // Cambia gradualmente la velocidad de las part�culas hacia el valor m�nimo cuando no es un beat.
        float currentSpeed = particleMainModule.startSpeed.constant;
        float newSpeed = Mathf.Lerp(currentSpeed, minSpeed, restSmoothTime * Time.deltaTime);
        particleMainModule.startSpeed = newSpeed;
    }

    public override void OnBeat()
    {
        base.OnBeat();

        // Cambia gradualmente la velocidad de las part�culas hacia el valor m�ximo en un beat.
        StopCoroutine("ChangeParticleSpeed");
        StartCoroutine("ChangeParticleSpeed", maxSpeed);
    }

    private IEnumerator ChangeParticleSpeed(float targetSpeed)
    {
        float currentSpeed = particleMainModule.startSpeed.constant;
        float timer = 0;

        while (currentSpeed != targetSpeed)
        {
            currentSpeed = Mathf.Lerp(minSpeed, targetSpeed, timer / timeToBeat);
            particleMainModule.startSpeed = currentSpeed;
            timer += Time.deltaTime;

            yield return null;
        }

        m_isBeat = false;
    }
}