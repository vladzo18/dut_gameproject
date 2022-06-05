using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Items.Save {
    
    public class BinarySaveSystem {
        
        private readonly string _filePath;

        public BinarySaveSystem(string fileName) {
            _filePath = Application.persistentDataPath + $"/{fileName}.sv";
        }

        public void Save(object data) {
            using (FileStream file = File.Create(_filePath)) {
                (new BinaryFormatter()).Serialize(file, data);
            }
        }

        public T Load<T>() {
            if (!File.Exists(_filePath)) {
                return default;
            }
            
            T saveData;
            
            using (FileStream file = File.Open(_filePath, FileMode.Open)) {
                object loadedData = new BinaryFormatter().Deserialize(file);
                saveData = (T)loadedData;
            }
            
            return saveData;
        } 
        
    }
    
}