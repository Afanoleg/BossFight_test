using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    GameObject player;

    public int maxHealth = 100;

    public Slider hpPlayerBar;

    private int currentPlayerHealth;

    private void UpdatePlayerHPBar()
    {
        hpPlayerBar.value = currentPlayerHealth;
    }

    private void Awake()
    {
        currentPlayerHealth = maxHealth;
        UpdatePlayerHPBar();
    }


    public void PlayerTakingDamage()
    {
        Grenade grenade = new Grenade();
        Boss boss = new Boss();

        currentPlayerHealth = Mathf.Max(0, currentPlayerHealth - grenade.grenadeDamage);
        currentPlayerHealth = Mathf.Max(0, currentPlayerHealth - boss.handAttackDamage);

        UpdatePlayerHPBar();
        if (currentPlayerHealth <= 0)
        {
            GameOver();
        }
        if (currentPlayerHealth > 0)
            return;
    }

    public void GameOver()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void TakingDamage2(int damage)
    {
        currentPlayerHealth -= damage;
        UpdatePlayerHPBar();
    }
}
