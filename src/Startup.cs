using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlockChain.Demo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.Map("/block", builder =>
            {
                builder.Run(async context =>
                {
                    if (context.Request.Method == "POST")
                    {
                        Block block = null;

                        if (Repository.BlockChain.Count == 0)
                        {
                            block = new Block(); //first
                            Repository.BlockChain.Add(block);
                        }
                        else
                        {
                            var data = context.Request.Form["data"][0]; //Form
                            var oldBlock = Repository.BlockChain.Last();

                            block = new Block(oldBlock, Encoding.UTF8.GetBytes(data));
                            if (block.IsValid(oldBlock))
                            {
                                var blockChain = Repository.BlockChain.ToList();
                                blockChain.Add(block);
                                Repository.SwitchChain(blockChain);
                            }
                        }
                        await WriteResponse(context, block);
                    }
                });
            });

            //blocks
            app.Map("/blocks",
                builder => builder.Run(
                    async context => await WriteResponse(context, Repository.BlockChain)
                    )
                );
        }

        private async Task WriteResponse(HttpContext httpContext, object content)
        {
            httpContext.Response.ContentType = "application/json";
            var jsonText = JsonConvert.SerializeObject(content);
            await httpContext.Response.WriteAsync(jsonText);
        }
    }
}
