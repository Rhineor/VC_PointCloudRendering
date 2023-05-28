using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PositionsToSpheres: MonoBehaviour
{
  public float size;

  private GameObject container;

  void Start ()
  {
    container = new GameObject ();

    var positionsWithColors = new List<string> (System.IO.File.ReadAllLines ("Assets/model.txt"));

    for (int index = 0; index < positionsWithColors.Count; index+=10)
    {
      var positionWithColor = positionsWithColors[index].Split (' ');

      var sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
      sphere.transform.position = new Vector3
      {
        x = float.Parse (positionWithColor[0], CultureInfo.InvariantCulture),
        y = float.Parse (positionWithColor[1], CultureInfo.InvariantCulture),
        z = float.Parse (positionWithColor[2], CultureInfo.InvariantCulture)
      };
      sphere.GetComponent<MeshRenderer> ().material.color = new Color
      {
        r = uint.Parse (positionWithColor[3]),
        g = uint.Parse (positionWithColor[4]),
        b = uint.Parse (positionWithColor[5]),
        a = 1
      };
      sphere.transform.localScale = new Vector3 (size, size, size);

      sphere.transform.SetParent (container.transform);
    }
  }

  void Update ()
  {
    if ((Input.GetKeyDown (KeyCode.Plus) || Input.GetKeyDown (KeyCode.KeypadPlus))
        && size < 100)
      size += 0.01f;

    if ((Input.GetKeyDown (KeyCode.Minus) || Input.GetKeyDown (KeyCode.KeypadMinus))
        && size > 0)
      size -= 0.01f;

    for (int index = 0; index < container.transform.childCount; index++)
    {
      var sphereTransform = container.transform.GetChild (index);
      sphereTransform.localScale = new Vector3 (size, size, size);
    }
  }
}
