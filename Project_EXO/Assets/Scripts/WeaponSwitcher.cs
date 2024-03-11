using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons; // Array of weapons to switch between
    private int currentWeaponIndex = 0; // Index of the currently active weapon

    public delegate void WeaponSwitchedHandler(int newIndex);
    public event WeaponSwitchedHandler OnWeaponSwitched; // Event to notify when weapon is switched

    public InputActionReference[] weaponSwitchActions; // Reference to the input actions for weapon switching

    void Start()
    {
        // Ensure only the first weapon is active at start
        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Check for input to switch weapons
        for (int i = 0; i < weaponSwitchActions.Length; i++)
        {
            if (weaponSwitchActions[i].action.triggered)
            {
                SwitchWeapon(i);
                break;
            }
        }
    }

    void SwitchWeapon(int newIndex)
    {
        // Deactivate all weapons
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        // Activate the selected weapon
        if (newIndex >= 0 && newIndex < weapons.Length)
        {
            weapons[newIndex].SetActive(true);
            currentWeaponIndex = newIndex;

            // Invoke the event to notify that weapon has been switched
            OnWeaponSwitched?.Invoke(newIndex);
        }
    }
}