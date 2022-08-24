using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [Header("Dependencies")]
    public GameObject combatMenu;
    public GameObject wonMenu;
    public GameObject levelupMenu;
    public GameObject lostMenu;

    public TextMeshProUGUI infoText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI earnedGoldWonMenuText;
    public TextMeshProUGUI earnedEXPWonMenuText;

    public TextMeshProUGUI earnedGoldlevelupMenuText;
    public TextMeshProUGUI earnedEXPlevelupMenuText;
    public TextMeshProUGUI actualLevel;

    public TextMeshProUGUI moneyLost;

    public TextMeshProUGUI playerName;
    public Slider playerHP;
    public Slider playerMana;
    public TextMeshProUGUI enemyName;
    public Slider enemyHP;
    public Slider enemyMana;

    public Image FirstWeapon;
    public Image SecondWeapon;

    public Text FirstWeaponFirstAbilityName;
    public Image FirstWeaponFirstAbilitySprite;
    public Text FirstWeaponSecondAbilityName;
    public Image FirstWeaponSecondAbilitySprite;
    public Text SecondWeaponFirstAbilityName;
    public Image SecondWeaponFirstAbilitySprite;
    public Text SecondWeaponSecondAbilityName;    
    public Image SecondWeaponSecondAbilitySprite;

    // Private
    private CombatUnitSO _player;
    private CombatUnitSO _enemy;
    private InventorySO _Inventory;
    private int level;


    public void SetupHUD(CombatUnitSO player, CombatUnitSO enemy, InventorySO inventory, int level, int gold)
    {
        // Save references for later
        this._player = player;
        this._enemy = enemy;
        this._Inventory = inventory;
        this.level = level;

        // Link HUD
        this.playerName.text = this._player.unitName;
        this.playerHP.minValue = 0;
        this.playerHP.maxValue = this._player.maxHP;

        this.playerMana.minValue = 0;
        this.playerMana.maxValue = this._player.maxMana;

        this.enemyName.text = this._enemy.unitName;
        this.enemyHP.minValue = 0;
        this.enemyHP.maxValue = this._enemy.maxHP;

        this.enemyMana.minValue = 0;
        this.enemyMana.maxValue = this._enemy.maxMana;

        this.levelText.text = level.ToString();

        if(inventory.firstWeapon != null)
        {
            this.FirstWeapon.gameObject.SetActive(true);
            this.FirstWeapon.sprite = inventory.firstWeapon.weaponImage;
        }
        if(inventory.secondWeapon != null)
        {
            this.SecondWeapon.gameObject.SetActive(true);
            this.SecondWeapon.sprite = inventory.secondWeapon.weaponImage;
        }

        if (inventory.firstWeapon != null)
        {
            if(inventory.firstWeapon.firstAbility != null)
            {
                this.FirstWeaponFirstAbilitySprite.gameObject.SetActive(true);
                this.FirstWeaponFirstAbilityName.text = inventory.firstWeapon.firstAbility.name;
                this.FirstWeaponFirstAbilitySprite.sprite = inventory.firstWeapon.firstAbility.Image;
            }
            if(inventory.firstWeapon.secondAbility != null)
            {
                this.FirstWeaponSecondAbilitySprite.gameObject.SetActive(true);
                this.FirstWeaponSecondAbilityName.text = inventory.firstWeapon.secondAbility.name;
                this.FirstWeaponSecondAbilitySprite.sprite = inventory.firstWeapon.secondAbility.Image;
            }
        }
        if (inventory.secondWeapon != null)
        {
            if(inventory.secondWeapon.firstAbility != null)
            {
                this.SecondWeaponFirstAbilitySprite.gameObject.SetActive(true);
                this.SecondWeaponFirstAbilityName.text = inventory.secondWeapon.firstAbility.name;
                this.SecondWeaponFirstAbilitySprite.sprite = inventory.secondWeapon.firstAbility.Image;
            }
            if(inventory.secondWeapon.secondAbility != null)
            {
                this.SecondWeaponSecondAbilitySprite.gameObject.SetActive(true);
                this.SecondWeaponSecondAbilityName.text = inventory.secondWeapon.secondAbility.name;
                this.SecondWeaponSecondAbilitySprite.sprite = inventory.secondWeapon.secondAbility.Image;
            }
        }

    }


    public void SetInfoText(string infoText)
    {
        this.infoText.text = infoText;
    }

    public void ShowCombatMenu()
    {
        combatMenu.SetActive(true);
        wonMenu.SetActive(false);
        lostMenu.SetActive(false);
        levelupMenu.SetActive(false);
    }

    public void ShowWonMenu(int earnedGold, float earnedEXP)
    {
        if(this._player.levelUp == false)
        {
            this.earnedGoldWonMenuText.text = "+" + earnedGold.ToString() + " oro";
            this.earnedEXPWonMenuText.text = "+" + earnedEXP.ToString() + " exp";

            wonMenu.SetActive(true);
            combatMenu.SetActive(false);
            lostMenu.SetActive(false);
            levelupMenu.SetActive(false);
        }
        else
        {
            this.earnedGoldlevelupMenuText.text = "+" + earnedGold.ToString() + " oro";
            this.earnedEXPlevelupMenuText.text = "+" + earnedEXP.ToString() + " exp";
            this.actualLevel.text = "Subiste a nivel " + (this.level + 1).ToString();

            wonMenu.SetActive(false);
            combatMenu.SetActive(false);
            lostMenu.SetActive(false);
            levelupMenu.SetActive(true);
        }
    }

    public void ShowLostMenu(int moneylost)
    {
        this.moneyLost.text = "-" + moneylost.ToString() + " oro";

        lostMenu.SetActive(true);
        wonMenu.SetActive(false);
        combatMenu.SetActive(false);
        levelupMenu.SetActive(false);
    }

    public void ResetHUD()
    {
        this._player = null;
        this._enemy = null;

        // Link HUD
        this.playerName.text = "";
        this.playerHP.minValue = 0;
        this.playerHP.maxValue = 0;
        this.playerHP.value = 0;

        this.playerMana.minValue = 0;
        this.playerMana.maxValue = 0;
        this.playerMana.value = 0;

        this.enemyName.text = "";
        this.enemyHP.minValue = 0;
        this.enemyHP.maxValue = 0;
        this.enemyHP.value = 0;

        this.enemyMana.minValue = 0;
        this.enemyMana.maxValue = 0;
        this.enemyMana.value = 0;

        this.levelText.text = "";

        this.FirstWeaponFirstAbilityName.text = "";
        this.FirstWeaponSecondAbilityName.text = "";
        this.SecondWeaponFirstAbilityName.text = "";
        this.SecondWeaponSecondAbilityName.text = "";

        this.FirstWeapon.gameObject.SetActive(false);
        this.SecondWeapon.gameObject.SetActive(false);

        this.FirstWeaponFirstAbilitySprite.gameObject.SetActive(false);
        this.FirstWeaponSecondAbilitySprite.gameObject.SetActive(false);
        this.SecondWeaponFirstAbilitySprite.gameObject.SetActive(false);
        this.SecondWeaponSecondAbilitySprite.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (this._player != null) {
            this.playerHP.value = this._player.currentHP;
            this.playerMana.value = this._player.currentMana;
        }

        if (this._enemy != null) {
            this.enemyHP.value = this._enemy.currentHP;
            this.enemyMana.value = this._enemy.currentMana;
        }
    }
}
