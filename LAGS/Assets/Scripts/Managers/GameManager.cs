using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController player;

    [Header("PLayerUI")]
    public TextMeshProUGUI[] txtPlayerStats;
    public TextMeshProUGUI txtGuide;

    [Header("Dialog and Transition")]
    [SerializeField, Space] private TransitionController transitionController;
    [SerializeField] private GameObject DialogSistemGObj;
    [SerializeField] private DialogueManager dialogueManager;

    [Header("PauseMenu")]
    [SerializeField] private GameObject pauseConteiner;
    [SerializeField] private GameObject[] pauseMenus;
    [HideInInspector] public bool pauseOpen = false;

    [Header("ConfigOptionsMenu")]
    [SerializeField] private Button[] btn1stOptions;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button[] btn2ndOptions;

    private void Awake()
    {
        //transitionController.InitTransition(false, () => dialogueManager.ShowOrHideDialogPanel(true));
        //dialogueManager.InitDialog("Prologo");

        player.ResetPlayer();

        InitPanelsBehavior();

        ConfigButtons();
        OpenPause(false);
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
        btn1stOptions[3].onClick.AddListener(() => ChangeMenus(1));
        btn1stOptions[5].onClick.AddListener(() => Application.Quit());

        btn2ndOptions[0].onClick.AddListener(() =>
        {
            if (!Screen.fullScreen) Screen.fullScreen = true;
        });
        btn2ndOptions[1].onClick.AddListener(() =>
        {
            if (Screen.fullScreen) Screen.fullScreen = false;
        });
        btn2ndOptions[4].onClick.AddListener(() => ChangeMenus(0));
    }

    private void ChangeTextLenguage()
    {

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
        }
    }
}
