using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PositionsToParticles : MonoBehaviour
{
  new ParticleSystem particleSystem;
  ParticleSystem.Particle[] particles;
  public float particleSize = 5;

  void Start ()
  {
    var positionsWithColors = new List<string> (System.IO.File.ReadAllLines ("Assets/model.txt"));

    particleSystem = GetComponent<ParticleSystem> ();
    if (particleSystem == null)
    {
      Debug.Log ("ParticleSystem not found!");
      return;
    }

    particles = new ParticleSystem.Particle[positionsWithColors.Count];

    for (int index = 0; index < particles.Length; index+=100)
    {
      particles[index].startSize = particleSize;

      var positionWithColor = positionsWithColors[index].Split (' ');

      particles[index].position = new Vector3
      {
        x = float.Parse (positionWithColor[0], CultureInfo.InvariantCulture),
        y = float.Parse (positionWithColor[2], CultureInfo.InvariantCulture),
        z = float.Parse (positionWithColor[1], CultureInfo.InvariantCulture)
      };

      var particleColor = new Color
      {
        r = uint.Parse (positionWithColor[3]),
        g = uint.Parse (positionWithColor[4]),
        b = uint.Parse (positionWithColor[5]),
        a = 1
      };
      particles[index].startColor = particleColor;
    }

    particleSystem.SetParticles (particles);
    particleSystem.Pause ();
  }

  void Update ()
  {
    if ((Input.GetKeyDown (KeyCode.Plus) || Input.GetKeyDown (KeyCode.KeypadPlus))
        && particleSize < 100)
      particleSize += 0.001f;

    if ((Input.GetKeyDown (KeyCode.Minus) || Input.GetKeyDown (KeyCode.KeypadMinus))
        && particleSize > 0)
      particleSize -= 0.001f;

    for (int index = 0; index < particles.Length; index++)
      particles[index].startSize = particleSize;

    particleSystem.SetParticles (particles);
  }
}
