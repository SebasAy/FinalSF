using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatPS : AudioSyncer
{
    public ParticleSystem Ps; // El sistema de part�culas al que deseas crear una nueva emisi�n.

    private ParticleSystem.MainModule particleMainModule;
    public int emVal;

    private void Start()
    {
        particleMainModule = Ps.main;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;
    }

    public override void OnBeat()
    {
        base.OnBeat();

        // Crea una nueva emisi�n de part�culas en cada beat.
        Ps.Emit(emVal); // representa la cantidad de part�culas que se emiten en cada beat.
        m_isBeat = false; // Restablece la bandera de beat.
    }
}