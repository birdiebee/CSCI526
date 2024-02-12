using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int player_max_hp;

    [SerializeField]
    private int player_iq;

    private int player_curr_hp;

    [SerializeField]
    private GameObject player_hp_display;

    [SerializeField]
    private GameObject player_iq_display;

    private int iq_buff = 0;

    private GameObject enemy;
    private int enemy_iq;
    private int enemy_hp;
    private int enemy_max_hp;

    [SerializeField]
    private GameObject enemy_ui;

    [SerializeField]
    private GameObject enemy_hp_display;

    [SerializeField]
    private GameObject enemy_iq_display;

    // Start is called before the first frame update
    void Start()
    {
        player_max_hp = player.GetComponent<PlayerController>().GetPlayerMaxHP();
        player_curr_hp = player_max_hp;
        player_iq = player.GetComponent<PlayerController>().GetPlayerIQ();
        iq_buff = player.GetComponent<PlayerController>().GetPlayerBuff();

        player_hp_display.GetComponent<TMP_Text>().text = player_curr_hp + " / " + player_max_hp;
        player_iq_display.GetComponent<TMP_Text>().text = player_iq + "";

        // Hides enemy UI unless in combat
        enemy_ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHP()
    {
        player_curr_hp = player.GetComponent<PlayerController>().GetPlayerHP();
        player_hp_display.GetComponent<TMP_Text>().text = (player_curr_hp + " / " + player_max_hp);
    }

    public void UpdateIQ()
    {
        player_iq = player.GetComponent<PlayerController>().GetPlayerIQ();
        iq_buff = player.GetComponent<PlayerController>().GetPlayerBuff();
        if (iq_buff != 0)
        {
            player_iq_display.GetComponent<TMP_Text>().text = player_iq + " (+" + iq_buff + ")";
        }
        else
        {
            player_iq_display.GetComponent<TMP_Text>().text = player_iq + "";
        }
    }

    public int GetMaxHp()
    {
        return player_max_hp;
    }

    public void SetEnemy(GameObject new_enemy)
    {
        enemy = new_enemy;
        enemy_hp = enemy.GetComponent<EnemyController>().GetEnemyHP();
        enemy_max_hp = enemy.GetComponent<EnemyController>().GetEnemyMaxHP();
        enemy_iq = enemy.GetComponent<EnemyController>().GetEnemyIQ();
        enemy_hp_display.GetComponent<TMP_Text>().text = enemy_hp + " / " + enemy_max_hp;
        enemy_iq_display.GetComponent<TMP_Text>().text = enemy_iq + "";
        enemy_ui.SetActive(true);
    }

    public void UpdateEnemyHP()
    {
        enemy_hp = enemy.GetComponent<EnemyController>().GetEnemyHP();
        enemy_hp_display.GetComponent<TMP_Text>().text = (enemy_hp + " / " + enemy_max_hp);
    }

    public void HideEnemyUI()
    {
        enemy_ui.SetActive(false);
    }
}
