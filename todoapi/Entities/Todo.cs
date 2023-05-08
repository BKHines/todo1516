namespace todoapi.Entities
{
    public class TodoItem
    {
        public int id {get;set;}
        public string description {get;set;}
        public int order {get;set;}
        public string status {get;set;}
        public string updated {get;set;}

        public TodoItem(int _id, string _desc, int _order, string _status, string _updated) 
        {
            id = _id;
            description = _desc;
            order = _order;
            status = _status;
            updated = _updated;
        }

        public TodoItem() : this(-1, "", -1, "", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"));
    }
}