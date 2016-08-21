namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class InstantiableRepositoryList
    {
        protected InstantiableRepositoryList()
        {
            InstantiateRepositories();
        }
        protected abstract void InstantiateRepositories();
    }
}
