using Orleans.CodeGeneration;
using Orleans.Runtime;

namespace CommonTypes
{
    [Version(2)]
    public interface IFoo : IGrainWithStringKey
    {
        Task<string> Bar();
        Task<string> Ping();
    }

    public class Foo : IGrainBase, IFoo
    {
        public Foo(IGrainContext context) => GrainContext = context;

        public IGrainContext GrainContext { get; }

        public Task<string> Bar()
        {
            return Task.FromResult("bar version 2");
        }

        public Task<string> Ping()
        {
            return Task.FromResult("ping version 2");
        }
    }
}