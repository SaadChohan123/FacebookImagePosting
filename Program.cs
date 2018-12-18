using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookBot
{
    class Program
    {
        static void Main(string[] args)
        {
            PostingItem item = new PostingItem();
            item.Title = "Captain Marvel";
            item.Description = "Captain Marvel Wallpaper";
            item.url = @"D:\01Projects\FacebookPosting\FacebookPosting\thumb-350-824755.png";
            FacebookPosting posting = new FacebookPosting(item);
            
            
            var result= posting.UploadFaceBookBatch(posting);
           
            Console.Read();

        }
    }
}
