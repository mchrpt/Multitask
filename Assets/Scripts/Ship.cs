using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public Slider gunSlider, shieldSlider, engineSlider, healthSlider;
    public float healthEnergy, gunEnergy, engineEnergy, shieldEnergy;
    public float healthAmount, shieldAmount, engineAmount, totalEnergyAmount, currentEnergyAmount;
    public GameObject speedBar, healthBar, shieldBar, levelCompletionBar, warningText;
    public Text energyOverloadText;
    public GameObject ShipFire;
    public List<GameObject> fireList;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        currentEnergyAmount = gunSlider.value + shieldSlider.value + engineSlider.value + healthSlider.value;

        foreach(GameObject obj in fireList)
        {
            if(obj == null)
            {
                fireList.Remove(obj);
            }
        }

        if(currentEnergyAmount > (totalEnergyAmount - fireList.Count * 10))
        {
            float energyDeficit = currentEnergyAmount - (totalEnergyAmount - (fireList.Count * 10));
            StartCoroutine(energyOverload(energyDeficit));
            energyOverloadText.text = "Warning: Energy Overload Detected! " + energyDeficit + " Deficit!";
        }
        else
        {
            energyOverloadText.text = "";
        }
        getSliderValues();
        manageBarLengths();
        restartLevel();
        engineSpeed();
        StartCoroutine(rechargeShields());
        StartCoroutine(rechargeHealth());
    }
    void engineSpeed()
    {
        engineAmount = engineEnergy / 40;
        transform.Translate(Vector2.right * (engineAmount / 10));
    }
    void restartLevel()
    {
        if(healthAmount <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    void manageBarLengths()
    {
        shieldBar.transform.localScale = new Vector3(shieldAmount / 100, 1, 1);
        healthBar.transform.localScale = new Vector3(healthAmount / 100, 1, 1);
    }
    private void getSliderValues()
    {
        gunEnergy = gunSlider.value;
        engineEnergy = engineSlider.value;
        healthEnergy = healthSlider.value;
        shieldEnergy = shieldSlider.value;
    }

    public float gunFireInterval()
    {
        return -(gunEnergy / 60) + 2f;
    }
    public IEnumerator rechargeShields()
    {
        if (shieldAmount < 100)
        {
            shieldAmount += (shieldEnergy / 450);
        }
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator rechargeHealth()
    {
        if (healthAmount < 100)
        {
            healthAmount += (healthEnergy / 1000);
        }
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator energyOverload(float energyDeficit)
    {
        healthAmount -= (energyDeficit / 450);

        if (Random.Range(0, 100) == 1) {
            Vector3 fireSpawnLoc = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(-.75f, .75f), -1);
            GameObject fire = Instantiate(ShipFire, fireSpawnLoc, transform.rotation) as GameObject;
            fire.transform.SetParent(transform);
            fireList.Add(fire);
        }
        yield return new WaitForSeconds(0.1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Rock"))
        {
            float damageAmount = (collision.gameObject.transform.localScale.x * 25) * (collision.gameObject.transform.localScale.y * 25);

            if (shieldAmount >= damageAmount)
            {
                shieldAmount -= damageAmount;

            }
            else if(shieldAmount > 0 && shieldAmount < damageAmount)
            {
                
                healthAmount -= (damageAmount - shieldAmount);
                shieldAmount = 0;
            }
            else
            {
                
                healthAmount -= damageAmount;
            }

        }
    }

}
