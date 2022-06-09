using Newtonsoft.Json;
using UnityEngine;

namespace Save {
    
    public class ObjectPref<T> : Pref<T> where T : class {
        
        public ObjectPref(string key, T defaultValue) : base(key, defaultValue) { }
        
        public override T Get() {
            string data = PlayerPrefs.GetString(this._key);
            return JsonConvert.DeserializeObject<T>(data);
        }

        public override void Set(T value) {
            string data = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(this._key, data);
        }
        
    }
    
}