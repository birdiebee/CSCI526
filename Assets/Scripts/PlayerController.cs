using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Tile movement
    [SerializeField]
    private float move_speed = 0.1f;
    private float move_timer = 0.0f;
    [SerializeField]
    private float tile_size = 1f;
    private bool is_moving = false;
    private bool hit_wall = false;
    private Vector2 curr_pos;
    private Vector2 next_pos;
    // Save previous position so you can go back if hitting walls or enemies
    private Vector2 prev_pos;

    // Player stats
    [SerializeField]
    private int player_iq;
    [SerializeField]
    private int player_hp;
    [SerializeField]
    private int player_buff;
    [SerializeField]
    private int player_max_hp;
    private bool in_combat = false;

    [SerializeField]
    private GameObject ui;

    private Rigidbody2D rigidBody;

    private Vector2 velocity;

    [SerializeField]
    private int attack_power;

    private GameObject curr_enemy;

    // Combat
    [SerializeField]
    private GameObject mathContainer;
    [SerializeField]
    private GameObject mathCombatPrefab;

    private void Start()
    {
        player_hp = player_max_hp;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (!in_combat && !is_moving)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                is_moving = true;
                curr_pos = gameObject.transform.position;
                prev_pos = curr_pos;
                next_pos = curr_pos + (Vector2.right * tile_size);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                is_moving = true;
                curr_pos = gameObject.transform.position;
                prev_pos = curr_pos;
                next_pos = curr_pos + (Vector2.left * tile_size);
            }

            else if (Input.GetKeyDown(KeyCode.W))
            {
                is_moving = true;
                curr_pos = gameObject.transform.position;
                prev_pos = curr_pos;
                next_pos = curr_pos + (Vector2.up * tile_size);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                is_moving = true;
                curr_pos = gameObject.transform.position;
                prev_pos = curr_pos;
                next_pos = curr_pos + (Vector2.down * tile_size);
            }
        }
        else if(is_moving)
        {
            if (move_timer >= move_speed)
            {
                if (hit_wall) hit_wall = false;
                is_moving = false;
                move_timer = 0.0f;
                prev_pos = curr_pos;
                gameObject.transform.position = next_pos;
            }
            else
            {
                move_timer += Time.deltaTime;
                float lerp_amt = move_timer / move_speed;
                gameObject.transform.position = Vector2.Lerp(curr_pos, next_pos, lerp_amt);
            }
        }
    }

    public int GetPlayerHP()
    {
        return player_hp;
    }

    public int GetPlayerMaxHP()
    {
        return player_max_hp;
    }

    public int GetPlayerIQ()
    {
        return player_iq;
    }

    public int GetPlayerBuff()
    {
        return player_buff;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(!in_combat)
            {
                ui.GetComponent<UIDisplay>().SetEnemy(collision.gameObject);
                curr_enemy = collision.gameObject;
                in_combat = true;
                is_moving = true;
                curr_pos = gameObject.transform.position;
                next_pos = prev_pos;
                move_timer = 0.0f;
                Instantiate(mathCombatPrefab, mathContainer.transform);
            }
        }
        if(collision.gameObject.tag == "Unwalkable" && !hit_wall)
        {
            hit_wall = true;
            is_moving = true;
            curr_pos = gameObject.transform.position;
            next_pos = prev_pos;
            move_timer = 0.0f;
        }
        if(collision.gameObject.tag == "Buff" && collision.gameObject != null)
        {
            if(!collision.gameObject.GetComponent<Buff>().GetCollectionStatus())
            {
                collision.gameObject.GetComponent<Buff>().CollectBuff();
                player_buff += collision.gameObject.GetComponent<Buff>().GetBuffAmount();
                ui.GetComponent<UIDisplay>().UpdateIQ();
                Destroy(collision.gameObject);
            }
        }
        if(collision.gameObject.tag == "Exit")
        {
            if(player_iq + player_buff < 150)
            {
                in_combat = true;
                collision.gameObject.GetComponent<Exit>().DumbEnding();
            }
            else
            {
                in_combat = true;
                collision.gameObject.GetComponent<Exit>().SmartEnding();
            }
        }
    }

    public void AttackEnemy()
    {
        float iq_modifier = (float)player_iq / 100.0f;
        float iq_buff_modifier = player_buff / 10.0f;
        float total_damage = (float)attack_power * (iq_modifier + iq_buff_modifier);
        int damage_dealt = (int)total_damage;
        curr_enemy.GetComponent<EnemyController>().TakeDamage(damage_dealt);
    }

    public void ResumeMovement()
    {
        in_combat = false;
    }

    public void TakeDamage()
    {
        float amount = (float)curr_enemy.GetComponent<EnemyController>().GetEnemyIQ();
        amount -= player_iq;
        amount = ((float)amount / 10.0f) * 5.0f;
        if ((int)amount <= 0) amount = 1;
        player_hp -= (int)amount;
        float iq_amount = (float)curr_enemy.GetComponent<EnemyController>().GetEnemyIQ();
        iq_amount /= 15.0f;
        if (iq_amount < 1) iq_amount = 1;
        player_iq -= (int)iq_amount;
        if (player_hp < 0) player_hp = 0;
        ui.GetComponent<UIDisplay>().UpdateHP();
        ui.GetComponent<UIDisplay>().UpdateIQ();
        if(player_hp == 0)
        {
            in_combat = true;
            GameObject.Find("Exit").GetComponent<Exit>().PhysicalDeath();
        }
        else if(player_iq + player_buff <= 0)
        {
            in_combat = true;
            GameObject.Find("Exit").GetComponent<Exit>().DumbDeath();
        }
        // Game Over
    }
    public void GainIQ()
    {
        float amount = (float)curr_enemy.GetComponent<EnemyController>().GetEnemyIQ();
        amount -= player_iq;
        amount = ((float)amount / 10.0f) * 3.0f;
        if ((int)amount <= 0) amount = 1;
        player_iq += (int)amount;
        ui.GetComponent<UIDisplay>().UpdateIQ();
    }

    public GameObject GetEnemy()
    {
        return curr_enemy;
    }
}
