using Orleans.CodeGeneration;
using Orleans.Runtime;

namespace CommonTypes
{
    [Version(1)]
    public interface IFoo : IGrainWithStringKey
    {
        Task<string> Bar();
    }

    public class Foo : IGrainBase, IFoo
    {
        public Foo(IGrainContext context) => GrainContext = context;

        public IGrainContext GrainContext { get; }

        public Task<string> Bar()
        {
            return Task.FromResult("version 1");
        }
    }
}