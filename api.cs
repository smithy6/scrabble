using System;
using System.Net;
using Newtonsoft.Json;

public class MyDictionary
{
    private string endpoint;
    public bool CheckWord(string word)
    {
        endpoint = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";
        using (var client = new HttpClient())
        {
            var response = client.GetAsync(endpoint).Result;
            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
