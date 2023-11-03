using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pernil : AudioSyncer
{
    public Terrain terrain;
    public float heightMultiplier = 2.0f;
    public float sensitivity = 1.0f;
    public Vector3 beatScale; // DeclaraciÃ³n de la variable beatScale
    public Vector3 restScale;

    private TerrainData terrainData;
    private float[,] originalHeights; // Almacena las alturas originales del terreno

    private float[] audioSpectrum;

    private IEnumerator MoveToScale(Vector3 _target)
    {
        Vector3 _curr = transform.localScale;
        Vector3 _initial = _curr;
        float _timer = 0;

        while (_curr != _target)
        {
            _curr = Vector3.Lerp(_initial, _target, _timer / timeToBeat);
            _timer += Time.deltaTime;

            transform.localScale = _curr;

            yield return null;
        }

        m_isBeat = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;

        transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatScale);
    }

    // Llamado en cada fotograma
    [System.Obsolete]
    void Update()
    {
        if (Application.isPlaying)
        {
            audioSpectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Blackman);
            ModifyTerrain();
        }
    }

    void ModifyTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;
        float[,] heights = terrainData.GetHeights(0, 0, width, height);

        int halfWidth = width / 2;
        int halfHeight = height / 2;

        for (int x = 0; x < halfWidth; x++)
        {
            for (int y = 0; y < halfHeight; y++)
            {
                float sample = audioSpectrum[x] * audioSpectrum[y] * sensitivity;
                ModifyHeight(heights, x, y, sample);
                ModifyHeight(heights, width - x - 1, y, sample);
                ModifyHeight(heights, x, height - y - 1, sample);
                ModifyHeight(heights, width - x - 1, height - y - 1, sample);
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }

    void ModifyHeight(float[,] heights, int x, int y, float sample)
    {
        float heightValue = heights[x, y];
        heightValue += sample * heightMultiplier;
        heightValue = Mathf.Clamp01(heightValue);
        heights[x, y] = heightValue;
    }

    // Restablecer el terreno a un plano al inicio del juego
    public override void Start()
    {
        base.Start();  

        if (Application.isPlaying)
        {
            terrainData = terrain.terrainData;
            int width = terrainData.heightmapResolution;
            int height = terrainData.heightmapResolution;
            originalHeights = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    originalHeights[x, y] = 0f; // Restablece la altura a 0 para un plano
                }
            }
            terrainData.SetHeights(0, 0, originalHeights);
        }
    }
}