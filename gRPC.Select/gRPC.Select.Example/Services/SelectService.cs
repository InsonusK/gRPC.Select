using System.Linq;
using System.Threading.Tasks;
using Example;
using Example.OtherStr;
using Example.Str;
using Grpc.Core;
using gRPC.Select.Interface;
using gRPC.Select.Test.TestTools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrpcService.Services
{
    public class SelectService : Example.SelectService.SelectServiceBase
    {
        private readonly ILogger<SelectService> _logger;
        private readonly ISelector _selector;
        private readonly DBContext _context;

        public SelectService(ILogger<SelectService> logger, ISelector selector, DBContext context)
        {
            _logger = logger;
            _selector = selector;
            _context = context;
        }

        public override async Task<Response> SelectOne(Select request, ServerCallContext context)
        {
            var resp = new Response();
            var tab = await _selector.Apply(_context.Table, request)
                .Select(model => new SomeData() {Id = model.Id, Value = model.StringValue}).ToArrayAsync();
            resp.Tab.AddRange(tab);
            return resp;
        }

        public override async Task<Response> SelectTwo(SelectRequest request, ServerCallContext context)
        {
            var resp = new Response();
            var tab = await _selector.Apply(_context.Table, request)
                .Select(model => new SomeData() {Id = model.Id, Value = model.StringValue}).ToArrayAsync();
            resp.Tab.AddRange(tab);
            return resp;
        }
    }
}
