using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {


        /*
        if(Mouse.current != null)
        {
            mouseX = Mouse.current.delta.ReadValue().x;
            mouseY = Mouse.current.delta.ReadValue().y;
        }

        if (Gamepad.current != null)
        {
            mouseX = Gamepad.current.rightStick.ReadValue().x;
            mouseY = Gamepad.current.rightStick.ReadValue().y;
        }

        //float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        */
    }

public class CameraController : MonoBehaviour
    {
        public Transform playerBody; // El cuerpo del jugador para rotaci�n horizontal
        public float mouseSensitivity = 100f; // Sensibilidad del mouse

        private float xRotation = 0f; // Rotaci�n vertical

        void Update()
        {
            // Verificar que haya al menos un toque en la pantalla
            if (Touchscreen.current.touches.Count == 0)
                return;

            Vector2 touchDeltaPosition = Vector2.zero;

            // Verificar si el primer toque est� en progreso y no est� sobre la interfaz
            if (Touchscreen.current.touches.Count > 0 && Touchscreen.current.touches[0].isInProgress)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[0].touchId.ReadValue()))
                {
                    touchDeltaPosition = Touchscreen.current.touches[0].delta.ReadValue();
                }
            }

            // Si hay m�s de un toque, usar el segundo toque para la rotaci�n (opcional)
            if (Touchscreen.current.touches.Count > 1 && Touchscreen.current.touches[1].isInProgress)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[1].touchId.ReadValue()))
                {
                    touchDeltaPosition = Touchscreen.current.touches[1].delta.ReadValue();
                }
            }

            // Aplicar sensibilidad
            float mouseX = touchDeltaPosition.x * mouseSensitivity * Time.deltaTime;
            float mouseY = touchDeltaPosition.y * mouseSensitivity * Time.deltaTime;

            // Ajustar rotaci�n vertical
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            // Aplicar rotaci�n vertical
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Aplicar rotaci�n horizontal al cuerpo del jugador
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}