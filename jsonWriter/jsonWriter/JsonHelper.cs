using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace jsonWriter
{
   public class Pessoa
    {
       public string name { get; set; }
       public string time { get; set; }
       public string password { get; set; }



    }
    
    public class JsonHelper
    {
        
        public void WriteToJson()
        {

            var person = new Pessoa();
            person.name = "JoBo";
            person.time = DateTime.Now.ToString();
            person.password = "123";
            string serial = JsonConvert.SerializeObject(person);
            System.IO.File.WriteAllText(@"D:\Json\jsonWriter\jsonWriter\data.json", serial);
            
        }

        public Object JsonReader()
        {
            using (StreamReader r = new StreamReader(@"D:\Json\jsonWriter\jsonWriter\data.json"))
            {
                string json = r.ReadToEnd();
                var item = JsonConvert.DeserializeObject(json);
                return item;
            }
        }




    }
}
