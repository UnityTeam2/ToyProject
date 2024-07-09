using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class XRController : MonoBehaviour
{
    public ActionBasedController targetCont;
    
    private Gun gun;
    private InputActionReference activateRef;
    private void Start()
    {
        gun = GetComponentInChildren<Gun>();
        activateRef = targetCont.activateAction.reference;
        activateRef.action.performed += delegate (InputAction.CallbackContext context)
        {
            gun.StartFiring();
        };
        activateRef.action.canceled += delegate (InputAction.CallbackContext context)
        {
            gun.StopFiring();
        };
    }
   
}