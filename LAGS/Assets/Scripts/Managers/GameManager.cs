using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private TranslateController translate;

    [Header("PLayerUI")]
    [SerializeField] private GameObject playerUICanvas;
    [SerializeField] private TextMeshProUGUI[] txtPlayerStats;
    [SerializeField] private TextMeshProUGUI txtGuide;

    [Header("Dialog and Transition")]
    [SerializeField, Space] private TransitionController transitionController;
    [SerializeField] private GameObject DialogSistemGObj;
    [SerializeField] private DialogueManager dialogueManager;

    [Header("PauseMenu")]
    [SerializeField] private GameObject cavasPause;
    [SerializeField] private GameObject pauseConteiner;
    [SerializeField] private GameObject[] pauseMenus;
    [HideInInspector] public bool pauseOpen = false;

    [Header("ConfigOptionsMenu")]
    [SerializeField] private Button[] btn1stOptions;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitiveSlider;
    [SerializeField] private Button[] btn2ndOptions;


    [Header("Data")]
    private Data data;

    [HideInInspector] public bool isEnglish = false;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        ConfigScene();

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            DontDestroyOnLoad(playerUICanvas);
            DontDestroyOnLoad(cavasPause);
            DontDestroyOnLoad(transitionController.gameObject);
            DontDestroyOnLoad(DialogSistemGObj);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(this.transform.parent);
        }

        data = DataManager.LoadData();
        isEnglish = data.isEnglish;
        volumeSlider.value = data.volume;
        sensitiveSlider.value = data.sensitive;

        ConfigButtons();

        player.ResetPlayer();
        InitPanelsBehavior();
        OpenPause(false);

        ChangeTextLenguage();
    }

    private void Start()
    {
        transitionController.InitTransition(false, () =>
        {
            dialogueManager.ShowOrHideDialogPanel(true);
            dialogueManager.InitDialog("Intro", false);
        });
    }

    private void InitPanelsBehavior()
    {
        txtGuide.gameObject.SetActive(false);
    }

    private void ChangeMenus(int menuIndex)
    {
        for (int i = 0; i < pauseMenus.Length; i++)
        {
            pauseMenus[i].SetActive(false);
        }

        switch (menuIndex)
        {
            case 0:
                pauseMenus[0].SetActive(true);
                break;
            case 1:
                pauseMenus[1].SetActive(true);
                break;
        }
    }

    private void ConfigButtons()
    {
        btn1stOptions[0].onClick.AddListener(() => OpenPause(false));
        btn1stOptions[1].onClick.AddListener(() => DataManager.SaveData(isEnglish, volumeSlider.value, sensitiveSlider.value));
        btn1stOptions[2].onClick.AddListener(() => DataManager.LoadData());
        btn1stOptions[3].onClick.AddListener(() => ChangeMenus(1));
        btn1stOptions[4].onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
            ConfigScene();
        });
        btn1stOptions[5].onClick.AddListener(() => Application.Quit());

        btn2ndOptions[0].onClick.AddListener(() =>
        {
            if (!Screen.fullScreen) Screen.fullScreen = true;
        });
        btn2ndOptions[1].onClick.AddListener(() =>
        {
            if (Screen.fullScreen) Screen.fullScreen = false;
        });

        volumeSlider.onValueChanged.AddListener((value) =>
        {
            data.volume = value;
            volumeSlider.value = value;
        });
        sensitiveSlider.onValueChanged.AddListener((value) =>
        {
            data.sensitive = value;
            sensitiveSlider.value = value;
        });


        btn2ndOptions[2].onClick.AddListener(() => translate.EsLanguage());
        btn2ndOptions[3].onClick.AddListener(() => translate.EnLanguage());
        btn2ndOptions[4].onClick.AddListener(() => ChangeMenus(0));
    }

    private void ChangeTextLenguage()
    {
        if (isEnglish) translate.EnLanguage();
        else translate.EsLanguage();
    }

    public void ShowTxtGuide(bool isShowing) => txtGuide.gameObject.SetActive(isShowing);

    public void MouseVisible(bool isVisible)
    {
        if (isVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OpenPause(bool isOpenning)
    {
        for (int i = 0; i < pauseMenus.Length; i++)
        {
            pauseMenus[i].SetActive(false);
        }

        if (isOpenning)
        {
            Time.timeScale = 0.0f;
            pauseOpen = true;
            pauseConteiner.SetActive(true);
            pauseMenus[0].SetActive(true);
            MouseVisible(true);


        }
        else
        {
            pauseOpen = false;
            for (int i = 0; i < pauseMenus.Length; i++)
            {
                pauseMenus[i].SetActive(false);
            }

            pauseConteiner.SetActive(false);
            MouseVisible(false);
            Time.timeScale = 1.0f;

            DataManager.SaveData(isEnglish, volumeSlider.value, sensitiveSlider.value);
        }
    }

    public void GuideTxtConfig(int indexTxt)
    {
        //0 == interact, 1 == locked door, 2 == switchTxtWhenUnlock

        if (isEnglish)
        {
            if (indexTxt == 0)
            {
                txtGuide.text = "Press space to interact";
            }
            else if (indexTxt == 1)
            {
                txtGuide.text = "Locked";
            }
            else if (indexTxt == 1)
            {
                txtGuide.text = "Door has been unlocked ";
            }
        }
        else
        {
            if (indexTxt == 0)
            {
                txtGuide.text = "Presiona Espacio para interactuar";
            }
            else if (indexTxt == 1)
            {
                txtGuide.text = "Bloqueado";
            }
            else if (indexTxt == 1)
            {
                txtGuide.text = "La puerta a sido desbloqueada";
            }
        }
    }

    public void ActivePlayerInput(bool isActivating)
    {
        if (isActivating) player.ActiveInputs(isActivating);
        else player.ActiveInputs(isActivating);
    }

    public void ChangeToAnotherLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
        ConfigScene();
    }

    //Config loadScene when loaded
    public void ConfigScene()
    {
        SceneManager.sceneLoaded += OnSceneLoadedTemp;
    }

    private void OnSceneLoadedTemp(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this);
            Destroy(transform.parent.gameObject);
            Destroy(playerUICanvas);
            Destroy(cavasPause);
            Destroy(transitionController.gameObject);
            Destroy(DialogSistemGObj);
            Destroy(player);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            player.transform.position = new Vector3(156.399994f, 0.699000001f, 108.5f);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            player.transform.position = new Vector3(-12f, 2f, 53f);
        }

        player.transform.Rotate(new Vector3(0f, 180f, 0f));
        SceneManager.sceneLoaded -= OnSceneLoadedTemp;
    }

    public void UpdateHP(float health) => txtPlayerStats[0].text = Mathf.RoundToInt(health).ToString() + "%";

    public void UpdateArmor(float armor) => txtPlayerStats[1].text = Mathf.RoundToInt(armor).ToString() + "%";

    private void OnApplicationQuit() => DataManager.SaveData(isEnglish, volumeSlider.value, sensitiveSlider.value);

}