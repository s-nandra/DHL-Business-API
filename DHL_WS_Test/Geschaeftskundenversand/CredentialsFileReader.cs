using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Geschaeftskundenversand
{
    class CredentialsFileReader
    {
        public static Dictionary<String, String> getSettings(String file, Encoding enc)
        {
            var data = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(file, enc))
                data.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
            return data;
        }
    }
}
