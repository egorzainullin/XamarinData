using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using XamarinData.Model;

namespace XamarinData
{
    public partial class ViewController : UIViewController
    {
        private UITableView _table;
        
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            _table = new UITableView(View.Bounds); 
            var tableItems = new string[]{"Downloading..Press here"};
            _table.Source = new TableSource(tableItems);
            View.AddSubview(_table);
            var url = @"http://partner.market.yandex.ru/pages/help/YML.xml";
            var downloader = new DataDownloader(url);
            downloader.Load();
            downloader.TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            var downloader = sender as DataDownloader;
            var xmlString = downloader.Text;
            var parser = new DataParser(xmlString);
            var offers = parser.Parse();
            var offersID = offers.Select(x => x.ToString());
            var tableItems = offersID.ToArray();
            _table.Source = new TableSource(tableItems);
            _table.ReloadData();
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}