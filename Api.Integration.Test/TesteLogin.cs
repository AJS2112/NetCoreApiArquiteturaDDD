using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test
{
    public class TesteLogin : BaseIntegration
    {
        [Fact(DisplayName = "Teste Login")]
        public async Task TesteDoToken()
        {
            await AdicionarToken();
        }
    }
}