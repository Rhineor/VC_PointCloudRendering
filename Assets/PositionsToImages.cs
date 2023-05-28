using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PositionsToImages : MonoBehaviour
{
  public Canvas canvas;
  public float size;

  void Start ()
  {
    var positionsWithColors = new List<string> (System.IO.File.ReadAllLines ("Assets/model.txt"));

    for (int index = 0; index < positionsWithColors.Count; index += 100)
    {
      var positionWithColor = positionsWithColors[index].Split (' ');

      var gameObject = new GameObject ();

      var image = gameObject.AddComponent<Image> ();
      image.transform.position = new Vector3
      {
        x = float.Parse (positionWithColor[0], CultureInfo.InvariantCulture),
        y = float.Parse (positionWithColor[1], CultureInfo.InvariantCulture),
        z = float.Parse (positionWithColor[2], CultureInfo.InvariantCulture)
      };
      image.color = new Color
      {
        r = uint.Parse (positionWithColor[3]),
        g = uint.Parse (positionWithColor[4]),
        b = uint.Parse (positionWithColor[5]),
        a = 1
      };
      var rect = new Rect (0.0f, 0.0f, size, size)
      {
        width = size,
        height = size
      };
      image.sprite = Sprite.Create (Texture2D.whiteTexture, rect, new Vector2 (0.5f, 0.5f));
      
      gameObject.transform.SetParent (canvas.gameObject.transform);
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

    for (int index = 0; index < canvas.gameObject.transform.childCount; index++)
    {
      var rect = canvas.gameObject.transform.GetChild (index).GetComponent<Image> ().sprite.rect;
      rect.width = size;
      rect.height = size;
    }
  }
}
