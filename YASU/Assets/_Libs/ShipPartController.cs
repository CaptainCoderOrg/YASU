using System;
using UnityEngine;

public class ShipPartController : MonoBehaviour
{
    internal void Collect(CharacterController2D characterController, LevelController levelController)
    {
        levelController.PartsFound++;
        Destroy(gameObject);
    }
}