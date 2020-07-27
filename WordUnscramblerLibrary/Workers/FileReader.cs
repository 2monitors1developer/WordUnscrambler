using System;
using System.IO;

namespace WordUnscramblerLibrary.Workers
{
    public class FileReader
    {
        string[] output;

        public string[] Read(string filename)
        {
            try
            {
                output = File.ReadAllLines(filename);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return output;
        }
    }
}
