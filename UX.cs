// ...existing code...
using System;
using System.Linq;
using static System.Console ;

public class UX
{
    private readonly Banco _banco;
    private readonly string _titulo;

    public UX(string titulo, Banco banco)
    {
        _titulo = titulo;
        _banco = banco;
    }

    public void Executar()
    {
        CriarTitulo(_titulo);
        WriteLine(" [1] Criar Conta");
        WriteLine(" [2] Listar Contas");
        WriteLine(" [3] Efetuar Saque");
        WriteLine(" [4] Efetuar Depósito");
        WriteLine(" [5] Aumentar Limite");
        WriteLine(" [6] Diminuir Limite");
        ForegroundColor = ConsoleColor.Red;
        WriteLine("\n [9] Sair");
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
        ForegroundColor = ConsoleColor.Yellow;
        Write(" Digite a opção desejada: ");
        var opcao = ReadLine() ?? "";
        ForegroundColor = ConsoleColor.White;
        switch (opcao)
        {
            case "1": CriarConta(); break;
            case "2": MenuListarContas(); break;
            case "3": Sacar(); break;
            case "4": Depositar(); break;
            case "5": AumentarLimite(); break;
            case "6": DiminuirLimite(); break;
        }
        if (opcao != "9")
        {
            Executar();
        }
        _banco.SaveContas();
    }

    private void CriarConta()
    {
        CriarTitulo(_titulo + " - Criar Conta");
        Write(" Numero:  ");
        if (!int.TryParse(ReadLine(), out var numero))
        {
            CriarRodape("Número inválido.");
            return;
        }
        Write(" Cliente: ");
        var cliente = ReadLine() ?? "";
        Write(" CPF:     ");
        var cpf = ReadLine() ?? "";
        Write(" Senha:   ");
        var senha = ReadLine() ?? "";
        Write(" Limite:  ");
        if (!decimal.TryParse(ReadLine(), out var limite))
        {
            CriarRodape("Limite inválido.");
            return;
        }

        var conta = new Conta(numero, cliente, cpf, senha, limite);
        _banco.Contas.Add(conta);

        CriarRodape("Conta criada com sucesso!");
    }

    private void MenuListarContas()
    {
        CriarTitulo(_titulo + " - Listar Contas");
        foreach (var conta in _banco.Contas)
        {
            WriteLine($" Conta: {conta.Numero} - {conta.Cliente}");
            WriteLine($" Saldo: {conta.Saldo:C} | Limite: {conta.Limite:C}");
            WriteLine($" Saldo Disponível: {conta.SaldoDisponível:C}\n");
        }
        CriarRodape();
    }

    private void CriarLinha()
    {
        WriteLine("-------------------------------------------------");
    }

    private void CriarTitulo(string titulo)
    {
        Clear();
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine(" " + titulo);
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
    }

    private void CriarRodape(string? mensagem = null)
    {
        CriarLinha();
        ForegroundColor = ConsoleColor.Green;
        if (mensagem != null)
            WriteLine(" " + mensagem);
        Write(" ENTER para continuar");
        ForegroundColor = ConsoleColor.White;
        ReadLine();
    }

    private void Sacar()
    {
        CriarTitulo(_titulo + " - Saque");

        Write(" Número da conta: ");
        if (!int.TryParse(ReadLine(), out var numero))
        {
            CriarRodape("Número inválido.");
            return;
        }

        var conta = _banco.Contas.FirstOrDefault(c => c.Numero == numero);

        if (conta == null)
        {
            CriarRodape("Conta não encontrada.");
            return;
        }

        Write(" Valor do saque: ");
        if (!decimal.TryParse(ReadLine(), out var valor))
        {
            CriarRodape("Valor inválido.");
            return;
        }

        if (valor <= 0)
        {
            CriarRodape("Valor inválido.");
            return;
        }

        if (valor > conta.SaldoDisponível)
        {
            CriarRodape("Saldo insuficiente.");
            return;
        }

        conta.Saldo -= valor;

        CriarRodape("Saque realizado com sucesso.");
    }

    private void Depositar()
    {
        CriarTitulo(_titulo + " - Depósito");

        Write(" Número da conta: ");
        if (!int.TryParse(ReadLine(), out var numero))
        {
            CriarRodape("Número inválido.");
            return;
        }

        var conta = _banco.Contas.FirstOrDefault(c => c.Numero == numero);

        if (conta == null)
        {
            CriarRodape("Conta não encontrada.");
            return;
        }

        Write(" Valor do depósito: ");
        if (!decimal.TryParse(ReadLine(), out var valor))
        {
            CriarRodape("Valor inválido.");
            return;
        }

        if (valor <= 0)
        {
            CriarRodape("Valor inválido.");
            return;
        }

        conta.Saldo += valor;

        CriarRodape("Depósito realizado com sucesso.");
    }

    private void AumentarLimite()
    {
        CriarTitulo(_titulo + " - Aumentar Limite");

        Write(" Número da conta: ");
        if (!int.TryParse(ReadLine(), out var numero))
        {
            CriarRodape("Número inválido.");
            return;
        }

        var conta = _banco.Contas.FirstOrDefault(c => c.Numero == numero);

        if (conta == null)
        {
            CriarRodape("Conta não encontrada.");
            return;
        }

        Write(" Valor para aumentar: ");
        if (!decimal.TryParse(ReadLine(), out var valor))
        {
            CriarRodape("Valor inválido.");
            return;
        }

        if (valor <= 0)
        {
            CriarRodape("Valor inválido.");
            return;
        }

        conta.Limite += valor;

        CriarRodape("Limite aumentado com sucesso.");
    }

    private void DiminuirLimite()
    {
        CriarTitulo(_titulo + " - Diminuir Limite");

        Write(" Número da conta: ");
        if (!int.TryParse(ReadLine(), out var numero))
        {
            CriarRodape("Número inválido.");
            return;
        }

        var conta = _banco.Contas.FirstOrDefault(c => c.Numero == numero);

        if (conta == null)
        {
            CriarRodape("Conta não encontrada.");
            return;
        }

        Write(" Valor para diminuir: ");
        if (!decimal.TryParse(ReadLine(), out var valor))
        {
            CriarRodape("Valor inválido.");
            return;
        }

        if (valor <= 0)
        {
            CriarRodape("Valor inválido.");
            return;
        }

        if (valor > conta.Limite)
        {
            CriarRodape("Não é possível diminuir mais que o limite atual.");
            return;
        }

        conta.Limite -= valor;

        CriarRodape("Limite reduzido com sucesso.");
    }
} 
