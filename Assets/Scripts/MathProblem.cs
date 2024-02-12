using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MathProblem : MonoBehaviour
{
    private int num1 = 0;
    private int num2 = 0;
    private int answer = 0;
    private int problem_type = 0;

    [SerializeField]
    private TMP_InputField input;

    private string input_answer;

    [SerializeField]
    GameObject problem_display;

    [SerializeField]
    GameObject ui;

    private GameObject player;
    private GameObject enemy;

    private float timer;
    private bool solving = false;

    [SerializeField]
    private float addition_time = 5.0f;
    [SerializeField]
    private float subtraction_time = 5.0f;
    [SerializeField]
    private float multiplication_time = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = player.GetComponent<PlayerController>().GetEnemy();
        GenerateMathProblem();
    }

    // Update is called once per frame
    void Update()
    {
        if (solving)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                timer = 0.0f;
                solving = false;
                player.GetComponent<PlayerController>().TakeDamage();
                EndCombat();
            }
        }
    }

    public void GenerateMathProblem()
    {
        problem_type = Random.Range(0, 3);
        if (problem_type == 0)
        {
            // Addition
            num1 = Random.Range(13, 40);
            num2 = Random.Range(13, 40);
            answer = num1 + num2;
            problem_display.GetComponent<TMP_Text>().text = num1 + " + " + num2 + " =";
            timer = addition_time;
        }
        else if (problem_type == 1)
        {
            // Subtraction
            num1 = Random.Range(13, 40);
            num2 = Random.Range(13, 40);
            answer = num1 - num2;
            problem_display.GetComponent<TMP_Text>().text = num1 + " - " + num2 + " =";
            timer = subtraction_time;
        }
        else
        {
            // Multiplication
            num1 = Random.Range(3, 13);
            num2 = Random.Range(3, 13);
            answer = num1 * num2;
            problem_display.GetComponent<TMP_Text>().text = num1 + " x " + num2 + " =";
            timer = multiplication_time;
        }
        solving = true;
        input.Select();
    }

    public void ParseInput()
    {
        input_answer = input.text.ToString();
        solving = false;
        //input.ActivateInputField();
        if (int.Parse(input_answer) == answer)
        {
            input.text = "";
            player.GetComponent<PlayerController>().AttackEnemy();
            player.GetComponent<PlayerController>().GainIQ();
        }
        else
        {
            player.GetComponent<PlayerController>().TakeDamage();
            input.text = "";
        }
        EndCombat();
    }

    public void EndCombat()
    {
        player.GetComponent<PlayerController>().ResumeMovement();
        GameObject.Find("UI").GetComponent<UIDisplay>().HideEnemyUI();
        Destroy(gameObject);
    }
}
