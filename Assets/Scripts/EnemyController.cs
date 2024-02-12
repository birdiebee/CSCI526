using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int enemy_hp;

    [SerializeField]
    private int enemy_max_hp;

    [SerializeField]
    private int enemy_iq;

    // Start is called before the first frame update
    void Start()
    {
        enemy_hp = enemy_max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetEnemyMaxHP()
    {
        return enemy_max_hp;
    }

    public int GetEnemyHP()
    {
        return enemy_hp;
    }

    public int GetEnemyIQ()
    {
        return enemy_iq;
    }

    public void TakeDamage(int damage)
    {
        enemy_hp -= damage;
        if(enemy_hp < 0)
        {
            enemy_hp = 0;
        }
        GameObject.Find("UI").GetComponent<UIDisplay>().UpdateEnemyHP();
        if(enemy_hp == 0)
        {
            Destroy(gameObject);
            GameObject.Find("UI").GetComponent<UIDisplay>().HideEnemyUI();
        }
    }
}
