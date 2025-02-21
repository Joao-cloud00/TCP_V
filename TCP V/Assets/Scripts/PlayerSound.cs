using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void testFunction()
    {
        AudioManager.instance.PlaySFX("atkJogador");
    }
}
