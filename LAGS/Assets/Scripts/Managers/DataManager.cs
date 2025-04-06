using System;
using System.IO;
using UnityEngine;

[Serializable]
public static class DataManager
{
    private static string _savePath = Application.persistentDataPath + "/settings.json";

    public static void SaveData(bool isEnglish)
    {
        Data data = new Data();
        data.isEnglish = isEnglish;

        string jsonData = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(_savePath, jsonData);

        Debug.Log("Datos guardados en: " + _savePath);
    }

    // Cargar datos desde JSON
    public static bool LoadData()
    {
        if (File.Exists(_savePath))
        {
            string jsonData = File.ReadAllText(_savePath);
            Data data = JsonUtility.FromJson<Data>(jsonData);

            Debug.Log("Datos cargados. isEnglish: " + data.isEnglish);
            return data.isEnglish;
        }
        else
        {
            Debug.Log("No se encontró archivo de guardado. Usando valor por defecto (false).");
            return false; // Valor por defecto si no hay archivo
        }
    }
}
