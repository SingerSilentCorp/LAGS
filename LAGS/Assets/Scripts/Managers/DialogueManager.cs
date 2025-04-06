using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TransitionController transitionController;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private InMemoryVariableStorage variableStorage;
    [HideInInspector] public bool DialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    [HideInInspector] public bool autoDialog;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance != null) Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        else instance = this;

        dialogueRunner.AddCommandHandler("Stop", ()=>transitionController.InitTransition(true,()=>transitionController.gameObject.SetActive(false)));
    }

    private IEnumerator AutoContinueDialog()
    {
        yield return new WaitForSeconds(1);
        dialogueRunner.dialogueViews[0].GetComponent<LineView>().autoAdvance = true;
        if(dialogueRunner.IsDialogueRunning) StartCoroutine(AutoContinueDialog());
    }

    //This is the order you should follow ---->

    public void ShowOrHideDialogPanel(bool isActivating) => dialoguePanel.SetActive(isActivating);


    public void InitDialog(string dialogPart, bool autoModeDialog)
    {
        autoDialog = autoModeDialog;

        if(autoDialog)dialogueRunner.dialogueViews[0].GetComponent<LineView>().autoAdvance = true;
        else dialogueRunner.dialogueViews[0].GetComponent<LineView>().autoAdvance = false;

        dialogueRunner.StartDialogue(dialogPart); //ejectura linea 1
    }

    //this way the dialog will work properly despite having diffrent yarns <---

    public void IsPlayingDialog()
    {
        dialogueRunner.dialogueViews[0].GetComponent<LineView>().OnContinueClicked(); //la siguiente linea
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

    public void SetIfFaddingFalse() => variableStorage.SetValue("$createFade", false);

    public void SetChangingIMGFalse() => variableStorage.SetValue("$changeIMG", false);

    public static DialogueManager GetInstance()
    {
        return instance;
    }
}
