using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntennaeManager : MonoBehaviour {

    [SerializeField] private Vector2 leftCurrentPos = new Vector2(0, 0);
    [SerializeField] private Vector2 rightCurrentPos = new Vector2(0, 0);
    [SerializeField] private float posModifier = 10.0f;
    [SerializeField] private float movementModifier;

    public Text rightText, leftText;


    public delegate void AntennaChange();
    public static event AntennaChange OnChange;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxisRaw("LeftX") != 0 || Input.GetAxisRaw("LeftY") != 0)
        {
            /* Vector2 posInc = new Vector2(Input.GetAxisRaw("LeftX"), Input.GetAxis("LeftY"));
             posInc *= movementModifier;
             leftCurrentPos += posInc;
             leftCurrentPos.x = Mathf.Clamp(leftCurrentPos.x, minPos.x, maxPos.x);
             leftCurrentPos.y = Mathf.Clamp(leftCurrentPos.y, minPos.y, maxPos.y);
             leftText.text = "Left Pos: " + leftCurrentPos.ToString();


     */
            Vector2 pos = new Vector2(Input.GetAxisRaw("LeftX"), Input.GetAxisRaw("LeftY"));
            leftCurrentPos = pos * posModifier;
            leftText.text = "Left Pos: " + leftCurrentPos.ToString();
            if (OnChange != null)
            {
                OnChange();
            }

        }
        else
        {
            leftCurrentPos = new Vector2(0, 0);
            OnChange();
        }
        if (Input.GetAxisRaw("RightX") != 0 || Input.GetAxisRaw("RightY") != 0)
        {
            /* Vector2 posInc = new Vector2(Input.GetAxisRaw("RightX"), Input.GetAxis("RightY"));
             posInc *= movementModifier;
             rightCurrentPos += posInc;
             rightCurrentPos.x = Mathf.Clamp(rightCurrentPos.x, minPos.x, maxPos.x);
             rightCurrentPos.y = Mathf.Clamp(rightCurrentPos.y, minPos.y, maxPos.x);
             rightText.text = "Right Pos: " + rightCurrentPos.ToString();*/

            Vector2 pos = new Vector2(Input.GetAxisRaw("RightX"), Input.GetAxisRaw("RightY"));
            rightCurrentPos = pos * posModifier;
            rightText.text = "Right Pos: " + rightCurrentPos.ToString(); 
            if (OnChange != null)
            {
                OnChange();
            }
        }
        else
        {
            rightCurrentPos = new Vector2(0, 0);
            OnChange();
        }
    }

    public Vector2 GetRightPos()
    {
        return rightCurrentPos;
    }

    public Vector2 GetLeftPos()
    {
        return leftCurrentPos;
    }
}
