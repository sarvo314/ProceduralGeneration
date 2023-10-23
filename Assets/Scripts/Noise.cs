using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int map_width, int map_height, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        float[,] noise_map = new float[map_width, map_height];
        System.Random prng = new System.Random(seed);
        Vector2[] octave_offsets = new Vector2[octaves];

        float half_width = map_width / 2f;
        float half_height = map_height / 2f;


        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octave_offsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 1.1f;
        }
        float max_noise_height = float.MinValue;
        float min_noise_height = float.MaxValue;

        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                float amplitude = 1f;
                float frequency = 1f;
                float noise_height = 0f;

                for (int i = 0; i < octaves; i++)
                {
                    float sample_x = ((x - half_width) / scale) * frequency + octave_offsets[i].x;
                    float sample_y = ((y - half_height) / scale) * frequency + octave_offsets[i].y;

                    //multiplied by 2 and subtracting 1 to get range from [-1, 1]
                    float perline_value = Mathf.PerlinNoise(sample_x, sample_y) * 2 - 1;

                    noise_height += perline_value * amplitude;
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                if (noise_height > max_noise_height)
                {
                    max_noise_height = noise_height;
                }
                else if (noise_height < min_noise_height)
                {
                    min_noise_height = noise_height;
                }

                noise_map[x, y] = noise_height;
            }
        }
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                //inverse lerp normalises the value
                noise_map[x, y] = Mathf.InverseLerp(min_noise_height, max_noise_height, noise_map[x, y]);
            }
        }
        return noise_map;
    }
}
