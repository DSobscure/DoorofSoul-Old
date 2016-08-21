namespace DoorofSoul.Library.General.KnowledgeComponents
{
    public class Status
    {
        public int StatusID { get; protected set; }
        public string StatusName { get; protected set; }

        public Status() { }
        public Status(int statusID, string statusName)
        {
            StatusID = statusID;
            StatusName = statusName;
        }
    }
}
