using System;
using System.Collections;
using Player;
using UnityEngine;

public class JumpResetter : MonoBehaviour , IItem 
{
    [SerializeField] private PlayerMovement playerMovement;
    
    public void Collect()
    {
        playerMovement.JumpAdd();
    }

 
    
}
