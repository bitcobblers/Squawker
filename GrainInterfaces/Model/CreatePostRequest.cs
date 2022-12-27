using GrainInterfaces.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces.Model
{
    [GenerateSerializer]
    public class CreatePostRequest : Post
    {        
    }

    [GenerateSerializer]
    public class SimpleTextRequest : CreatePostRequest
    {
        public SimpleTextRequest() { }
        public SimpleTextRequest(string body, Guid user, Guid? reply = null)
        {
            this.State = PostState.New;
            this.Author = user;
            this.Content = new[] { new PostContentSection()
            {
                ContentType = "Text",
                Body = body
            }};
            this.TimeStamp = DateTime.Now;
            this.ReplyTo = reply;
        }
    }
}
