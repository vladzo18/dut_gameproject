namespace Scripts.Save {

    public interface ISaveSystem<SaveDataType> where SaveDataType : SaveData {

        public void SaveData(SaveDataType data);
        public SaveDataType LoadData();

    }
    
}