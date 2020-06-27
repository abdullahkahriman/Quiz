using Newtonsoft.Json;
using Quiz.Core.Infrastructure.Encryption;
using System;

namespace Quiz.Core
{
    public static class StringExtension
    {
        public static string ToSHA256(this string rawData)
        {
            return Enigma.ComputeSHA256(rawData);
        }
        public static string ToSHA512(this string rawData)
        {
            return Enigma.ComputeSHA512(rawData);
        }
        public static string ToMD5(this string rawData)
        {
            return Enigma.ComputeMD5(rawData);
        }

        public static string ToRijndael(this string plainText)
        {
            return Enigma.Encrypt(plainText);
        }

        public static string FromRijndael(this string cipherText)
        {
            return Enigma.Decrypt(cipherText);
        }

        public static Model FromRijndael<Model>(this string cipherText) where Model : class
        {
            return Enigma.Decrypt(cipherText).FromJson<Model>();
        }

        private static JsonSerializerSettings set = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        public static string ToJson(this object model)
        {
            if (model != null)
                return JsonConvert.SerializeObject(model, set);
            else
                return null;
        }
        
        public static Model FromJson<Model>(this string jsonModel) where Model : class
        {
            if (!string.IsNullOrEmpty(jsonModel))
                return JsonConvert.DeserializeObject<Model>(jsonModel, set);
            else
                return Activator.CreateInstance<Model>();
        }
    }
}