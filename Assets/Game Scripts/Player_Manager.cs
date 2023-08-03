using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player_Manager : MonoBehaviour
{
    public static bool IsAlive = true;

    public bool FlashlightInUse = false;
    [Range(1f, 25f)]
    public float FlashlightMaxDistance;
    public Vector3 FlashlightEndPosition;

    private RaycastHit hit;
    private Ray VisionRay;

    LayerMask InteractableLayerMask;
    [Range(0f, 5f)]
    public float InteractableDistance;
    public bool SomethingToInteractWith;
    private Interactable _interactable;
    public Interactable Interactable {
        get => _interactable;
        set {
            _interactable = value;
            InteractText.enabled = !(_interactable == null);
        }
    }

    [SerializeField]
    private TextMeshProUGUI InteractText;

    private float deadTimer = 0f;
    private void Start() {
        InteractableLayerMask = LayerMask.GetMask("Interactable");
        IsAlive = true;
    }

    private void FixedUpdate() {
        VisionRay = new Ray(origin: GetComponentInChildren<Camera>().transform.position, direction: GetComponentInChildren<Camera>().transform.forward);
        FlashlightEndPosition = FlashlightInUse ? (FlashlightEndPosition = Physics.Raycast(VisionRay, out hit, maxDistance: FlashlightMaxDistance) ? VisionRay.GetPoint(hit.distance) : VisionRay.GetPoint(FlashlightMaxDistance)) : Vector3.negativeInfinity;
        SomethingToInteractWith = Physics.Raycast(VisionRay, out hit, maxDistance: InteractableDistance, InteractableLayerMask);
    }

    public void Update() {
        if (!IsAlive) {
            deadTimer += Time.deltaTime;
            if (deadTimer >= 5f) {
                SceneManager.LoadSceneAsync("Main Menu");
                SceneManager.UnloadSceneAsync("Main Game Scene");
            }
                
            return;
        }
        Interactable = SomethingToInteractWith ? hit.transform.gameObject.GetComponent<Interactable>() : null;
        InputHandling();
    }

    private void InputHandling() {
        if (Input.GetButtonDown("Flashlight"))
            FlashlightInUse = !FlashlightInUse;
        if (Input.GetButton("Interact") && Interactable != null)
            Interactable.OnInteraction();
        if (Input.GetButtonUp("Interact") && Interactable != null)
            Interactable.OnInteractionExit();
    }

}
