namespace WebServiceAccess.HttpModels
{
    public class HttpUser //VALIDATEUSER
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }

    public class HttpUserCreate : HttpUser
    {
        public int Rol { get; set; }
        public int Permissions { get; set; }
    }
}
