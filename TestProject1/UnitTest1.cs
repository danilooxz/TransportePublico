using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportePublico;

namespace TestProject1
{
    public class GerenciadorRotasTests
    {
        [Fact]
        public void AdicionarRota_DeveAdicionarRotaValida()
        {
            var gerenciador = new GerenciadorRotas();

            gerenciador.AdicionarRota(1, "Rota 1");

            Assert.Contains(gerenciador.BuscarRota(1), gerenciador.Rotas);
        }

        [Fact]
        public void AdicionarParada_DeveAdicionarParadaValida()
        {
            var rota = new Rota(1, "Rota 1");
            var parada = new Parada("Parada 1", TimeSpan.Parse("08:00"), TimeSpan.Parse("08:10"));

            rota.AdicionarParada(parada);

            Assert.Contains(parada, rota.Paradas);
        }

        [Fact]
        public void AdicionarRota_DeveLancarExcecaoParaRotaDuplicada()
        {

            var gerenciador = new GerenciadorRotas();
            gerenciador.AdicionarRota(1, "Rota 1");

            Assert.Throws<Exception>(() => gerenciador.AdicionarRota(1, "Rota 2"));
        }

        [Fact]
        public void RemoverRota_DeveRemoverRotaExistente()
        {
            var gerenciador = new GerenciadorRotas();
            gerenciador.AdicionarRota(1, "Rota 1");

            gerenciador.RemoverRota(1);

            Assert.DoesNotContain(gerenciador.BuscarRota(1), gerenciador.Rotas);
        }

        [Fact]
        public void RemoverRota_DeveLancarExcecaoParaRotaInexistente()
        {
            var gerenciador = new GerenciadorRotas();

            Assert.Throws<Exception>(() => gerenciador.RemoverRota(999));
        }

        [Fact]
        public void RemoverParada_DeveRemoverParadaExistente()
        {
            var rota = new Rota(1, "Rota 1");
            var parada = new Parada("Parada 1", TimeSpan.Parse("08:00"), TimeSpan.Parse("08:10"));
            rota.AdicionarParada(parada);

            rota.RemoverParada("Parada 1");

            Assert.DoesNotContain(parada, rota.Paradas);
        }

        [Fact]
        public void RemoverParada_DeveLancarExcecaoParaParadaInexistente()
        {
            var rota = new Rota(1, "Rota 1");

            Assert.Throws<Exception>(() => rota.RemoverParada("Parada Inexistente"));
        }

        [Fact]
        public void AtualizarNome_DeveAtualizarNomeCorretamente()
        {
            var rota = new Rota(1, "Rota 1");

            rota.AtualizarNome("Nova Rota 1");

            Assert.Equal("Nova Rota 1", rota.Nome);
        }

        [Fact]
        public void ListarRotas_DeveListarTodasAsRotas()
        {

        }

        [Fact]
        public void ListarParadas_DeveListarTodasParadas()
        {
            var rota = new Rota(1, "Rota 1");
            rota.AdicionarParada(new Parada("Parada 1", TimeSpan.Parse("08:00"), TimeSpan.Parse("08:10")));
            rota.AdicionarParada(new Parada("Parada 2", TimeSpan.Parse("08:15"), TimeSpan.Parse("08:25")));

            rota.ListarParadas();
        }

        [Fact]
        public void BuscarRota_DeveRetornarRotaCorreta()
        {
            var gerenciador = new GerenciadorRotas();
            gerenciador.AdicionarRota(1, "Rota 1");

            var rota = gerenciador.BuscarRota(1);

            Assert.NotNull(rota);
            Assert.Equal(1, rota.Numero);
        }

        [Fact]
        public void BuscarRota_DeveRetornarNuloParaRotaInexistente()
        {

        }

        [Fact]
        public void VerificarConflitos_DeveIdentificarConflitosCorretamente()
        {
            var gerenciador = new GerenciadorRotas();
            var rota1 = new Rota(1, "Rota 1");
            rota1.AdicionarParada(new Parada("Parada 1", TimeSpan.Parse("08:00"), TimeSpan.Parse("08:10")));
            gerenciador.AdicionarRota(1, "Rota 1");

            var rota2 = new Rota(2, "Rota 2");
            rota2.AdicionarParada(new Parada("Parada 1", TimeSpan.Parse("08:05"), TimeSpan.Parse("08:15")));
            gerenciador.AdicionarRota(2, "Rota 2");

            var conflitos = gerenciador.VerificarConflitos();

            Assert.False(conflitos);
        }

        [Fact]
        public void VerificarConflitos_AposRemoverRota_DeveRetornarFalso()
        {
            var gerenciador = new GerenciadorRotas();
            var rota1 = new Rota(1, "Rota 1");
            rota1.AdicionarParada(new Parada("Parada 1", TimeSpan.Parse("08:00"), TimeSpan.Parse("08:10")));
            gerenciador.AdicionarRota(1, "Rota 1");

            var rota2 = new Rota(2, "Rota 2");
            rota2.AdicionarParada(new Parada("Parada 1", TimeSpan.Parse("08:05"), TimeSpan.Parse("08:15")));
            gerenciador.AdicionarRota(2, "Rota 2");

            gerenciador.RemoverRota(2);

            var conflitos = gerenciador.VerificarConflitos();

            Assert.False(conflitos);
        }
    }
}
