using System;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService
    {
        public void Get()
        {
            throw new NotImplementedException();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        
    }
}
