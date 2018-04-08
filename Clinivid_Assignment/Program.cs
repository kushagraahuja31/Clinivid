using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Clinivid_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                //string jsonData = @"{  
                //    'FirstName': 'Jignesh', 'LastName': 'Trivedi'
                //}
                //";
                //dynamic data = JObject.Parse(jsonData);

                //Console.WriteLine(string.Concat("Hi ", data.FirstName, " " + data.LastName));
                //Console.ReadLine();

                string jsonData = "profile|73241232|<Aamir><Hussain><Khan>|<Mumbai><<72.872075><19.075606>>|" +
                    "73241 232.jpg**followers|54543342|<Anil><><Kapoor>|<Delhi><<23.23><12.07>>|54543342.jpg@@|" +
                    "12311334|<Amit><><Bansal>|<Bangalore><<><>>|12311334.jpg";
                profile obj = new profile(jsonData);

                Console.WriteLine(JsonConvert.SerializeObject(obj));
                Console.ReadLine();


            }
        }

        public class profile
        {
            public string id { get; set; }
            public nameClass name { get; set; }
            public locationClass location { get; set; }
            public string imageId { get; set; }
            public List<followerClass> followers { get; set; }

            public profile(string input)
            {
                string[] profileSplit = input.Split(new string[] { "**" }, StringSplitOptions.None);
                for (int j = 0; j < profileSplit.Length; j++)
                {
                    if (j == 0)
                    {
                        string[] jsonSplit = profileSplit[j].Split('|');
                        if (jsonSplit[0] == "profile" || jsonSplit[0] == "followers")
                        {
                            jsonSplit = jsonSplit.Skip(1).ToArray();
                        }
                        for (int i = 0; i < jsonSplit.Length; i++)
                        {
                            if (i == 0)
                            {
                                this.id = jsonSplit[i];
                            }
                            else if (i == 1)
                            {
                                this.name = new nameClass(jsonSplit[i]);
                            }
                            if (i == 2)
                            {
                                this.location = new locationClass(jsonSplit[i]);
                            }
                            if (i == 3)
                            {
                                this.imageId = jsonSplit[i];
                            }
                           
                        }
                    }
                    else
                    {
                        followers = followersList(profileSplit[j]);
                    }
                }
            }
        }


        public class followerClass
        {
            public string id { get; set; }
            public nameClass name { get; set; }
            public locationClass location { get; set; }
            public  string imageId { get; set; }
            public List<profile> followers { get; set; }

            public followerClass(string input)
            {

                string[] jsonSplit = input.Split('|');
                if (jsonSplit[0] == "profile" || jsonSplit[0] == "followers")
                {
                    jsonSplit = jsonSplit.Skip(1).ToArray();
                }
                for (int i = 0; i < jsonSplit.Length; i++)
                {
                    if (i == 0)
                    {
                        this.id = jsonSplit[i];
                    }
                    else if (i == 1)
                    {
                        this.name = new nameClass(jsonSplit[i]);
                    }
                    if (i == 2)
                    {
                        this.location = new locationClass(jsonSplit[i]);
                    }
                    if (i == 3)
                    {
                        this.imageId = jsonSplit[i];
                    }
                    
                }

            }
        }


        public static List<followerClass> followersList(string input)
        {
            List<followerClass> followers = new List<followerClass>();
            string[] splitInput = input.Split(new string[] { "@@" }, StringSplitOptions.None);
            for (int i = 0; i < splitInput.Length; i++)
            {
                followers.Add(new followerClass(splitInput[i]));
            }
            return followers;
        }

        public class nameClass
        {
            public string first { get; set; }
            public string middle { get; set; }
            public string last { get; set; }

            public nameClass(string input)
            {
                input = input.Substring(1, input.Length - 2);
                //input.Replace("<", "");
                string [] jsonSplit = input.Split( new string[] { "><" },StringSplitOptions.None);

                for(int i = 0; i < jsonSplit.Length; i++)
                {
                    if (i == 0)
                    {
                        this.first = jsonSplit[i];
                    }
                    else if (i == 1)
                    {
                        this.middle = jsonSplit[i];
                    }
                    else if (i == 2)
                    {
                        this.last = jsonSplit[i];
                    }
                }
            }
        }
        public class locationClass
        {
            public string name { get; set; }
            public choordsClass choords { get; set; }
            public locationClass(string input)
            {
                input = input.Substring(1, input.Length - 3);
                string[] jsonSplit = input.Split(new string[] { "><<" }, StringSplitOptions.None);
                for(int i = 0; i < jsonSplit.Length; i++)
                {
                    if (i == 0)
                    {
                        this.name = jsonSplit[i];
                    }
                    else if (i==1)
                    {
                        this.choords = new choordsClass(jsonSplit[i]);
                    }
                }
            }

        }
        public class choordsClass
        {
            public string @long { get; set; }
            public  string lat { get; set; }
            public choordsClass(string input)
            {
                //input = input.Substring(0, input.Length - 1);
                string[] jsonSplit = input.Split(new string[] { "><" }, StringSplitOptions.None);
                for(int i = 0; i < jsonSplit.Length; i++)
                {
                    if (i == 0)
                    {
                        this.@long = jsonSplit[i];
                    }
                    else if (i == 1)
                    {
                        this.lat = jsonSplit[i];
                    }
                }
            }
        }


    }
}
