using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace XamarinData.Model
{
    public class DataDownloader
    {
        private readonly string _url;

        private volatile string text = "";

        public event EventHandler TextChanged;
        
        public string Text
        {
            get => text;
            private set
            {
                text = value;
                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public DataDownloader(string url)
        {
            _url = url;
        }
        
        private async Task<string> LoadString()
        {
            var request = WebRequest.Create(_url);
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream?? Stream.Null);
            return await reader.ReadToEndAsync();
        }

        public async void Load()
        {
            var xmlText = await LoadString();
            Text = xmlText;
        }
    }
}