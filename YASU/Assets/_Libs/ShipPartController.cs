using System;
using UnityEngine;

public class ShipPartController : MonoBehaviour
{
    private bool _collected = false;
    [SerializeField] private SelfDestructSFX _collectSound;
    internal void Collect(CharacterController2D characterController, LevelController levelController)
    {
        if (_collected) { return; }
        _collected = true;
        levelController.PartsFound++;
        Destroy(gameObject);
        Instantiate(_collectSound);
    }
}