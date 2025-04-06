using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Overlays;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TranslateController translate;

    [Header("PauseMenu")]
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

        data = DataManager.LoadData();
        isEnglish = data.isEnglish;
        volumeSlider.value = data.volume;
        sensitiveSlider.value = data.sensitive;

        OpenPause(true);

        ChangeTextLenguage();
        ConfigButtons();
    }

    private void NewGame()
    {
        DataManager.SaveData(isEnglish, volumeSlider.value, sensitiveSlider.value);
        data = DataManager.LoadData();
        isEnglish = data.isEnglish;
        volumeSlider.value = data.volume;
        sensitiveSlider.value = data.sensitive;

        SceneManager.LoadScene(1);
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
        btn1stOptions[0].onClick.AddListener(() => NewGame());
        btn1stOptions[1].onClick.AddListener(() => DataManager.SaveData(isEnglish, volumeSlider.value, sensitiveSlider.value));
        btn1stOptions[2].onClick.AddListener(() => DataManager.LoadData());
        btn1stOptions[3].onClick.AddListener(() => ChangeMenus(1));
        btn1stOptions[4].onClick.AddListener(() => Application.Quit());


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
        btn2ndOptions[4].onClick.AddListener(() =>
        {
            DataManager.SaveData(isEnglish, volumeSlider.value, sensitiveSlider.value);
            ChangeMenus(0);
        });

    }

    private void ChangeTextLenguage()
    {
        if (isEnglish) translate.EnLanguage();
        else translate.EsLanguage();
    }

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
            DataManager.SaveData(isEnglish, volumeSlider.value, sensitiveSlider.value);
        }
    }

    private void OnApplicationQuit() => DataManager.SaveData(isEnglish, volumeSlider.value, sensitiveSlider.value);
}
