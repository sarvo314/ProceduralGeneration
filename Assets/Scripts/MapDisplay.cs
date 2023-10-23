using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;

    public void DrawNoiseMap(float[,] noise_map)
    {
        int width = noise_map.GetLength(0);
        int height = noise_map.GetLength(1);

        Texture2D texture = new Texture2D(width, height);
        Color[] colourMap = new Color[width * height];

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noise_map[x, y]);
            }
        }

        texture.SetPixels(colourMap);

        texture.Apply();
        //as we can't edit the main material we are editing the shared material

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 0.1f, height);
    }
}
