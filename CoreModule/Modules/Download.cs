using System;
using System.IO;
using System.Net;
using Spindle;

namespace CoreModule.Modules {
    /// <summary>
    ///     Module to allow bots to download files
    /// </summary>
    public class Download : IBotModule {
        public string Identifier => "DOWNLOAD";

        public string CommandReceived(string command) {
            if (String.IsNullOrEmpty(command)) return null;

            try {
                Uri uri = new Uri(command);
                string dest = Path.GetFileName(uri.LocalPath);

                if(command.Contains(" ")) {
                    string[] parts = command.Split(' ');
                    uri = new Uri(parts[0]);
                    dest = parts[1].Replace("%20", " ");
                }

                using (var client = new WebClient()) {
                    Console.WriteLine("Downloading {0} to {1}", uri, dest);
                    client.DownloadFile(uri, dest);
                    Console.WriteLine("Done");
                }
            } catch(Exception) {
                // cannot download
            }

            return null;
        }
    }
}
