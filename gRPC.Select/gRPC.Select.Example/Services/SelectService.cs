using System.Linq;
using System.Threading.Tasks;
using Example;
using Example.OtherStr;
using Example.Str;
using Grpc.Core;
using gRPC.Select.Interface;
using gRPC.Select.TestDB.TestTools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrpcService.Example.Services
{
    public class SelectService : global::Example.SelectService.SelectServiceBase
    {
        private readonly ISelector _selector;
        private readonly DBContext _context;

        public SelectService(ILogger<SelectService> logger, ISelector selector, DBContext context)
        {
            _selector = selector;
            _context = context;
        }

        public override async Task<Response> SelectOne(Select request, ServerCallContext context)
        {
            var _resp = new Response();
            var _tab = await _selector
                .Apply(
                    _context.Table
                        .Select(model => new SomeData {Id = model.Id, Value = model.StringValue}),
                    request)
                .ToArrayAsync();
            _resp.Tab.AddRange(_tab);
            return _resp;
        }

        public override async Task<Response> SelectTwo(SelectRequest request, ServerCallContext context)
        {
            var _resp = new Response();
            var _tab = await _selector
                .Apply(
                    _context.Table
                        .Select(model => new SomeData {Id = model.Id, Value = model.StringValue}),
                    request)
                .ToArrayAsync();
            _resp.Tab.AddRange(_tab);
            return _resp;
        }
    }
}
