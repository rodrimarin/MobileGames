using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class WeaponSwitching : MonoBehaviour
{
    InputAction switching;
    public int selectedWeapon = 0;
    public TextMeshProUGUI ammoInfoText;

    void Start()
    {
        switching = new InputAction("Scroll", binding: "<Mouse>/scroll");
        switching.AddBinding("<Gamepad>/Dpad");
        switching.Enable();

        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        Gun gun = FindObjectOfType<Gun>();
        ammoInfoText.text = gun.currentAmmo + " / " + gun.magazineAmmo;
        
        float scrollValue = switching.ReadValue<Vector2>().y;

        int previousSelected = selectedWeapon;
        
     
        
        if (CrossPlatformInputManager.GetButtonDown("Switch"))
        {
            selectedWeapon++;
            if (selectedWeapon == transform.childCount)
                selectedWeapon = 0;
        }

        if(previousSelected != selectedWeapon)
            SelectWeapon();

    }

    private void SelectWeapon()
    {
        foreach (Transform weapon in transform)
        {    
            if(ScoreManager.scoreCount == 500)
            weapon.gameObject.SetActive(false);
        }
        transform.GetChild(selectedWeapon).gameObject.SetActive(true);
    }
}
