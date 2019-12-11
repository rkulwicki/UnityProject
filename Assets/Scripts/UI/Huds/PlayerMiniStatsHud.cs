//Name: PlayerMiniStatsHud
//Desc: Small upper bar that displays hp, fp, exp, and coins. Non-intrusive and for battle
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMiniStatsHud : MonoBehaviour
{
    public Text playerHPText;

    public Text playerFPText;

    public Text playerCoinsText;
    public Text playerExperienceText;

    private GameObject player;
    private PlayerStats playerStats;
    //continue: https://www.youtube.com/watch?v=_1pz_ohupPs
    //ideas: https://tcrf.net/Paper_Mario

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        SetMiniPlayerHud(playerStats);
    }

    private void Update()
    {
        SetMiniPlayerHud(playerStats);
    }

    public void SetMiniPlayerHud(PlayerStats playerStats)
    {
        playerHPText.text = "HP " + playerStats.currentHP.ToString() + "/" +playerStats.maxHP.ToString();

        playerFPText.text = "FP " + playerStats.currentFP.ToString() + "/" + playerStats.maxFP.ToString();

        playerExperienceText.text = "EXP x " + playerStats.playerExperience;

        playerCoinsText.text = "COINS x " + playerStats.playerCoins;

    }
}
