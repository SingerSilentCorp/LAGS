using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TransitionController transitionController;
    private PlayerController player;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private InMemoryVariableStorage variableStorage;
    [HideInInspector] public bool DialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    [HideInInspector] public bool autoDialog;
    private bool isEnding;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        autoDialog = false;
        isEnding = false;

        if (instance != null) Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        else instance = this;

        player = FindFirstObjectByType<PlayerController>();

        dialogueRunner.AddCommandHandler("Transition", () =>
        {
            if (GetIfEnding() == false) transitionController.InitTransition(false, null);
            else transitionController.InitTransition(true, () => 
            {
                transitionController.gameObject.SetActive(false);
                player.ActiveInputs(true);

            });
        });
    }

    private IEnumerator AutoContinueDialog()
    {
        yield return new WaitForSeconds(2.0f);
        dialogueRunner.dialogueViews[0].GetComponent<LineView>().autoAdvance = true;
        if(dialogueRunner.IsDialogueRunning) StartCoroutine(AutoContinueDialog());
    }

    //This is the order you should follow ---->

    public void ShowOrHideDialogPanel(bool isActivating) => dialoguePanel.SetActive(isActivating);


    public void InitDialog(string dialogPart, bool autoModeDialog)
    {
        autoDialog = autoModeDialog;

        if(autoDialog)dialogueRunner.dialogueViews[0].GetComponent<LineView>().autoAdvance = true;
        else
        {
            dialogueRunner.dialogueViews[0].GetComponent<LineView>().autoAdvance = false;
            player.ActiveInputs(false);
        }

        dialogueRunner.StartDialogue(dialogPart); //ejectura linea 1
    }

    //this way the dialog will work properly despite having diffrent yarns <---

    public void IsPlayingDialog()
    {
        dialogueRunner.dialogueViews[0].GetComponent<LineView>().OnContinueClicked(); //la siguiente linea
        transitionController.isFadding = GetIfFadding();
        transitionController.changeImg = GetIfChangingIMG();
        isEnding = GetIfEnding();
    }

    public void IsAutoPlayingDialog() => dialogueRunner.dialogueViews[0].GetComponent<LineView>().UserRequestedViewAdvancement();

    public bool GetIfChangingIMG()
    {
        bool change;
        variableStorage.TryGetValue("$changeIMG", out change);
        return change;
    }

    public bool GetIfFadding()
    {
        bool fade;
        variableStorage.TryGetValue("$createFade", out fade);
        return fade;
    }

    private bool GetIfEnding()
    {
        bool ending;
        variableStorage.TryGetValue("$isEnding", out ending);
        return ending;
    }

    public void SetIfFaddingFalse() => variableStorage.SetValue("$createFade", false);

    public void SetChangingIMGFalse() => variableStorage.SetValue("$changeIMG", false);

    public static DialogueManager GetInstance()
    {
        return instance;
    }
}
