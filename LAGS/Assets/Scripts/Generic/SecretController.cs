using UnityEngine;
using Yarn.Unity;

public class SecretController : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueRunner dialogueRunner;

    [SerializeField] private YarnProject yarn;

    [SerializeField] private string yarnTittle;

    private void Awake()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();
    }

    public void StarSecretDialog()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueManager.ShowOrHideDialogPanel(true);
            Debug.Log("Yarn config");
            dialogueManager.InitDialog(yarnTittle,true);
        }
    }
}
