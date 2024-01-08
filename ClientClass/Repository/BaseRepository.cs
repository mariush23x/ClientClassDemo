namespace ClientClass.Repository {
    public abstract class BaseRepository<T> where T: class {
        public abstract T Create(T enitity);
        public abstract T Delete(T enitity);
        public abstract T Update(T enitity);
        public abstract void Remove();
        public abstract List<T> GetAll();
        public abstract T? Find(string id);
    }
}
