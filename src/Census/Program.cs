using Census.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Census
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceFactory
                .Provider
                .GetService<ICensusAnalyzer>()
                .Start();
        }
    }
}
