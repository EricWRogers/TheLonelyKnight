using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
public class UIManager : MonoBehaviour
{

    [Header("Scrape")]
    public TMP_Text scrapeText;

    [Header("Waves")]
    public GameObject wavePanel;
    public TMP_Text waveText;

    [Header("Toast")]
    public GameObject toastOB;
    public TMP_Text scrapeRequiredText;
    public TMP_Text instructionText;

    [Header("Health Bars")]
    public Animator playerHealthBarAnim;
    public Animator castleHealthBarAnim;
    [SerializeField]
    private GameObject[] playerHealthBarImages;
    [SerializeField]
    private GameObject[] castleHealthBarImages;
    [SerializeField]
    private Color[] healthColors;
    private float prevPlayerHealth = 100f;
    private float prevCastleHealth = 100f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerHealthBar(float health)
    {
        int avaibleBars = (int)(health / 6.25f);
        if (health < prevPlayerHealth)
        {
            playerHealthBarAnim.SetBool("Play", true);
        }
        else
        {
            playerHealthBarAnim.SetBool("Play", false);
        }
        prevPlayerHealth = health;
        
        if (health > 67)
        {
            for (int i = 0; i < playerHealthBarImages.Length; i++)
            {
                playerHealthBarImages[i].GetComponent<Image>().tintColor = healthColors[0];

                if (i > avaibleBars)
                {
                    playerHealthBarImages[i].SetActive(false);
                }
            }

        }
        else if (health < 67 && health > 34)
        {
            for (int i = 0; i < playerHealthBarImages.Length; i++)
            {
                playerHealthBarImages[i].GetComponent<Image>().tintColor = healthColors[1];

                if (i > avaibleBars)
                {
                    playerHealthBarImages[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < playerHealthBarImages.Length; i++)
            {
                playerHealthBarImages[i].GetComponent<Image>().tintColor = healthColors[2];

                if (i > avaibleBars)
                {
                    playerHealthBarImages[i].SetActive(false);
                }
            }
        }

    }
    public void CastleHealthBar(float health)
    {
        int avaibleBars = (int)(health / 6.25f);
        
        if (health < prevCastleHealth)
        {
            castleHealthBarAnim.SetBool("Play", true);
        }
        else
        {
            castleHealthBarAnim.SetBool("Play", false);
        }
        prevCastleHealth = health;
        
        if (health > 67)
        {
            for (int i = 0; i < castleHealthBarImages.Length; i++)
            {
                castleHealthBarImages[i].GetComponent<Image>().tintColor = healthColors[0];

                if (i > avaibleBars)
                {
                    castleHealthBarImages[i].SetActive(false);
                }
            }

        }
        else if (health < 67 && health > 34)
        {
            for (int i = 0; i < castleHealthBarImages.Length; i++)
            {
                castleHealthBarImages[i].GetComponent<Image>().tintColor = healthColors[1];

                if (i > avaibleBars)
                {
                    castleHealthBarImages[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < castleHealthBarImages.Length; i++)
            {
                castleHealthBarImages[i].GetComponent<Image>().tintColor = healthColors[2];

                if (i > avaibleBars)
                {
                    castleHealthBarImages[i].SetActive(false);
                }
            }
        }

    }
}
