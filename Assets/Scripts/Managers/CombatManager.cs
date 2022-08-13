using UnityEngine;
using System.Collections;

public enum CombatStates
{
    NONE,
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class CombatManager : MonoBehaviour
{
    [Header("Combat Configuration")]
    public float timeBetweenActions = 1.5f; // 1.5 seconds

    [Header("Combat Messages Configuration")]
    public string combatStartedInfoText = "An enemy appeared...";
    public string playerTurnInfoText = "Player's turn...";
    public string playerWonInfoText = "Enemy defeated!";
    public string enemyTurnInfoText = "Enemy's turn...";
    public string enemyAttackedInfoText = "Enemy attacked!";
    public string noPrimaryWeaponInfoText = "No tiene ningún arma equipada de la primera ranura";
    public string noSecondaryWeaponInfoText = "No tiene ningún arma equipada de la segunda ranura";
    public string NoAbilityInfoText = "Ranura de habilidad vacía.";

    [Header("Dependencies")]
    public CombatUI combatUI;


    // Private

    private CombatStates _combatState = CombatStates.NONE;
    private CombatRequest _request;
    public InventorySO _inventory;
    public CombatUnitSO _Player;
    private CombatUnitSO _currentEnemy;
    private GameObject _currentEnemyGO;

    public void SetupCombat(CombatRequest request)
    {
        // Save references for later
        this._request = request;

        // Instantiate player
        GameObject playerGO = Instantiate(this._request.player.unitPrefab, request.playerPosition.position, Quaternion.identity);
        playerGO.transform.parent = request.playerPosition;

        // Start combat
        StartCoroutine(StartCombat());
    }

    public void NextCombat()
    {
        StartCoroutine(StartCombat());
    }

    public void ResetCombat()
    {
        StartCoroutine(StartCombat());
    }

    public void OnPlayerAttackPrimaryWeapon()
    {
        if (this._combatState != CombatStates.PLAYERTURN)
            return;
        if (_inventory.firstWeapon == null)
        {
            this.combatUI.SetInfoText(noPrimaryWeaponInfoText);
            return;
        }
        else
        {
            PlayerAttack(_inventory.firstWeapon.damage, false);
        }
    }

    public void OnPlayerAttackSecondaryWeapon()
    {
        if (this._combatState != CombatStates.PLAYERTURN)
            return;
        if (_inventory.secondWeapon == null)
        {
            this.combatUI.SetInfoText(noSecondaryWeaponInfoText);
            return;
        }
        else
        {
            PlayerAttack(_inventory.secondWeapon.damage, false);
        }
    }

    public void OnPlayerUseFirstMagicFirstWeapon()
    {
        if (this._combatState != CombatStates.PLAYERTURN)
            return;
        if (_inventory.firstWeapon == null)
        {
            this.combatUI.SetInfoText(noPrimaryWeaponInfoText);
            return;
        }
        if (_inventory.firstWeapon.firstAbility == null)
        {
            this.combatUI.SetInfoText(NoAbilityInfoText);
            return;
        }
        PlayerAttack(_inventory.firstWeapon.firstAbility.damage, true);
    }

    public void OnPlayerUseSecondMagicFirstWeapon()
    {
        if (this._combatState != CombatStates.PLAYERTURN)
            return;
        if (_inventory.firstWeapon == null)
        {
            this.combatUI.SetInfoText(noPrimaryWeaponInfoText);
            return;
        }
        if (_inventory.firstWeapon.secondAbility == null)
        {
            this.combatUI.SetInfoText(NoAbilityInfoText);
            return;
        }
        PlayerAttack(_inventory.firstWeapon.secondAbility.damage, true);
    }

    public void OnPlayerUseFirstMagicSecondWeapon()
    {
        if (this._combatState != CombatStates.PLAYERTURN)
            return;
        if (_inventory.secondWeapon == null)
        {
            this.combatUI.SetInfoText(noSecondaryWeaponInfoText);
            return;
        }
        if (_inventory.secondWeapon.firstAbility == null)
        {
            this.combatUI.SetInfoText(NoAbilityInfoText);
            return;
        }
        PlayerAttack(_inventory.secondWeapon.firstAbility.damage, true);
    }

    public void OnPlayerUseSecondMagicSecondWeapon()
    {
        if (this._combatState != CombatStates.PLAYERTURN)
            return;
        if (_inventory.secondWeapon == null)
        {
            this.combatUI.SetInfoText(noSecondaryWeaponInfoText);
            return;
        }
        if (_inventory.secondWeapon.secondAbility == null)
        {
            this.combatUI.SetInfoText(NoAbilityInfoText);
            return;
        }
        PlayerAttack(_inventory.secondWeapon.secondAbility.damage, true);
    }

    private void PlayerAttack(float extraDamage, bool usedMagic)
    {
        if (this._combatState != CombatStates.PLAYERTURN)
            return;

        this._request.player.AttackUnit(this._currentEnemy, extraDamage, usedMagic);

        if (this._currentEnemy.currentHP <= 0f) // Enemy is dead
        {
            StartCoroutine(CombatWon());
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }


    // Combat status

    private IEnumerator StartCombat()
    {
        this._combatState = CombatStates.START;


        int randomNumber = Random.Range(0, this._request.enemies.Length);
        this._currentEnemy = this._request.enemies[randomNumber];
        this._currentEnemy.level = this._Player.level-1;
        this._currentEnemy.LevelUP();
        this._Player.levelUp = false;

        this._currentEnemy.currentHP = this._currentEnemy.maxHP;

        // Instantiate enemy
        this._currentEnemyGO = Instantiate(this._currentEnemy.unitPrefab, this._request.enemyPosition.position, Quaternion.identity);
        this._currentEnemyGO.transform.parent = this._request.enemyPosition;

        // Configure HUD
        this.combatUI.ResetHUD();
        this.combatUI.ShowCombatMenu();
        this.combatUI.SetupHUD(this._request.player, this._currentEnemy, this._inventory, _Player.level, _inventory.gold);
        this.combatUI.SetInfoText(combatStartedInfoText);

        yield return new WaitForSeconds(this.timeBetweenActions);

        PlayerTurn();
    }

    private void PlayerTurn()
    {
        this._combatState = CombatStates.PLAYERTURN;
        this.combatUI.SetInfoText(playerTurnInfoText);

        // Wait until player attacks
    }

    private IEnumerator EnemyTurn()
    {
        this._combatState = CombatStates.ENEMYTURN;
        this.combatUI.SetInfoText(enemyTurnInfoText);

        yield return new WaitForSeconds(this.timeBetweenActions);

        // Enemy attacks
        this.combatUI.SetInfoText(enemyAttackedInfoText);
        this._currentEnemy.AttackUnit(this._request.player, 0.0f, false);

        yield return new WaitForSeconds(this.timeBetweenActions);

        if (this._request.player.currentHP <= 0f) // Player is dead
        {
            CombatLost();
        }
        else
        {
            PlayerTurn();
        }
    }

    private IEnumerator CombatWon()
    {
        this._combatState = CombatStates.WON;

        // Set UI text
        this.combatUI.SetInfoText(playerWonInfoText);

        // Get some money for your win
        int earnedGold = _currentEnemy.moneyPerDefeat;
        _inventory.gold += earnedGold;

        float earnedEXP = _currentEnemy.expPerDefeat;
        _Player.GainXP(earnedEXP);
        Debug.Log(earnedEXP);

        // Wait a bit
        yield return new WaitForSeconds(this.timeBetweenActions);

        // And show the win menu
        this.combatUI.ShowWonMenu(earnedGold, earnedEXP);
        this.ResetEnemysHPToBase();
        Destroy(this._currentEnemyGO);
        this._currentEnemyGO = null;
    }

    private void CombatLost()
    {
        this._combatState = CombatStates.LOST;

        int moneylost = _inventory.gold/2; 
        _inventory.gold -= moneylost;

        this.combatUI.ShowLostMenu(moneylost);
        this.ResetEnemysHPToBase();
    }

    private void ResetEnemysHPToBase()
    {
        this._currentEnemy.maxHP = this._currentEnemy.baseHP;
    }
}
