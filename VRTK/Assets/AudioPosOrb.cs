using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPosOrb : AudioSyncer
{
    private IEnumerator MoveToPosition(Vector3 _target)
    {
        Vector3 _curr = transform.position;
        Vector3 _initial = _curr;
        float _timer = 0;

        while (_timer < timeToBeat)
        {
            float t = _timer / timeToBeat;
            _curr = Vector3.Lerp(_initial, _target, t);
            _curr.y = Mathf.Lerp(_initial.y, _target.y + beatScale.y, Mathf.Sin(t * Mathf.PI)); // Aplica la función senoidal a la posición en Y
            _timer += Time.deltaTime;

            transform.position = _curr;

            yield return null;
        }

        m_isBeat = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;

        transform.position = Vector3.Lerp(transform.position, restPosition, restSmoothTime * Time.deltaTime); // Ajusta la posición en lugar de escala
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToPosition");
        StartCoroutine("MoveToPosition", beatPosition);
    }

    public Vector3 beatPosition;
    public Vector3 restPosition;
}