using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Money money;

    static int lifeRestoreCost = 10;
    static int healthRestoreCost = 5;
    static int speedIncreaseCost = 1;
    static int swordSpeedCost = 1;
    static int swordDamageCost = 1;
    static int castSpeedCost = 1;
    static int castDamageCost = 1;
    static int castBackShotCost = 10;
    static int castExtraShotCost = 10;
    static int castProjectileSpeedCost = 1;
    static int castProjectileSizeCost = 1;
    static int castFireCost = 10;
    static int castFireDamageCost = 1;
    static int castIceCost = 10;
    static int castIceDamageCost = 1;
    static int decreaseEnemySpeedCost = 1;
    static int decreaseEnemyDamageCost = 1;

    public Button lifeRestoreButton;
    public Button healthRestoreButton;
    public Button speedIncreaseButton;
    public Button swordSpeedButton;
    public Button swordDamageButton;
    public Button castSpeedButton;
    public Button castDamageButton;
    public Button castBackShotButton;
    public Button castExtraShotButton;
    public Button castProjectileSpeedButton;
    public Button castProjectileSizeButton;
    public Button castFireButton;
    public Button castFireDamageButton;
    public Button castIceButton;
    public Button castIceDamageButton;
    public Button decreaseEnemySpeedButton;
    public Button decreaseEnemyDamageButton;

    public Text lifeRestoreText;
    public Text healthRestoreText;
    public Text speedIncreaseText;
    public Text swordSpeedText;
    public Text swordDamageText;
    public Text castSpeedText;
    public Text castDamageText;
    public Text castBackShotText;
    public Text castExtraShotText;
    public Text castProjectileSpeedText;
    public Text castProjectileSizeText;
    public Text castFireText;
    public Text castFireDamageText;
    public Text castIceText;
    public Text castIceDamageText;
    public Text decreaseEnemySpeedText;
    public Text decreaseEnemyDamageText;

    // Start is called before the first frame update
    void Start()
    {
        setButtons();
        setTexts();
    }
    
    void setButtons()
    {
        if (Player.livesCount == 3) lifeRestoreButton.interactable = false;
        if (Player.currentHealth == Player.maxHealth) healthRestoreButton.interactable = false;

        if (lifeRestoreCost > Money.total) lifeRestoreButton.interactable = false;
        if (healthRestoreCost > Money.total) healthRestoreButton.interactable = false;
        if (speedIncreaseCost > Money.total) speedIncreaseButton.interactable = false;
        if (swordSpeedCost > Money.total) swordSpeedButton.interactable = false;
        if (swordDamageCost > Money.total) swordDamageButton.interactable = false;
        if (castSpeedCost > Money.total) castSpeedButton.interactable = false;
        if (castDamageCost > Money.total) castDamageButton.interactable = false;
        if (castBackShotCost > Money.total) castBackShotButton.interactable = false;
        if (castExtraShotCost > Money.total) castExtraShotButton.interactable = false;
        if (castProjectileSpeedCost > Money.total) castProjectileSpeedButton.interactable = false;
        if (castProjectileSizeCost > Money.total) castProjectileSizeButton.interactable = false;
        if (castFireCost > Money.total) castFireButton.interactable = false;
        if (castFireDamageCost > Money.total) castFireDamageButton.interactable = false;
        if (castIceCost > Money.total) castIceButton.interactable = false;
        if (castIceDamageCost > Money.total) castIceDamageButton.interactable = false;
        if (decreaseEnemySpeedCost > Money.total) decreaseEnemySpeedButton.interactable = false;
        if (decreaseEnemyDamageCost > Money.total) decreaseEnemyDamageButton.interactable = false;
    }

    void setTexts()
    {
        lifeRestoreText.text = "Restore one lost life\n$" + lifeRestoreCost;
        healthRestoreText.text = "Fully restore character health\n$" + healthRestoreCost;
        speedIncreaseText.text = "Increase character speed\n$" + speedIncreaseCost;
        swordSpeedText.text = "Increase sword attack speed\n$" + swordSpeedCost;
        swordDamageText.text = "Increase sword attack damage\n$" + swordDamageCost;
        castSpeedText.text = "Increase casting speed\n$" + castSpeedCost;
        castDamageText.text = "Increase casting damage\n$" + castDamageCost;
        castBackShotText.text = "Add a backward shot to casting\n$" + castBackShotCost;
        castExtraShotText.text = "Add an extra shot to casting\n$" + castExtraShotCost;
        castProjectileSpeedText.text = "Increase cast projectile speed\n$" + castProjectileSpeedCost;
        castProjectileSizeText.text = "Increase cast projectile size\n$" + castProjectileSizeCost;
        castFireText.text = "Add fire element to casting that burns enemies over time\n$" + castFireCost;
        castFireDamageText.text = "Increase burn damage from fire shots\n$" + castFireDamageCost;
        castIceText.text = "Add ice element to casting that freezes enemies\n$" + castIceCost;
        castIceDamageText.text = "Increase freeze duration from ice shots\n$" + castIceDamageCost;
        decreaseEnemySpeedText.text = "Decrease enemy speed\n$" + decreaseEnemySpeedCost;
        decreaseEnemyDamageText.text = "Decrease enemy damage\n$" + decreaseEnemyDamageCost;
    }

    public void LifeRestoreButton()
    {
        Player.livesCount++;
        money.SpendMoney(lifeRestoreCost);
        setButtons();
        setTexts();
    }

    public void HealthRestoreButton()
    {
        Player.currentHealth = Player.maxHealth;
        money.SpendMoney(healthRestoreCost);
        setButtons();
        setTexts();
    }

    public void SpeedIncreaseButton()
    {
        Player.speed *= 1.1f;
        money.SpendMoney(speedIncreaseCost);
        speedIncreaseCost += 5;
        setButtons();
        setTexts();
    }

    public void SwordSpeedButton()
    {
        Player.attackRate *= 1.1f;
        money.SpendMoney(swordSpeedCost);
        swordSpeedCost += 5;
        setButtons();
        setTexts();
    }

    public void SwordDamageButton()
    {
        Player.swordDamage += 1;
        money.SpendMoney(swordDamageCost);
        swordDamageCost += 5;
        setButtons();
        setTexts();
    }

    public void CastSpeedButton()
    {
        Player.castRate *= 1.1f;
        money.SpendMoney(castSpeedCost);
        castSpeedCost += 5;
        setButtons();
        setTexts();
    }

    public void CastDamageButton()
    {
        Player.castDamage += 1;
        money.SpendMoney(castDamageCost);
        castDamageCost += 5;
        setButtons();
        setTexts();
    }

    public void CastBackShotButton()
    {
        Player.backShot = true;
        money.SpendMoney(castBackShotCost);
        setButtons();
        setTexts();
        castBackShotButton.gameObject.SetActive(false);
    }

    public void CastExtraShotButton()
    {
        Player.extraShot += 1;
        money.SpendMoney(castExtraShotCost);
        castExtraShotCost += 5;
        setButtons();
        setTexts();
        if (Player.extraShot == 2)
        {
            castExtraShotButton.gameObject.SetActive(false);
        }
    }

    public void CastProjectileSpeedButton()
    {
        Player.castSpeed *= 1.1f;
        money.SpendMoney(castProjectileSpeedCost);
        castProjectileSpeedCost += 5;
        setButtons();
        setTexts();
    }

    public void CastProjectileSizeButton()
    {
        Player.castSize *= 1.1f;
        money.SpendMoney(castProjectileSizeCost);
        castProjectileSizeCost += 5;
        setButtons();
        setTexts();
    }

    public void CastFireButton()
    {
        Player.fireShot = true;
        money.SpendMoney(castFireCost);
        setButtons();
        setTexts();
        castFireButton.gameObject.SetActive(false);
        castIceButton.gameObject.SetActive(false);
        castIceDamageButton.gameObject.SetActive(false);
    }

    public void CastFireDamageButton()
    {
        Player.burnDamage += 1;
        money.SpendMoney(castFireDamageCost);
        castFireDamageCost += 5;
        setButtons();
        setTexts();
    }

    public void CastIceButton()
    {
        Player.iceShot = true;
        money.SpendMoney(castIceCost);
        setButtons();
        setTexts();
        castIceButton.gameObject.SetActive(false);
        castFireButton.gameObject.SetActive(false);
        castFireDamageButton.gameObject.SetActive(false);
    }

    public void CastIceDamageButton()
    {
        Player.iceLength *= 1.1f;
        money.SpendMoney(castIceDamageCost);
        castIceDamageCost += 5;
        setButtons();
        setTexts();
    }

    public void DecreaseEnemySpeedButton()
    {
        Goblin.speed *= .9f;
        money.SpendMoney(decreaseEnemySpeedCost);
        decreaseEnemySpeedCost += 5;
        setButtons();
        setTexts();
    }

    public void DecreaseEnemyDamageButton()
    {
        Goblin.damage -= 1;
        money.SpendMoney(decreaseEnemyDamageCost);
        decreaseEnemyDamageCost += 5;
        setButtons();
        setTexts();
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("Level1");
    }
}
