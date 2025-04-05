using TMPro;
using UnityEngine;
using Yarn.Unity;

public class TranslateController : MonoBehaviour
{
    [SerializeField, Space] private TextMeshProUGUI[] pauseMenuTxt;
    [SerializeField, Space] private DialogueRunner dialogueRunner;
    [SerializeField, Space] private TextMeshProUGUI[] playerUITxt;

    public void EnLanguage()
    {
        //PauseMenu
        pauseMenuTxt[0].text = "Resume";
        pauseMenuTxt[1].text = "Save";
        pauseMenuTxt[2].text = "Load";
        pauseMenuTxt[3].text = "Options";
        pauseMenuTxt[4].text = "Main Menu";
        pauseMenuTxt[5].text = "Exit";
        pauseMenuTxt[6].text = "Volume";
        pauseMenuTxt[7].text = "Screen Mode";
        pauseMenuTxt[8].text = "Full Screen";
        pauseMenuTxt[9].text = "Window";
        pauseMenuTxt[10].text = "Lenguage";
        pauseMenuTxt[11].text = "Spanish";
        pauseMenuTxt[12].text = "English";
        pauseMenuTxt[13].text = "Return";

        //PLayerUI
        this.GetComponent<GameManager>().GuideTxtConfig(0);
    }

    public void EsLanguage()
    {
        //PauseMenu
        pauseMenuTxt[0].text = "Volver";
        pauseMenuTxt[1].text = "Guardar";
        pauseMenuTxt[2].text = "Cargar";
        pauseMenuTxt[3].text = "Opciones";
        pauseMenuTxt[4].text = "Menu principal";
        pauseMenuTxt[5].text = "Salir";
        pauseMenuTxt[6].text = "Volumen";
        pauseMenuTxt[7].text = "Modo de pantalla";
        pauseMenuTxt[8].text = "Pantalla completa";
        pauseMenuTxt[9].text = "Ventana";
        pauseMenuTxt[10].text = "Lenguaje";
        pauseMenuTxt[11].text = "Español";
        pauseMenuTxt[12].text = "Ingles";
        pauseMenuTxt[13].text = "Volver";

        //PLayerUI
        this.GetComponent<GameManager>().GuideTxtConfig(0);
    }
}
