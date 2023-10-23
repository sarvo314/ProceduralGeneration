using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int map_width;
    public int map_height;
    public float noise_scale;
    public int octaves;
    public float lacunarity;
    [Range(0, 1)]
    public float persistence;
    public int seed;
    public Vector2 offset;
    public bool auto_update;

    public void GenerateMap()
    {
        float[,] noise_map = Noise.GenerateNoiseMap(map_width, map_height, seed, noise_scale, octaves, persistence, lacunarity, offset);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noise_map);
    }

    private void OnValidate()
    {
        if (map_width < 1)
        {
            map_width = 1;
        }
        if (map_height < 1)
        {
            map_height = 1;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }
    }

}
