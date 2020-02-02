using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
public class UIManager : MonoBehaviour
{

    [Header("Scrape")]
    public TMP_Text scrapText;
    private float scrapAmount = 0f;

    [Header("Waves")]
    public GameObject wavePanel;
    public TMP_Text waveText;

    [Header("Toast")]
    public GameObject toastOB;
    public TMP_Text scrapRequiredText;
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

    [Header("Settings")]
    public GameObject settingsPanel;

    [Header("StartMenu")]
    public GameObject startMenu;
    public GameObject hudPanel;
    public GameObject optionsPanel;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playrHealth != prevPlayerHealth)
        {
            PlayerHealthBar(GameManager.Instance.playrHealth);
        }

        if (GameManager.Instance.CstlHealth != prevCastleHealth)
        {
            CastleHealthBar(GameManager.Instance.CstlHealth);
        }
        if (GameManager.Instance.scrapCount != scrapAmount)
        {
            scrapAmount = GameManager.Instance.scrapCount;
            scrapText.text = "" + scrapAmount;
        }

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
    public void ToastPopUp(float scrapeAmount)
    {
        scrapRequiredText.text = "" + scrapeAmount;
        instructionText.text = "Press E to upgrade";
        toastOB.SetActive(true);
    }

    public void OpenSettings()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        settingsPanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void GoToStartMenu()
    {
        Time.timeScale = 1;
        startMenu.SetActive(true);
        hudPanel.SetActive(false);
    }
    public void StartButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startMenu.SetActive(false);
        hudPanel.SetActive(true);
    }
}
