using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    // Variables
    public static LightningManager me;
    public LightningBoltScript lightningPref;
    PlayerMechController player;

    [Header("Attractive Lightning")]
    public int[] charges;
    public float chargeRate = 0.1f;
    public float attractionSpeed = 5;

    [Header("Random Lightning")]
    public float randomChargeRate = 4;
    public float lightningRange = 200;

    // Ignore these
    public float charge;
    float lastChargePlayerTime;
    float lastRandomChargeTime;

    // Start is called before the first frame update
    void Start()
    {
        // Sets variables on start
        me = this;
        player = FindObjectOfType<PlayerMechController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spawns lightning randomly when the time is right
        if (Time.time > lastRandomChargeTime + randomChargeRate)
        {
            lastRandomChargeTime = Time.time + Random.Range(-2, 2);
            SpawnLightning(player.transform.position + new Vector3(Random.Range(-lightningRange, lightningRange), 0, Random.Range(-lightningRange, lightningRange)));
        }

        // Checks if the player is moving too fast
        if (player.rigi.velocity.magnitude > attractionSpeed & !player.lightningStruckFall)
        {
            // if the player is moving too fast then, the charge will increase
            if (Time.time > lastChargePlayerTime + chargeRate)
            {
                lastChargePlayerTime = Time.time;
                charge += 1;
            }
        }
        else
        {
            // if the player is moving slowly then, the charge will decrease
            if (Time.time > lastChargePlayerTime + chargeRate & !player.lightningStruckFall)
            {
                lastChargePlayerTime = Time.time;
                charge -= 1;
            }
        }
        charge = Mathf.Clamp(charge, 0, charges[charges.Length - 1]);

        // Sets the player's lights in the cockpit based on the severity of the lightning
        player.warningLightning = charge >= charges[0];
        if (!player.dangerLightning)
            player.dangerLightning = charge >= charges[charges.Length - 1] - 1;
        
        // Checks if charge is high enough, and then spawns lightning on the player
        if (charge >= charges[charges.Length - 1] & !player.lightningStruckFall)
        {
            charge = 0;
            lastChargePlayerTime = Time.time;
            SpawnLightning(player.lightningPos.position);
        }
    }

    // Spawns lightning at the stated pos
    public void SpawnLightning(Vector3 pos)
    {
        LightningBoltScript lightning = Instantiate(lightningPref);
        lightning.transform.position = pos;
    }
}
