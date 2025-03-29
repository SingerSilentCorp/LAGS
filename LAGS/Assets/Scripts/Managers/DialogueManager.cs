using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private InMemoryVariableStorage variableStorage;
    [HideInInspector] public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        else instance = this;
    }

    public void ShowOrHideDialogPanel(bool isActivating) => dialoguePanel.SetActive(isActivating);

    public void InitDialog(string dialogPart) => dialogueRunner.StartDialogue(dialogPart); //ejectura linea 1

    public void IsPlayingDialog() => dialogueRunner.dialogueViews[0].GetComponent<LineView>().OnContinueClicked(); //la siguiente linea

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
