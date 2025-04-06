using TMPro;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Linq;
using Yarn.Unity.UnityLocalization;

public class TranslateController : MonoBehaviour
{
    [SerializeField, Space] private TextMeshProUGUI[] pauseMenuTxt;
    [SerializeField, Space] private DialogueRunner dialogueRunner;
    [SerializeField, Space] private TextMeshProUGUI[] playerUITxt;

    private void Awake()
    {
        if (this.GetComponent<GameManager>() != null)
        {
            if (this.GetComponent<GameManager>().isEnglish) EnLanguage();
        }
        else
        {
            if (this.GetComponent<MenuManager>().isEnglish) EsLanguage();
        }
    }

    public void EnLanguage()
    {
        if (this.GetComponent<GameManager>() != null) this.GetComponent<GameManager>().isEnglish = true;
        else this.GetComponent<MenuManager>().isEnglish = true;

        CambiarIdioma("en-US");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //PauseMenu
            pauseMenuTxt[0].text = "New Game";
            pauseMenuTxt[1].text = "Save";
            pauseMenuTxt[2].text = "Load";
            pauseMenuTxt[3].text = "Options";
            pauseMenuTxt[4].text = "Exit";
            pauseMenuTxt[5].text = "Volume";
            pauseMenuTxt[6].text = "Sensitive";
            pauseMenuTxt[7].text = "Screen Mode";
            pauseMenuTxt[8].text = "Full Screen";
            pauseMenuTxt[9].text = "Window";
            pauseMenuTxt[10].text = "Lenguage";
            pauseMenuTxt[11].text = "Spanish";
            pauseMenuTxt[12].text = "English";
            pauseMenuTxt[13].text = "Return";

        }
        else
        {
            //PauseMenu
            pauseMenuTxt[0].text = "Resume";
            pauseMenuTxt[1].text = "Save";
            pauseMenuTxt[2].text = "Load";
            pauseMenuTxt[3].text = "Options";
            pauseMenuTxt[4].text = "Main Menu";
            pauseMenuTxt[5].text = "Exit";
            pauseMenuTxt[6].text = "Volume";
            pauseMenuTxt[7].text = "Sensitive";
            pauseMenuTxt[8].text = "Screen Mode";
            pauseMenuTxt[9].text = "Full Screen";
            pauseMenuTxt[10].text = "Window";
            pauseMenuTxt[11].text = "Lenguage";
            pauseMenuTxt[12].text = "Spanish";
            pauseMenuTxt[13].text = "English";
            pauseMenuTxt[14].text = "Return";

            //PLayerUI
            this.GetComponent<GameManager>().GuideTxtConfig(0);
        }
    }

    public void EsLanguage()
    {
        if (this.GetComponent<GameManager>() != null) this.GetComponent<GameManager>().isEnglish = false;
        else this.GetComponent<MenuManager>();
        CambiarIdioma("es-PE");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //PauseMenu
            pauseMenuTxt[0].text = "Nueva Partida";
            pauseMenuTxt[1].text = "Guardar";
            pauseMenuTxt[2].text = "Cargar";
            pauseMenuTxt[3].text = "Opciones";
            pauseMenuTxt[4].text = "Salir";
            pauseMenuTxt[5].text = "Volumen";
            pauseMenuTxt[6].text = "Sensibilidad";
            pauseMenuTxt[7].text = "Modo de pantalla";
            pauseMenuTxt[8].text = "Pantalla completa";
            pauseMenuTxt[9].text = "Ventana";
            pauseMenuTxt[10].text = "Lenguaje";
            pauseMenuTxt[11].text = "Español";
            pauseMenuTxt[12].text = "Ingles";
            pauseMenuTxt[13].text = "Volver";
        }
        else
        {
            //PauseMenu
            pauseMenuTxt[1].text = "Guardar";
            pauseMenuTxt[2].text = "Cargar";
            pauseMenuTxt[3].text = "Opciones";
            pauseMenuTxt[5].text = "Salir";
            pauseMenuTxt[6].text = "Volumen";
            pauseMenuTxt[7].text = "Sensibilidad";
            pauseMenuTxt[8].text = "Modo de pantalla";
            pauseMenuTxt[9].text = "Pantalla completa";
            pauseMenuTxt[10].text = "Ventana";
            pauseMenuTxt[11].text = "Lenguaje";
            pauseMenuTxt[12].text = "Español";
            pauseMenuTxt[13].text = "Ingles";
            pauseMenuTxt[14].text = "Volver";

            //PLayerUI
            this.GetComponent<GameManager>().GuideTxtConfig(0);
        }

    }

    public void CambiarIdioma(string codigoIdioma)
    {
        // Busca el locale correspondiente
        var locale = LocalizationSettings.AvailableLocales.Locales.FirstOrDefault(
            l => l.Identifier.CultureInfo.Name.Equals(codigoIdioma));

        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }
    }

}
