using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CodeChecker : MonoBehaviour {

    private List<int> numbers = new List<int>();
    private string currentCode = "____";
    private int maxTries = 3;
    private int attempts = 0;
    [SerializeField] private List<int> correctCode;
    [SerializeField] private int level;
	// Use this for initialization
	void Start () {

        for (int i = 0; i <= 9; i++)
        {
            numbers.Add(i);
        }
        this.gameObject.GetComponent<Text>().text = currentCode;
	}
	
	// Update is called once per frame
	void Update () {
        CheckForInput();
	}
    

    /// <summary>
    /// Checks the code input from the user and leads it to next level
    /// </summary>
    private void CheckForInput() {
        int result;
        if (int.TryParse(Input.inputString, out result)) {
            if (numbers.Contains(result)) {
                char[] charsToTrim = { '_' };
                currentCode = currentCode.Trim(charsToTrim);
                currentCode += Input.inputString;
                if (currentCode.Length >= 4) {
                    int codeInt;
                    int.TryParse(currentCode, out codeInt);
                    if (codeInt == correctCode[level]) {
                        this.gameObject.GetComponent<Text>().text = "YOU WIN";
                    } else {
                        attempts++;
                        currentCode = "____";
                        this.gameObject.GetComponent<Text>().text = currentCode;
                        if (attempts >= maxTries) {
                            SceneManager.LoadScene(1);
                        }
                    }

                } else {
                    currentCode += "____";
                    currentCode = currentCode.Substring(0, 3);
                    this.gameObject.GetComponent<Text>().text = currentCode;
                }
            }
        }
    }
}
