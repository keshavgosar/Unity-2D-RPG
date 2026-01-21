using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string fullPath;
    private bool encryptData;
    private string codeWord = "unityKESHAVrpg.com";

    public FileDataHandler(string datDirPath, string dataFileName, bool encryptData)
    {
        fullPath = Path.Combine(datDirPath, dataFileName);
        this.encryptData = encryptData;
    }

    public void SaveData(GameData gameData)
    {
        try
        {
            // 1. Create directory if it does not exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // 2. convert GameData to json string
            string dataToSave = JsonUtility.ToJson(gameData, true);

            if (encryptData)
                dataToSave = EncryptDecrypt(dataToSave);

            // 3. Open/Create a new file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                // 4. write the json text to the file
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToSave);
                }
            }
        }

        catch (Exception e)
        {
            // log any error that happens
            Debug.LogError("Erroe on trying to save data to file: " + fullPath + "\n" + e);
        }
    }


    public GameData LoadData()
    {
        GameData loadData = null;

        // 1. check if the save file exists
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                // 2. open the file
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {

                    // 3. read the file text content
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (encryptData)
                    dataToLoad = EncryptDecrypt(dataToLoad);

                // 4. convert the json string back into a GameData object
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }

            catch (Exception e)
            {
                // log any error that happens
                Debug.LogError("Error trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return loadData;
    }

    public void Delete()
    {
        if(File.Exists(fullPath))
            File.Delete(fullPath);
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ codeWord[i % codeWord.Length]);
        }

        return modifiedData;
    }
}
