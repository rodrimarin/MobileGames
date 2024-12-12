using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class WeaponSwitching : MonoBehaviour
{
    InputAction switching; // Acción de entrada para cambiar de arma
    public int selectedWeapon = 0; // Arma seleccionada actualmente
    public TextMeshProUGUI ammoInfoText; // Texto que muestra la información de munición

    private Gun currentGun; // Referencia al arma actual

    // Se ejecuta al inicio para configurar la acción de entrada
    void Start()
    {
        // Configura el InputAction para el desplazamiento del mouse y el D-pad del gamepad
        switching = new InputAction("Switch", binding: "<Mouse>/scroll/y");
        switching.AddBinding("<Gamepad>/dpad/y");
        switching.Enable(); // Habilita la acción

        SelectWeapon(); // Selecciona el arma inicialmente
    }

    // Se ejecuta en cada frame
    void Update()
    {
        // Actualiza la información de munición
        currentGun = GetCurrentGun();
        ammoInfoText.text = currentGun.currentAmmo + " / " + currentGun.magazineAmmo;

        // Detecta el desplazamiento del mouse o del gamepad
        float scrollValue = switching.ReadValue<float>();
        Debug.Log("Scroll Value: " + scrollValue); // Debugging Scroll Value
        int previousSelected = selectedWeapon;

        // Cambia el arma hacia adelante o hacia atrás según el desplazamiento
        if (scrollValue > 0f)
        {
            selectedWeapon++;
            if (selectedWeapon >= transform.childCount)
                selectedWeapon = 0;
        }
        else if (scrollValue < 0f)
        {
            selectedWeapon--;
            if (selectedWeapon < 0)
                selectedWeapon = transform.childCount - 1;
        }

        // Si se ha seleccionado un arma diferente, cambia el arma
        if (previousSelected != selectedWeapon)
        {
            Debug.Log("Weapon Changed: " + selectedWeapon); // Debugging Weapon Change
            SelectWeapon();
        }
    }

    // Obtiene el arma actualmente seleccionada
    private Gun GetCurrentGun()
    {
        Transform selectedWeaponTransform = transform.GetChild(selectedWeapon);
        return selectedWeaponTransform.GetComponent<Gun>();
    }

    // Selecciona el arma activando solo la seleccionada
    private void SelectWeapon()
    {
        // Desactiva todas las armas y activa solo la seleccionada
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == selectedWeapon);
        }

        // Actualiza la referencia al arma seleccionada
        currentGun = GetCurrentGun();
    }
}
