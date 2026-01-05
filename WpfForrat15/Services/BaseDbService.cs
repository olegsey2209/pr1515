using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfForrat15.Models;

namespace WpfForrat15.Services
{
    public class BaseDbService
    {
   
        private BaseDbService()
        {
            context = new Forrat158Context();
        }

        private static BaseDbService instance;
        public static BaseDbService Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaseDbService();
                return instance;
            }
        }

        private Forrat158Context context;
        public Forrat158Context Context => context;
    }
}
