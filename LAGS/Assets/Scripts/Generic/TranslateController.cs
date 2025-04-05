using TMPro;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class TranslateController : MonoBehaviour
{
    [SerializeField, Space] private TextMeshProUGUI[] pauseMenuTxt;
    [SerializeField, Space] private DialogueRunner dialogueRunner;
    [SerializeField, Space] private TextMeshProUGUI[] playerUITxt;

    public void EnLanguage()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //PauseMenu
            pauseMenuTxt[0].text = "Save";
            pauseMenuTxt[1].text = "Load";
            pauseMenuTxt[2].text = "Options";
            pauseMenuTxt[3].text = "Exit";
            pauseMenuTxt[4].text = "Volume";
            pauseMenuTxt[5].text = "Screen Mode";
            pauseMenuTxt[6].text = "Full Screen";
            pauseMenuTxt[7].text = "Window";
            pauseMenuTxt[8].text = "Lenguage";
            pauseMenuTxt[9].text = "Spanish";
            pauseMenuTxt[10].text = "English";
            pauseMenuTxt[11].text = "Return";

        }
        else{
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
    }

    public void EsLanguage()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //PauseMenu
            pauseMenuTxt[0].text = "Guardar";
            pauseMenuTxt[1].text = "Cargar";
            pauseMenuTxt[2].text = "Opciones";
            pauseMenuTxt[3].text = "Salir";
            pauseMenuTxt[4].text = "Volumen";
            pauseMenuTxt[5].text = "Modo de pantalla";
            pauseMenuTxt[6].text = "Pantalla completa";
            pauseMenuTxt[7].text = "Ventana";
            pauseMenuTxt[8].text = "Lenguaje";
            pauseMenuTxt[9].text = "Español";
            pauseMenuTxt[10].text = "Ingles";
            pauseMenuTxt[11].text = "Volver";
        }
        else
        {
            //PauseMenu
            pauseMenuTxt[1].text = "Guardar";
            pauseMenuTxt[2].text = "Cargar";
            pauseMenuTxt[3].text = "Opciones";
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
}
