using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace subscribers
{
    public partial class Subscriber
    {
        public string FIO
        {
            get
            {
                return lastname + " " + firstname + "." + patromic + ".";
            }
            set
            {

            }
        }

    
        public string ListServices
        {
            get
            {
                List<Subscriber_service> services = Base.EM.Subscriber_service.Where(x => x.id_subcriber == id_subscriber).ToList();
                string nameService = "";
                for (int i = 0; i < services.Count; i++)
                {
                    int ind = services[i].id_service;
                    if (i == services.Count - 1)
                    {
                       
                        List<Services> services1 = Base.EM.Services.Where(x => x.id_service == ind).ToList();

                        nameService = nameService + services1[0].service;
                    }
                    else
                    {
                        
                        List<Services> services1 = Base.EM.Services.Where(x => x.id_service == ind).ToList();

                        nameService = nameService + services1[0].service + ", ";
                    }
                }
                return nameService;
            }
           
        }
    }
}
