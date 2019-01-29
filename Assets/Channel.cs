using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Channel : MonoBehaviour {

    public Vector2 rightPos;
    public Vector2 leftPos;
    public Image img;
    public VideoPlayer vidp;
    public AudioClip aud;

    [SerializeField] private bool video;
    private AntennaeManager antm;
    private float diffMod = 1.0f;

    private GameObject stat;


    // Use this for initialization
    void Start () {

        stat = GameObject.Find("Static");

        if (this.GetComponent<Image>() != null)
        {
            img = this.GetComponent<Image>();
        }
        if(this.GetComponent<VideoPlayer>() != null)
        {
            vidp = this.GetComponent<VideoPlayer>();
        }
        antm = GameObject.Find("AntennaeManager").GetComponent<AntennaeManager>();
	}

    private void OnEnable()
    {
        AntennaeManager.OnChange += calculateOpacity;
    }

    private void OnDisable()
    {
        AntennaeManager.OnChange -= calculateOpacity;
    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// Calculates the opacity for each images in the level depending upon the anetena position
    /// </summary>
    void calculateOpacity()
    {
        Vector2 antLeft = antm.GetLeftPos();
        Vector2 antRight = antm.GetRightPos();

        Vector2 RDiff = rightPos - antRight;
        Vector2 LDiff = leftPos - antLeft;

        float totalRDiff = Mathf.Abs(RDiff.x) + Mathf.Abs(RDiff.y);
        float totalLDiff = Mathf.Abs(LDiff.x) + Mathf.Abs(LDiff.y);
        float diffPercent = 0;

        Debug.Log("Total R Diff: " + totalRDiff);
        Debug.Log("Total L Diff: " + totalLDiff);

        if (totalRDiff <= 1.0f)
        {
            diffPercent += (1.0f - totalRDiff) * 0.5f;
        }
        if(totalLDiff <= 1.0f)
        {
            diffPercent += (1.0f - totalLDiff) * 0.5f;
        }

        if(diffPercent > 0.0f)
        {
            diffPercent += 0.25f;
            diffPercent = Mathf.Clamp(diffPercent, 0, 1.0f);
        }

        if (video)
        {
            vidp.targetCameraAlpha = diffPercent;
            if (vidp.GetDirectAudioMute(0))
            {
                vidp.SetDirectAudioVolume(0, diffPercent);
            }
            else
            {
                vidp.GetComponent<AudioSource>().volume = diffPercent;
            }
        }
        else
        {
            Color newCol = img.color;
            newCol.a = diffPercent;
            img.color = newCol;
        }

        // reduces noise to make image visible
        if( diffPercent < 1.0f)
        {
            stat.GetComponent<AudioSource>().volume = 1.0f - diffPercent;
        }

    }
}
