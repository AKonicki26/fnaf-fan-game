using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour {

    public static readonly float WALKING_SPEED = 3f;
    public static readonly float SNEAKING_SPEED = 1f;
    public static readonly float SPRINTING_SPEED = 6f;

    public static float speed;

    [SerializeField]
    private Slider StaminaBar;

    private float _maxStamina = 1000000000000000000000f;
    public float MaxStamina {
        get => _maxStamina;
        set {
            _maxStamina = value;
            StaminaBar.maxValue = value;
        }
    }

    public float minTurnAngle = -90f;
    public float maxTurnAngle = 90f;
    public float turnSpeed = 17.5f;
    public float rotX;
    private float _stamina;
    public float Stamina {
        get => _stamina;
        set {
            _stamina = value;
            StaminaBar.value = value;
        }
    }

    public float WalkingStaminaRecovery = 0.1f;
    public float SprintingStaminaLossage = 0.05f;
    public float SneakingStaminaRecovery = 0.15f;
    public bool inRecovery = false;

    public CharacterController characterController;
    public Camera PlayerCamera;

    List<string> list = new();

    public enum MovementModes {
        Walk,
        Sprint,
        Sneak,
        Recovery
    }
    public MovementModes currentMovement;

    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
        PlayerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        speed = WALKING_SPEED;

        PlayerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
    }

    // Update is called once per frame
    void Update() {
        if (!Player_Manager.IsAlive) return;
        CameraMovement();
        CharacterMovement();
    }
    void CameraMovement() {
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        // rotate the camera
        Vector3 Rotation = new(-rotX, transform.eulerAngles.y + y, 0);
        //PlayerCamera.transform.eulerAngles = Rotation;
        transform.eulerAngles = Rotation;
    }

    [Range(0,10)]
    public float CameraPanSpeed;
    private Vector3 zeroVector;
    void CharacterMovement() {
        if (inRecovery) currentMovement = MovementModes.Recovery;
        else if (Input.GetButton("Sprint") && (Stamina > 0f) && GetComponent<CharacterController>().velocity != Vector3.zero) currentMovement = MovementModes.Sprint;
        else if (Input.GetButton("Sneak")) currentMovement = MovementModes.Sneak;
        else currentMovement = MovementModes.Walk;

        switch (currentMovement) {
        case MovementModes.Walk:
            speed = WALKING_SPEED;
            Stamina += WalkingStaminaRecovery;
            clampStamina();
            PlayerCamera.transform.position = Vector3.SmoothDamp(PlayerCamera.transform.position, new Vector3(transform.position.x, transform.position.y + 0.65f, transform.position.z), ref zeroVector, CameraPanSpeed * Time.deltaTime);
            break;
        case MovementModes.Sprint:
            speed = SPRINTING_SPEED;
            Stamina -= SprintingStaminaLossage;
            clampStamina();
            PlayerCamera.transform.position = Vector3.SmoothDamp(PlayerCamera.transform.position, new Vector3(transform.position.x, transform.position.y + 0.65f, transform.position.z), ref zeroVector, CameraPanSpeed * Time.deltaTime);
            break;
        case MovementModes.Sneak:
            speed = SNEAKING_SPEED;
            Stamina += SneakingStaminaRecovery;
            clampStamina();
            PlayerCamera.transform.position = Vector3.SmoothDamp(PlayerCamera.transform.position, transform.position, ref zeroVector, CameraPanSpeed * Time.deltaTime);
            //PlayerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            break;
        case MovementModes.Recovery:
            speed = SNEAKING_SPEED;
            Stamina += WalkingStaminaRecovery;
            PlayerCamera.transform.position = Vector3.SmoothDamp(PlayerCamera.transform.position, new Vector3(transform.position.x, transform.position.y + 0.65f, transform.position.z), ref zeroVector, CameraPanSpeed * Time.deltaTime);
            
            if (Stamina > 100f) inRecovery = false;
            clampStamina();
            break;
        }

        if (Stamina <= 0f) {
            currentMovement = MovementModes.Recovery;
            inRecovery = true;
        }

            Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        characterController.Move(move * Time.deltaTime * speed);
    }

    private void clampStamina() {
        Stamina = Mathf.Clamp(Stamina, 0f, MaxStamina);
    }
}