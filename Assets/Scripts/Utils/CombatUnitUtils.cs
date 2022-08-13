using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnitUtils : MonoBehaviour
{
public CombatUnitSO _player;

    public void OnCompleteHeal()
    {
        _player.ResetHP();
    }
}
