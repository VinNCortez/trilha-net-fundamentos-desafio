using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private readonly decimal PrecoInicial = 0;
        private readonly decimal PrecoPorHora = 0;
        private readonly List<Veiculo> Veiculos = new List<Veiculo>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            PrecoInicial = precoInicial;
            PrecoPorHora = precoPorHora;
        }

        public static int GetHorasValidas()
        {

            while (true)
            {
                var quantidadeHoras = Console.ReadLine(); 
                var parseSuccess = int.TryParse(quantidadeHoras, out int resultado);
                if (parseSuccess && resultado >= 0)
                {
                    return resultado;
                }
                else
                {
                    Console.WriteLine($"Valor inválido, por favor digite novamente:");
                }
            }
        }

        public void AdicionarVeiculo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Digite a placa do veículo para estacionar:");

                string placa = Console.ReadLine();

                var veiculo = new Veiculo(placa);

                ICollection<ValidationResult> results = new List<ValidationResult>();
                var validationItems = new Dictionary<object, object> { {"veiculos", Veiculos} };

                bool isValid = Validator.TryValidateObject(veiculo, new ValidationContext(veiculo, validationItems), results, true);

                if (isValid)
                {
                    Veiculos.Add(veiculo);

                    Console.Clear();
                    Console.WriteLine($"O veiculo com placa \"{veiculo.Placa}\" foi adicionado. Pressione" +
                        $" Esc para voltar ao menu, ou pressione qualquer tecla para adicionar um novo veículo");

                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Não foi possível adicionar o veículo com placa {veiculo.Placa}.");
                    Console.WriteLine(string.Join("\n", results.Select(r => r.ErrorMessage)));
                    Console.WriteLine("Pressione Esc para cancelar e voltar ao menu, ou qualquer outra tecla para tentar novamente");
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                }
            }
        }

        public void RemoverVeiculo()
        {
            while (true)
            {
                Console.Clear();
                ExibirVeiculosEstacionados();
                Console.WriteLine("\nDigite a placa do veículo para remover:");
                string placa = Console.ReadLine()?.ToUpper();

                var regexPlaca = new Regex(@"[a-zA-Z]{3}\d[a-zA-Z\d][\d]{2}");

                if (!regexPlaca.IsMatch(placa))
                {
                    Console.WriteLine($"A placa {placa} é inválida.");
                    Console.WriteLine("Pressione Esc para voltar ao menu, ou pressione qualquer outra tecla para tentar novamente");

                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        return;
                    }

                    continue;

                }


                if (Veiculos.Exists(v => v.Placa == placa))
                {
                    Console.Clear();
                    Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                    int horas = GetHorasValidas();
                    decimal valorTotal = PrecoInicial + PrecoPorHora * horas;

                    Veiculos.Remove(Veiculos.Find(v => v.Placa == placa));

                    Console.Clear();
                    Console.WriteLine($"O veículo com placa {placa} foi removido e o preço total foi de: R$ {valorTotal}");
                    Console.WriteLine("Pressione Esc para retornar ao menu, ou pressione qualquer outra tecla para remover outro veículo");
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                    Console.WriteLine("Pressione Esc para voltar ao menu, ou qualquer outra tecla para tentar novamente");
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                }
            }
        }

        public void ExibirVeiculosEstacionados()
        {
            Console.WriteLine(string.Join(", ", Veiculos.Select(v => v.Placa)));
        }

        public void ListarVeiculos()
        {
            if (Veiculos.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("Os veículos estacionados são:");
                ExibirVeiculosEstacionados();
                Console.WriteLine();

                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu...");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu");
                Console.ReadKey(true);
            }
        }
    }
}
