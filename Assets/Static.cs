using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Static : MonoBehaviour {

    [SerializeField] private int pixWidth;
    [SerializeField] private int pixHeight;

    [SerializeField] private float xOrg;
    [SerializeField] private float yOrg;


    [SerializeField] private float scale = 1.0f;
    [SerializeField] private float scaleMin = 100.0f;
    [SerializeField] private float scaleMax = 500.0f;

    private Texture2D noiseTex;
    private Color[] pix;
    private Image img;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();

        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        img.material.mainTexture = noiseTex;
	}
	
    void CalcNoise()
    {
        // For each pixel in the texture...
        float y = 0.0F;

        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }


    // Update is called once per frame
    void Update () {
        scale = Random.Range(scaleMin, scaleMax);
        CalcNoise();
	}
}
