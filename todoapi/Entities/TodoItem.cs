namespace todoapi.Entities
{
    public class TodoItem
    {
        public int id {get;set;}
        public string description {get;set;}
        public int order {get;set;}
        public string status {get;set;}
        public string itemtype {get;set;}
        public string updated {get;set;}
        public string userident {get;set;}

        public TodoItem(int _id, string _desc, int _order, string _status, string _itemtype, string _updated, string _userident) 
        {
            id = _id;
            description = _desc;
            order = _order;
            status = _status;
            itemtype = _itemtype;
            updated = _updated;
            userident = _userident;
        }

        public TodoItem() : this(-1, "", -1, "", "", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), "") { }
    }
}