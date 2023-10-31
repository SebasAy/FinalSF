using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioColor : AudioSyncer
{
    public Color[] beatColors;
    public Color restColor;

    [SerializeField] public float restSmoothTime = 1.0f;

    private int m_randomIndx;
    private Renderer m_renderer;
    private Material m_material;

    private IEnumerator MoveToColor(Color _target)
    {
        Color _curr = m_material.color;
        Color _initial = _curr;
        float _timer = 0;

        while (_curr != _target)
        {
            _curr = Color.Lerp(_initial, _target, _timer / timeToBeat);
            _timer += Time.deltaTime;

            m_material.color = _curr;

            yield return null;
        }

        m_isBeat = false;
    }

    private Color RandomColor()
    {
        if (beatColors == null || beatColors.Length == 0) return Color.white;
        m_randomIndx = Random.Range(0, beatColors.Length);
        return beatColors[m_randomIndx];
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;

        m_material.color = Color.Lerp(m_material.color, restColor, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        Color _c = RandomColor();

        StopCoroutine("MoveToColor");
        StartCoroutine("MoveToColor", _c);
    }

    private void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_material = m_renderer.material;
    }
}

