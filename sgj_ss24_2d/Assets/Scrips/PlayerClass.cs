using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerClass : MonoBehaviour
{
    [SerializeField] private InputActionReference inputActionReference;
    [SerializeField] private BlobController blobController;
    
    private void Start()
    {
        inputActionReference.action.performed += ActionOnperformed;
    }
    
    private void ActionOnperformed(InputAction.CallbackContext obj)
    {
       Vector2 input = obj.ReadValue<Vector2>();
       if(input.y >= 0)
           blobController.SetUp(input.y);
       if(input.y < 0)
           blobController.SetDown(input.y);
       if(input.x >= 0)
           blobController.SetRight(input.x);
       if(input.y > 0)
           blobController.SetLeft(input.x);
    }
}
