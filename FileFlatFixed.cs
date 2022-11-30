using ModelAttributesManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAttributeManager
{
    internal class FileFlatFixed
    {
        public void ReadBy(string fullFileName)
        {
            if (!File.Exists(fullFileName))
                throw new Exception($"File '{fullFileName}' does not exists.");

            using (StreamReader file = new StreamReader(fullFileName)) {
                int counter = 0;
                string line;

                while ((line = file.ReadLine()) != null) {
                    Console.WriteLine(line);
                    counter++;
                }
                file.Close();
             
            }
        }

    }
}
