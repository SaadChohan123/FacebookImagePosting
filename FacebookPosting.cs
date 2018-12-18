using Facebook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookBot
{
    public class FacebookPosting
    {
        public string FacebookToken { get; set; }
        public string FacebookPageId { get; set; }
        List<PostingItem> PostingItems { get; set; }
        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }
       public FacebookPosting(PostingItem item)
        {
            this.FacebookToken = "ACCESS_TOKEN";
            this.FacebookPageId = "PAGE_ID";
            this.FacebookAppId = "FACEBOOK_APP_ID";
            this.FacebookAppSecret = "FACEBOOK_APP_SECRET";
            PostingItems = new List<PostingItem>();
            PostingItems.Add(item);
        }
        public object UploadFaceBookBatch(FacebookPosting input)
        {
            try
            {
                var fb = new FacebookClient(input.FacebookToken);
                fb.AppId = input.FacebookAppId;
                fb.AppSecret = input.FacebookAppSecret;
                List<FacebookBatchParameter> fbp = new List<FacebookBatchParameter>();

                int count = 0;
                foreach (var item in input.PostingItems)
                {
                    string extension = Path.GetExtension(item.url);
                    string FileName = Path.GetFileName(item.url);

                    if (!string.IsNullOrEmpty(extension))
                    {
                        extension = extension.Replace(".", "");
                    }

                    var param = new FacebookBatchParameter(HttpMethod.Post, "/me/photos", new Dictionary<string, object> { { "message", item.Title }, { "pic" + count, new FacebookMediaObject { ContentType = "image/" + extension, FileName = FileName }.SetValue(File.ReadAllBytes(item.url)) } });
                    fbp.Add(param);
                    count++;
                }
                
                    return fb.BatchTaskAsync(fbp.ToArray()).Result;
                
                
                
                
            }
            catch (FacebookOAuthException ex)
            {
                Console.WriteLine(ex.Message);
                return ex;
            }
            catch (FacebookApiException ex)
            {
                Console.WriteLine(ex.Message);
                return ex;
            }

        }
    }
}
