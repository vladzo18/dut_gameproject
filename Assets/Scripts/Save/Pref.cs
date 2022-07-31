using UnityEngine;

namespace Save {
    
    public abstract class Pref <T> {

        protected string _key;
        protected T _defaultValue;

        public bool IsSaved => PlayerPrefs.HasKey(_key);

        public Pref(string key, T defaultValue) {
            _key = key;
            _defaultValue = defaultValue;

            if (!IsSaved) Set(_defaultValue);
        }

        public abstract T Get();
        
        public abstract void Set(T value);

        public void Delete() => PlayerPrefs.DeleteKey(_key);

    }

}