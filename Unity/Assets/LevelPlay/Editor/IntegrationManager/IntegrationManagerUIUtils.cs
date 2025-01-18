using Unity.Services.LevelPlay.Editor;
using UnityEngine;

namespace Unity.Services.LevelPlay.Editor
{
    static class IntegrationManagerUIUtils
    {
        internal static Texture2D CreateTexture(int red, int green, int blue, float alpha)
        {
            var color = new Color(red / 255f, green / 255f, blue / 255f, alpha);
            return CreateTexture(color);
        }

        internal static Texture2D CreateTexture(Color color)
        {
            Color[] pix = new Color[1];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = color;

            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixels(pix);
            texture.Apply();

            return texture;
        }
    }
}
