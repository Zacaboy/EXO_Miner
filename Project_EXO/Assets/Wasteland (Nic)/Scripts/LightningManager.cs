using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
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

    public float charge;
    float lastChargePlayerTime;
    float lastRandomChargeTime;

    // Start is called before the first frame update
    void Start()
    {
        me = this;
        player = FindObjectOfType<PlayerMechController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastRandomChargeTime + randomChargeRate)
        {
            lastRandomChargeTime = Time.time + Random.Range(-2, 2);
            SpawnLightning(player.transform.position + new Vector3(Random.Range(-lightningRange, lightningRange), 0, Random.Range(-lightningRange, lightningRange)));
        }
        if (player.rigi.velocity.magnitude > attractionSpeed & !player.lightningStruckFall)
        {
            if (Time.time > lastChargePlayerTime + chargeRate)
            {
                lastChargePlayerTime = Time.time;
                charge += 1;
            }
        }
        else
        {
            if (Time.time > lastChargePlayerTime + chargeRate & !player.lightningStruckFall)
            {
                lastChargePlayerTime = Time.time;
                charge -= 1;
            }
        }
        charge = Mathf.Clamp(charge, 0, charges[charges.Length - 1]);
        player.warningLightning = charge >= charges[0];
        if (!player.dangerLightning)
            player.dangerLightning = charge >= charges[charges.Length - 1] - 1;
        if (charge >= charges[charges.Length - 1] & !player.lightningStruckFall)
        {
            charge = 0;
            lastChargePlayerTime = Time.time;
            SpawnLightning(player.lightningPos.position);
        }
    }

    public void SpawnLightning(Vector3 pos)
    {
        LightningBoltScript lightning = Instantiate(lightningPref);
        lightning.transform.position = pos;
    }
}
