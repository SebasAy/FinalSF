using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatPS : AudioSyncer
{
    public ParticleSystem Ps; // El sistema de partículas al que deseas crear una nueva emisión.

    private ParticleSystem.MainModule particleMainModule;
    public int emVal;

    public override void Start()
    {
        base.Start();
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

        // Crea una nueva emisión de partículas en cada beat.
        Ps.Emit(emVal); // representa la cantidad de partículas que se emiten en cada beat.
        m_isBeat = false; // Restablece la bandera de beat.
    }
}