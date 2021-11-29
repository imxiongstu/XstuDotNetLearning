namespace AutofacDemo.Services
{
    public class PersonService : IPersonService
    {
        public string Talk()
        {
            return "This is a autofac instance.";
        }
    }
}
