using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private List<MessageListItemResponseModel> MessageData = new List<MessageListItemResponseModel>();
        private readonly HttpClient httpClient;
        public MessageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            for (int i = 0; i < 100; i++)
            {
                var blog = new MessageListItemResponseModel()
                {
                    Id = "id"+i,
                    Body = "Body"+i,
                    CreateTime = DateTime.Now,
                    CoverImageUrl = "",
                    Type = 1,
                    ImageUrls = new List<string>() { "", "", "" },
                    Title = "Title"+i,
                };
                MessageData.Add(blog);
            }
        }
        public Task<List<MessageListItemResponseModel>> GetMessageListAsync(MessageListRequestModel request)
        {
            //await httpClient.GetFromJsonAsync<List<MessageListItemResponseModel>>($"/api/blog");
            List<MessageListItemResponseModel> resp = MessageData.Skip(request.PageSize * ( request.PageIndex-1)).Take(request.PageSize).ToList();

            return Task.FromResult(resp);
        }

        public Task<MessageListItemResponseModel> GetMessageNextAsync(MessageNextRequestModel request)
        {
            //await httpClient.GetFromJsonAsync<List<MessageListItemResponseModel>>($"/api/blog/next");
            MessageListItemResponseModel resp= new MessageListItemResponseModel();
            var current = MessageData.FirstOrDefault(a=>a.Id==request.CurrentId);
            if (current!=null)
            {
                if (request.Action == 1)
                {
                    resp = MessageData.Where(a => a.CreateTime > current.CreateTime).OrderBy(a => a.CreateTime).FirstOrDefault();
                    if (resp==null)
                    {
                        resp = MessageData.OrderByDescending(a => a.CreateTime).FirstOrDefault();
                    }
                }
                else
                {
                    resp = MessageData.Where(a => a.CreateTime < current.CreateTime).OrderByDescending(a => a.CreateTime).FirstOrDefault();
                    if (resp == null)
                    {
                        resp = MessageData.OrderBy(a => a.CreateTime).FirstOrDefault();
                    }
                }
                
            }
            


            return Task.FromResult(resp);
        }

        public Task<MessageListItemResponseModel> GetMessageAsync(string id)
        {
            var blog = MessageData.FirstOrDefault(a => a.Id == id);

            return Task.FromResult(blog);
        }
    }
}
