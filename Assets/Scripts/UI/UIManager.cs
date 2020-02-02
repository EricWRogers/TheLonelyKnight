using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{

    [Header("Scrape")]
    public TMP_Text scrapText;
    private float prevScrapAmount = 0f;

    [Header("Waves")]
    public GameObject wavePanel;
    public TMP_Text waveText;
    private bool waveCountDown = false;

    [Header("Toast")]
    public GameObject toastOB;
    public TMP_Text scrapRequiredText;
    public TMP_Text instructionText;

    [Header("Health Bars")]
    public Animator playerHealthBarAnim;
    public Animator castleHealthBarAnim;
    public Image[] playerHealthBarImages;
    public Image[] castleHealthBarImages;
    public Color[] healthColors;
    private float prevPlayerHealth = 100f;
    private float prevCastleHealth = 100f;

    public TMP_Text playerHealthText;
    public TMP_Text castleHealthText;

    [Header("Settings")]
    public GameObject settingsPanel;
    public int alreadyPressed = 0;

    [Header("StartMenu")]
    public GameObject startMenu;
    public GameObject hudPanel;
    public GameObject optionsPanel;

    public static UIManager Instance { get; private set; } = null;

    //Destroy the instance.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.playrHealth != prevPlayerHealth)
        {
            playerHealthText.text = "" + GameManager.Instance.playrHealth;
            PlayerHealthBar(GameManager.Instance.playrHealth);
        }

        if (GameManager.Instance.castleHealth != prevCastleHealth)
        {
            //castleHealthText.text = "" + GameManager.Instance.castleHealth;
            CastleHealthBar(GameManager.Instance.castleHealth);
        }
        if (GameManager.Instance.scrapCount != prevScrapAmount)
        {
            prevScrapAmount = GameManager.Instance.scrapCount;
            scrapText.text = "" + prevScrapAmount;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (alreadyPressed > 0)
            {
                ResumeGame();
            }
            else
            {
                OpenSettings();
                alreadyPressed++;
            }
        }

        if (waveCountDown)
        {
            if (GameManager.Instance.WTimer > 0)
            {
                waveText.text = "Wave " + GameManager.Instance.WaveNumber + " incoming... " + (int)GameManager.Instance.WTimer;
            }
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
                //playerHealthBarImages[i].tintColor = healthColors[0];

                if (i > avaibleBars)
                {
                    playerHealthBarImages[i].SetActive(false);
                }
                else
                {
                    playerHealthBarImages[i].SetActive(true);
                }
            }

        }
        else if (health < 67 && health > 34)
        {
            for (int i = 0; i < playerHealthBarImages.Length; i++)
            {
                //playerHealthBarImages[i].tintColor = healthColors[1];

                if (i > avaibleBars)
                {
                    playerHealthBarImages[i].SetActive(false);
                }
                else
                {
                    playerHealthBarImages[i].SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < playerHealthBarImages.Length; i++)
            {
                //playerHealthBarImages[i].tintColor = healthColors[2];

                if (i > avaibleBars)
                {
                    playerHealthBarImages[i].SetActive(false);
                }
                else
                {
                    playerHealthBarImages[i].SetActive(true);
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
                //castleHealthBarImages[i].tintColor = healthColors[0];

                if (i > avaibleBars)
                {
                    castleHealthBarImages[i].SetActive(false);
                }
                else
                {
                    playerHealthBarImages[i].SetActive(true);
                }
            }

        }
        else if (health < 67 && health > 34)
        {
            for (int i = 0; i < castleHealthBarImages.Length; i++)
            {
                //castleHealthBarImages[i].tintColor = healthColors[1];

                if (i > avaibleBars)
                {
                    castleHealthBarImages[i].SetActive(false);
                }
                else
                {
                    playerHealthBarImages[i].SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < castleHealthBarImages.Length; i++)
            {
                //castleHealthBarImages[i].tintColor = healthColors[2];

                if (i > avaibleBars)
                {
                    castleHealthBarImages[i].SetActive(false);
                }
                else
                {
                    playerHealthBarImages[i].SetActive(true);
                }
            }
        }

    }
    public void ToastPopUp(float scrapAmount)
    {
        scrapRequiredText.text = "" + scrapAmount;
        instructionText.text = "Press E to repair";
        toastOB.SetActive(true);
    }
    public void CloseToastPopUp()
    {
        toastOB.SetActive(false);
    }

    public void OpenSettings()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        settingsPanel.SetActive(true);
    }
    public void ResumeGame()
    {
        alreadyPressed = 0;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void GoToStartMenu()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void StartButton()
    {
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.StartRestingState();
        startMenu.SetActive(false);
        hudPanel.SetActive(true);
    }

    public void WaveIncoming()
    {
        waveCountDown = true;
        wavePanel.SetActive(true);
    }
}
