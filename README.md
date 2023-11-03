# FinalSF

# Documentación de la evidencia evaluativa del proyecto final

## Enlace al video en YouTube
https://youtu.be/HY1RaoZI3bg

## Explicación de la solución

Este proyecto ofrece una experiencia de navegación espacial única, donde una nave espacial viaja por el espacio, y la música que reproduce afecta el entorno. La nave espacial tiene la capacidad de interactuar con el entorno, causando la rotación y el cambio de color de planetas y espectros de la nave. Para lograr esto, hemos utilizado Unity y una variedad de activos y herramientas, incluido el motor de realidad virtual VRTK para el movimiento dentro de la nave.

![image](https://github.com/jfUPB/evaluaciones-2023-20-SebasAy/assets/68132813/f692074d-6878-49de-9aa6-e1c420ad35a7)

Aca podemos ver la nave, esta fue modificada para poder ver todo el espacio que lo rodea, este tiene unos cubos blancos,estos se mueven segun la musica haciendo de limites de la nave y base del poder de esta.

![image](https://github.com/jfUPB/evaluaciones-2023-20-SebasAy/assets/68132813/e15ce4c4-5f3e-4d05-8110-4bc61fcef86b)

Aqui se ve el terreno, este tiene un material que asemeja a la tela del espacio la cual se modifica por la musica y dentro del contexto por el poder de la nave. para este usamos reuido de perlin ya que queriamos formas diferente cada vez ademas de este efcto de ruptura desorganizada.

![image](https://github.com/jfUPB/evaluaciones-2023-20-SebasAy/assets/68132813/1382940e-5d84-424b-9f9f-0de9555c5542)

en esta imagen podemos ver mas cosas, podemos notar unos cubos orbitando a la nave, estos demuestran parte del poder de la nave, ademas estos cambian de escala y de color viendose afectados por la musica, tambein podemos ver unos planetas, estos tambien orbitan la nave y cambain su escala segun el sonido, tambien tenemos un cometa orbitandolo, este cambia su posicion en y segun el ritmo, ademas podemos ver una onda que sale de la parte superior de la nave, este sale cada cierto tiempo. Ademas tenemos otras particulas que salen segun el ritmo de la musica.

El codigo esta hecho de una manera en la cual con el audiomanager es facil cambiar la cancion y esta seguira afectando la experiencia segun su propio ritmo ademas, cada objeto se afecta segun el canal enlazada, estos canales son limites, si el sonido pasa esos limites manda una señal que hace que los diferentes codigos interactuen, como la escala, la posicion y el color, y en algunos casos si se activa o no, como las particulas.

En este proyecto, utilizamos un sistema de sincronización de audio para que la música afecte dinámicamente el entorno y la nave espacial. Este sistema se basa en dos componentes clave: AudioSpectrum y AudioSyncer. Aquí te explicaremos cómo funcionan estos componentes y cómo se conectan.

### AudioSpectrum
El AudioSpectrum es un componente que captura y procesa los datos del espectro de audio. Utiliza la función AudioListener.GetSpectrumData para obtener un espectro de frecuencias de la música que se está reproduciendo. En nuestro proyecto, nos centramos en un solo valor del espectro, spectrumValue, que representa una característica específica de la música.

```csharp

public class AudioSpectrum : MonoBehaviour
{
    private void Update()
    {
        // Obtén los datos del espectro de audio
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        // Asigna el valor del espectro (spectrumValue) para su uso en otros componentes.
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }
    }

    // ...
}
```
### AudioSyncer
AudioSyncer es un componente que hereda de AudioSpectrum y se encarga de detectar los "beats" en la música. Funciona comparando el valor del espectro con un valor umbral (bias) y un tiempo mínimo entre beats (timeStep). Cuando se detecta un beat, se llama a la función OnBeat(). Esto permite que otros componentes respondan al ritmo de la música.

```csharp
public class AudioSyncer : MonoBehaviour
{
    public float bias;
    public float timeStep;

    // ...

    public virtual void OnUpdate()
    {
        // Actualiza el valor del espectro de audio
        m_previousAudioValue = m_audioValue;
        m_audioValue = AudioSpectrum.spectrumValue;

        // Comprueba si se produce un beat
        if (m_previousAudioValue > bias && m_audioValue <= bias)
        {
            if (m_timer > timeStep)
                OnBeat(); // Llama a la función de beat
        }

        // ...
    }

    // ...
}
```
Este fragmento de código representa la lógica básica que permite detectar beats en la música y responder a ellos en otros componentes heredados de AudioSyncer, los cuales son todos aquellos que reaccionaan a la musica, como el terreno, los planetas, los limites de la nave, el cometa y los cubos que orbitan

### Orbiting
El sistema de orbiting en este proyecto permite que objetos, como planetas o cometas, giren alrededor de un punto central. Este sistema es fundamental para crear un efecto espacial dinámico y responder a la música que se está reproduciendo. A continuación, se explica cómo funciona y se proporciona un fragmento de código que demuestra su uso.

Funcionamiento del Sistema
El sistema de orbiting se basa en la rotación de objetos alrededor de un punto central en un espacio 3D. En nuestro proyecto, hemos utilizado este sistema para hacer que los planetas y cometas giren alrededor de un objeto central, como la nave espacial. El sistema consta de los siguientes elementos clave:

Centro de Orbita (CenterObject): Este es el objeto central alrededor del cual giran otros objetos, como planetas o cometas. En nuestro caso, el objeto central es la nave espacial.

Prefab de Objetos: Hemos creado prefabs de objetos, como planetas y cometas, que se instancian en posiciones específicas alrededor del centro de órbita.

Código de Orbiting: El código asociado al sistema de orbiting permite que los objetos giren gradualmente alrededor del centro de órbita. Esto se logra utilizando la función Transform.RotateAround, que aplica rotación a un objeto en el espacio.

```csharp

public class Orbiting : MonoBehaviour
{
    [SerializeField] GameObject centerObject;
    [SerializeField] float orbitSpeed = 30f;

    void Update()
    {
        // Hacer que los objetos orbiten alrededor del centro de órbita (centerObject).
        transform.RotateAround(centerObject.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
```

### Creditos

Agradecemos a los siguientes creadores y recursos por sus contribuciones a este proyecto:

Nave espacial: Scifi Platform Stage Scene Baked por McWinterL bajo licencia Creative Commons Attribution.
Planetas: Stylized Planet Pack.
Skyboxes: Free Skyboxes - Space.
Motor de Realidad Virtual: VRTK - Virtual Reality Toolkit.
