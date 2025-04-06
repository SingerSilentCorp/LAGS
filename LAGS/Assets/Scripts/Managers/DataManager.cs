using System;
using System.IO;
using UnityEngine;

[Serializable]
public static class DataManager
{
    private static string _savePath = Application.persistentDataPath + "/settings.json";

    public static void SaveData(bool isEnglish, float volume, float sensitive)
    {
        Data data = new Data();
        data.isEnglish = isEnglish;
        data.volume = volume;
        data.sensitive = sensitive;

        string jsonData = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(_savePath, jsonData);

        Debug.Log("Datos guardados en: " + _savePath);
    }

    // Cargar TODOS los datos (retorna la clase Data completa)
    public static Data LoadData()
    {
        if (File.Exists(_savePath))
        {
            try
            {
                string jsonData = File.ReadAllText(_savePath);
                Data data = JsonUtility.FromJson<Data>(jsonData);
                Debug.Log("Datos cargados correctamente.");
                return data;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error al cargar datos: " + e.Message);
                return CreateDefaultData();
            }
        }
        else
        {
            Debug.Log("No se encontró archivo. Usando valores por defecto.");
            return CreateDefaultData();
        }
    }

    public static void ResetToDefaultData()
    {
        Data defaultData = CreateDefaultData();
        SaveData(defaultData.isEnglish, defaultData.volume, defaultData.sensitive);
    }

    // Datos por defecto (evita retornar "null")
    private static Data CreateDefaultData()
    {
        return new Data
        {
            isEnglish = false,
            volume = 0.5f,   
            sensitive = 350.0f 
        };
    }
}
