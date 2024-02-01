using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    private static string path;
    private static char sep;
    
    public string InspectPath;

    public static DataManager Instance;
    public static SaveData Data;
    public UnityEvent<string> OnCoinsUpdate;

    void OnEnable()
    {
        Instance = this;

        path = Application.persistentDataPath;
        sep = Path.DirectorySeparatorChar;
        InspectPath = path;

        Load(Data, "SaveData");
        OnCoinsUpdate.Invoke(Data.Coins.ToString());
    }

    public static void ChangeCoins(int amount)
    {
        Data.Coins += amount;
        Save(Data, "SaveData");
        Instance.OnCoinsUpdate.Invoke(Data.Coins.ToString());
    }
    


    // Turns an instance of a class into JSON, Encrypts and writes the data into a file
    public static void Save(object obj, string name)
    {
        string jsonString = JsonUtility.ToJson(obj); // TURN OBJECT TO JSON
        jsonString = AESEncryption(jsonString); // ENCRYPT
        File.WriteAllText($"{path}{sep}{name}.json", jsonString); // WRITE TO FILE
    }
    
    // Reads a file by name, Decrypts and writes the data into the given object
    public static void Load(object obj, string name)
    {
        if (File.Exists($"{path}{sep}{name}.json"))
        {
            // Write into the linked obj
            string fileContents = File.ReadAllText($"{path}{sep}{name}.json");
            fileContents = AESDecryption(fileContents); // DECRYPT
            JsonUtility.FromJsonOverwrite(fileContents, obj);
        }
        else // When file doesn't exist
        {
            // Save the linked obj
            Save(obj, name);
        }
    }


    #region AES Cryptography
    private static string key = "KLJ679LK234HDFGH098KLJDS978FGXC3"; //set any string of 32 chars
    private static string iv = "DFH507JHGG79X48F"; //set any string of 16 chars

    public static string AESEncryption(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateEncryptor(AEScryptoProvider.Key, AEScryptoProvider.IV);

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Convert.ToBase64String(result);
    }

    public static string AESDecryption(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = Convert.FromBase64String(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateDecryptor();

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return ASCIIEncoding.ASCII.GetString(result);
    }
    #endregion
}

[System.Serializable]
public class SaveData
{
    public int Coins;
}