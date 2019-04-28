using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voyager.Helpers
{
    class Error
    {
        public int id { get; set; }
        public Error(int error_id)
        {
            id = error_id;
            Debug.WriteLine("Error Caught", this.get_name());
            ErrorStorage.errors.Add(this);
        }
        public string get_name()
        {
            string name = "";
            switch (this.id)
            {
                case 0:
                    name = "cmd";
                    break;
                case 1:
                    name = "update";
                    break;
            }
            return name;
        }
    }
    class ErrorStorage
    {
        public static List<Error> errors = new List<Error> { };
    }
}
