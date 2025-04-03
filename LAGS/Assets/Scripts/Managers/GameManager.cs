using TMPro;
using UnityEngine;

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
    public bool pauseOpen = false;

    private void Awake()
    {
        //transitionController.InitTransition(false, () => dialogueManager.ShowOrHideDialogPanel(true));
        //dialogueManager.InitDialog("Prologo");

        player.ResetPlayer();

        InitPanelsBehavior();
    }

    private void InitPanelsBehavior()
    {
        txtGuide.gameObject.SetActive(false);
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
