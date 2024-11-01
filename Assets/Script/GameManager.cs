using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Image imgYou;       // your selected image (rock, paper, scissors)
    Image imgCom;       // computer selected image (rock, paper, scissors)

    public TextMeshProUGUI txtYou;        // the score you win
    public TextMeshProUGUI txtCom;        // the score computer wins
    public TextMeshProUGUI txtResult;     // the result

    int cntYou = 0;     // number of times you win
    int cntCom = 0;     // number of times computer wins

    void CheckResult(int yourResult)
    {
        // 1 = Scissors, 2 = Rock, 3 = Paper
        int comResult = UnityEngine.Random.Range(1, 4);
        int k = yourResult - comResult;
        Debug.Log(yourResult + ", " + comResult);

        if (k == 0)
        {
            txtResult.text = "Draw.";
        }
        else if ((yourResult == 1 && comResult == 3) || // Scissors beats Paper
                 (yourResult == 2 && comResult == 1) || // Rock beats Scissors
                 (yourResult == 3 && comResult == 2))   // Paper beats Rock
        {
            cntYou++;
            txtResult.text = "You win.";
        }
        else
        {
            cntCom++;
            txtResult.text = "Computer wins.";
        }

        SetResult(yourResult, comResult); // update the UI with game result

        // Check if either player has reached 3 points
        if (cntYou == 3)
        {
            Won();
        }
        else if (cntCom == 3)
        {
            Lose();
        }
        else if (cntYou == 3 && cntCom == 3) // handle Draw case if both players reach 3 at the same time
        {
            Draw();
        }
    }

    void SetResult(int you, int com)
    {
        // update images based on choices
        imgCom.sprite = Resources.Load<Sprite>("img_" + com);
        imgYou.sprite = Resources.Load<Sprite>("img_" + you);

        // invert computer's image on the x-axis
        imgCom.transform.localScale = new Vector3(-1, 1, 1);

        // update score display
        txtYou.text = cntYou.ToString();
        txtCom.text = cntCom.ToString();

        // play result text animation if available
        // txtResult.GetComponent<Animator>().Play("TextScale", -1, 0);
    }

    public void OnButtonClick(GameObject buttonObject)
    {
        // Extract the first character from the button's name and try to parse it as an integer.
        if (buttonObject != null && !string.IsNullOrEmpty(buttonObject.name))
        {
            int you;
            string firstChar = buttonObject.name.Substring(0, 1);
            if (int.TryParse(firstChar, out you))
            {
                CheckResult(you);
                Debug.Log("Button clicked: " + buttonObject.name + ", Parsed number: " + you);
            }
            else
            {
                Debug.LogError("Failed to parse button name. The first character is not a number: " + buttonObject.name);
            }
        }
        else
        {
            Debug.LogError("Button object or its name is null/empty.");
        }
    }

    public void OnMouseExit(GameObject buttonObject)
    {
        // event when mouse exits the button
        Animator anim = buttonObject.GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("Normal");
        }
    }

    private void InitGame()
    {
        imgYou = GameObject.Find("ImgYou")?.GetComponent<Image>();
        imgCom = GameObject.Find("ImgCom")?.GetComponent<Image>();

        txtYou = GameObject.Find("TxtYou")?.GetComponent<TextMeshProUGUI>();
        txtCom = GameObject.Find("TxtCom")?.GetComponent<TextMeshProUGUI>();
        txtResult = GameObject.Find("TxtResult_")?.GetComponent<TextMeshProUGUI>();

        if (imgYou == null || imgCom == null || txtYou == null || txtCom == null || txtResult == null)
        {
            Debug.LogError("One or more UI elements are not found or assigned correctly.");
            return;
        }

        // initialize the result text before the game starts
        txtResult.text = "Let's Play!";
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize the game
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        // exit the game if the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Won()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Draw()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
}
